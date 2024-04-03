using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SPCaemucals.Backend.Interface;
using SPCaemucals.Backend.Models;
using SPCaemucals.Backend.Validator;
using SPCaemucals.Data.Identities;

namespace SPCaemucals.Backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;
        private readonly IMapper _mapper;

        public JobController(IJobService jobService,IMapper mapper)
        {
            _jobService = jobService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var job = await _jobService.GetJobById(id);
            if (job == null)
            {
                return NotFound();
            }
            return Ok(job);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] JobModel model)
        {
            try
            {

                JobModelValidator validator = new JobModelValidator();

                var result = validator.Validate(model);
                // model validation
                if (!result.IsValid)
                {
                    return BadRequest(result);
                }
                
                var userId = HttpContext.User.FindFirstValue("UserId");

                Job job = await _jobService.CreateJob(model,userId);

                JobDTO jobDto = _mapper.Map<JobDTO>(job);

                // add logic here to handle the model
                // typically, you would add it to your database
                // context.Add(model)
                // await context.SaveChangesAsync()

                return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
            }
            catch (Exception ex)
            {
                // LOG ex, not simply discard
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
