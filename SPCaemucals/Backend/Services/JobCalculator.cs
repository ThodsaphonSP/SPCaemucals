using SPCaemucals.Backend.Models;
using SPCaemucals.Data.Identities;

namespace SPCaemucals.Backend.Services;

public class JobCalculator
{
    public void CalculateTatal(JobModel model,Job job,JobType jobType)
    {
        
        
        
        decimal netSale = (model.WorkValue*model.Quantity) - model.MC;

        if (jobType.JobTypeName.Contains("ล้าง"))
        {
            // ยอดขาย
            
            
            job.Total = (netSale * 80)/100;
        }
        
    }
}