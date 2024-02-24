using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPCaemucals.Backend.Dto;
using SPCaemucals.Backend.Dto.Model;
using SPCaemucals.Backend.Filters;
using SPCaemucals.Data.Enum;
using SPCaemucals.Data.Identities;
using SPCaemucals.Data.Models;
using SPCaemucals.Data.Utility;

namespace SPCaemucals.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ParcelController : ControllerBase
    {
        private readonly ILogger<ParcelController> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public ParcelController(ILogger<ParcelController> logger, ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userManager,IMapper mapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _userManager = userManager;
            _mapper = mapper;
        }

        // GET: api/<ParcelController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ParcelController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// Creates a new parcel entry.
        /// </summary>
        /// <param name="model">The parcel form data.</param>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ParcelForm model)
        {
            
            string errorMessage = string.Empty;
            await _dbContext.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
            {
                using (var transaction = _dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        var receiver = model.Receive;
                        //check whether customer exists
                        Customer? customer = await _dbContext.Customers.Where(x => x.PhoneNo == receiver.PhoneNo)
                            .Include(x => x.Addresses)
                            .FirstOrDefaultAsync();
                        Customer newCustomer = null;
                        Address newCustomerAddress = null;
                        //save address for customer and app user

                        if (customer == null)
                        {
                            newCustomerAddress = receiver.GetAddress();

                            newCustomer = receiver.GetCustomer();


                            newCustomer.Addresses = newCustomerAddress;

                            _dbContext.Customers.Add(newCustomer);
                            await _dbContext.SaveChangesAsync();
                        }


                        var sender = model.Sender;

                        var userId = HttpContext.User.FindFirstValue("UserId");
                        var saleman = await _userManager.Users.Where(x => x.Id == userId)
                            .Include(x => x.Address).FirstOrDefaultAsync();

                        if (saleman == null)
                        {
                            throw new Exception("User is not logged in or does not exist");
                        }

                        if (saleman.Address == null)
                        {
                            saleman.Address = sender.GetAddress();
                            _dbContext.Entry(saleman).State = EntityState.Modified;
                        }
                        else
                        {
                            var address = saleman.Address;
                            _dbContext.Addresses.Remove(address);

                            saleman.Address = sender.GetAddress();
                            _dbContext.Entry(saleman).State = EntityState.Modified;
                        }

                        await _dbContext.SaveChangesAsync();


                        //create parcel
                        var parcel = new Parcel()
                        {
                            Customer = newCustomer == null ? customer : newCustomer,
                            SaleMan = saleman,
                            ParcelStatus = ParcelStatus.Ready,
                            CashOnDelivery = receiver.Cod,
                            DeliveryVendorId = sender.VendorDelivery.Id
                        };


                        _dbContext.Parcels.Add(parcel);
                        await _dbContext.SaveChangesAsync();

                        List<SelectProduct> senderProductList = sender.SelectProduct.ToList();

                        List<Product> productList = new List<Product>();

                        List<ProductMoveHistory> histList = new List<ProductMoveHistory>();
                        List<ProductParcel> productParcels = new List<ProductParcel>();


                        // Instead of looking up one product at a time in the database, 
// we create a lookup map of products ahead of time.
                        List<int> productIds = senderProductList
                            .Select(selectProduct => selectProduct.IndexNumber.product.Id)
                            .ToList();

                        Dictionary<int, Product> productLookup = _dbContext.Products
                            .Where(p => productIds.Contains(p.Id))
                            .ToDictionary(p => p.Id, p => p);

                        foreach (SelectProduct selectProduct in senderProductList)
                        {
                            if (productLookup.TryGetValue(selectProduct.IndexNumber.product.Id, out var product))
                            {
                                productList.Add(product);

                                var history = new ProductMoveHistory()
                                {
                                    ProductId = product.Id,
                                    CategoryId = product.CategoryId,
                                    Change = selectProduct.IndexNumber.quantity,
                                    QuantityBeforeChange = product.Quantity,
                                    QuantityAfterChange = product.Quantity - selectProduct.IndexNumber.quantity,
                                    MoveType = MoveType.Subtract,
                                    CreatedById = saleman.Id,
                                    UpdatedDate = DateTime.Now
                                };

                                histList.Add(history);
                                product.Quantity -= selectProduct.IndexNumber.quantity;

                                ProductParcel productParcel = new ProductParcel()
                                {
                                    Product = product,
                                    Quantity = selectProduct.IndexNumber.product.Quantity,
                                    Parcel = parcel
                                };

                                productParcels.Add(productParcel);
                            }
                            else
                            {
                                throw new Exception($"product id:{selectProduct.IndexNumber.product.Id}  is not exist");
                            }
                        }


                        //create product in parcel

                        //commit transaction
                        _dbContext.ProductMoveHistories.AddRange(histList);
                        _dbContext.Products.UpdateRange(productList);
                        _dbContext.ProductParcels.AddRange(productParcels);
                        await _dbContext.SaveChangesAsync();
                        transaction.Commit();
                        
                    }
                    catch (Exception exception)
                    {
                        //rollback transaction
                        transaction.Rollback();

                        string message = exception.Message;
                        if (exception.InnerException != null)
                        {
                            message += "\n" + exception.InnerException.Message;
                        }

                        errorMessage = message;
                    }
                }
            });

            if (string.IsNullOrEmpty(errorMessage))
            {
                return Ok();
                
            }
            else
            {
                return BadRequest(errorMessage);
            }
        }

        [HttpGet("getPage")]
       
        public async Task<IActionResult> GetParcelPageLIst(int pageNumber = 1, int pageSize = 10,string receiveName ="")
        {
            var query = _dbContext.Parcels.AsQueryable();
            if (!string.IsNullOrEmpty(receiveName))
            {
                query = query.Where(
                    p => p.Customer.FirstName.Contains(receiveName) || p.Customer.LastName.Contains(receiveName));
            }
            var userId = HttpContext.User.FindFirstValue("UserId");
            query = query.Where(x => x.SaleManId == userId);

            query = query
                .Include(x => x.Customer).ThenInclude(x => x.Addresses)
                .Include(x => x.Customer).ThenInclude(x => x.Addresses).ThenInclude(x => x.Province)
                .Include(x => x.Customer).ThenInclude(x => x.Addresses).ThenInclude(x => x.District)
                .Include(x => x.Customer).ThenInclude(x => x.Addresses).ThenInclude(x => x.SubDistrict)
                .Include(x => x.Customer).ThenInclude(x => x.Addresses).ThenInclude(x => x.PostalCode)
                .Include(x => x.DeliveryVendor)
                .Include(x => x.SaleMan)
                .Include(x => x.SaleMan).ThenInclude(x => x.Address)
                .Include(x => x.SaleMan).ThenInclude(x => x.Address).ThenInclude(x => x.Province)
                .Include(x => x.SaleMan).ThenInclude(x => x.Address).ThenInclude(x => x.District)
                .Include(x => x.SaleMan).ThenInclude(x => x.Address).ThenInclude(x => x.SubDistrict)
                .Include(x => x.SaleMan).ThenInclude(x => x.Address).ThenInclude(x => x.PostalCode);
                
                
            
            var parcels = await PagedList<Parcel>.CreateAsync(query, pageNumber, pageSize);

            var parcelList = parcels.ToList();

            var parcelDto = _mapper.Map<List<ParcelDTO>>(parcelList);
      
            
           

            var newOp = new
            {
                parcels.TotalCount, parcels.CurrentPage, parcels.PageSize, parcels.TotalPages,
                Parcels = parcelDto
            };

            return Ok(newOp);
            
        }

        // PUT api/<ParcelController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ParcelController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}