using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPCaemucals.Backend.Dto.Model;
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

        public ParcelController(ILogger<ParcelController> logger, ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _dbContext = dbContext;
            _userManager = userManager;
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


                        foreach (SelectProduct selectProduct in senderProductList)
                        {
                            var product =
                                _dbContext.Products.FirstOrDefault(x => x.Id == selectProduct.IndexNumber.product.Id);
                            if (product != null)
                            {
                                productList.Add(product);
                                // save phoduct his


                                var history = new ProductMoveHistory()
                                {
                                    ProductId = product.Id,
                                    CategoryId = product.CategoryId,
                                    Change = selectProduct.IndexNumber.product.Quantity,
                                    QuantityBeforeChange = product.Quantity,
                                    QuantityAfterChange = product.Quantity - selectProduct.IndexNumber.product.Quantity,
                                    MoveType = MoveType.Subtract,
                                    CreatedById = saleman.Id,
                                    UpdatedDate = DateTime.Now
                                };


                                //store stock history
                                histList.Add(history);

                                product.Quantity -= selectProduct.IndexNumber.product.Quantity;

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