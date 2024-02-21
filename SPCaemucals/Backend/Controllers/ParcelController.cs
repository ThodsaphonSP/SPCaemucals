using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPCaemucals.Backend.Dto.Model;
using SPCaemucals.Data.Identities;

namespace SPCaemucals.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParcelController : ControllerBase
    {
        private readonly ILogger<ParcelController> _logger;
        private readonly ApplicationDbContext _dbContext;

        public ParcelController(ILogger<ParcelController> logger,ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
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
        public IActionResult Post([FromBody] ParcelForm model)
        {
            

            var sender = model.Sender;

            var receiver = model.Receive;
            
            
            //creat customer
            
            
            //save address for customer and app user
            var receiverAddress = new Data.Models.Address();
            
            //create parcel
            
            
            // create product in parcel
            
            
            // เก็บประวัติ ตัดสต๊อก
            
            
            
            
            
            return Ok();
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
