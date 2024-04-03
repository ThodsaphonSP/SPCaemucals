using Microsoft.EntityFrameworkCore;
using SPCaemucals.Backend.Controllers;
using SPCaemucals.Backend.Interface;
using SPCaemucals.Backend.Models;
using SPCaemucals.Data.Identities;

namespace SPCaemucals.Backend.Services;

class JobService : IJobService
{
    private readonly ApplicationDbContext _context;

    public JobService(ApplicationDbContext context)
    {
        _context = context;
    }
    public Task<object?> GetJobById(int id)
    {
        throw new NotImplementedException();
    }

  

    public async Task<Job> CreateJob(JobModel model,string userId)
    {
        
        

        
        
        Job job = new Job()
        {
            UserId = userId,
            JobServiceId = model.JobServiceId,
            JobTypeId = model.JobTypeId,
            WorkValue = model.WorkValue,
            Quantity = model.Quantity,
            MC = model.MC,
            
        };

        var calculator = new JobCalculator();

        var jobtype = this._context.JobTypes.FirstOrDefault(x => x.Id == model.JobTypeId);

        calculator.CalculateTatal(model,job,jobtype);


        

        _context.Jobs.Add(job);

        await _context.SaveChangesAsync();

        return job;




        // calculate mc


    }

    
}