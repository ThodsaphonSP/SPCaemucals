using JetBrains.Annotations;
using Moq;
using SPCaemucals.Backend.Models;
using SPCaemucals.Backend.Services;
using SPCaemucals.Data.Identities;
using Xunit;
using JobService = SPCaemucals.Backend.Services.JobService;

namespace SPChemicalTest.Backend.Services
{
    [UsedImplicitly]
    [TestSubject(typeof(JobService))]
    public class JobCalculatorTest
    {

        

        

        [Fact]
        public async Task TestCalcualtor()
        {
            // Arrange
            JobModel jobModel = new JobModel();

            Job job = new Job();

            JobType jobType = new JobType();
            

            var calculator = new JobCalculator();
            // Act
            calculator.CalculateTatal(jobModel, job, jobType);

            // Assert
            Assert.NotNull(job);
            Assert.Equal(job.Total, job.JobServiceId);
            
        }
    }
}