using SPCaemucals.Backend.Controllers;
using SPCaemucals.Backend.Models;
using SPCaemucals.Data.Identities;

namespace SPCaemucals.Backend.Interface;

public interface IJobService
{
    Task<object?> GetJobById(int id);
    Task<Job> CreateJob(JobModel model, string userId);
}