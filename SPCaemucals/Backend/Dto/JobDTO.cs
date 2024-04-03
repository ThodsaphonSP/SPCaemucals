namespace SPCaemucals.Backend.Controllers;

public class JobDTO
{
    public int Id { get; set; }

    public int JobServiceId { get; set; }
    public virtual JobServiceDTO JobService { get; set; }

    public virtual JobTypeDTO JobType { get; set; }
    public int JobTypeId { get; set; }
    public decimal WorkValue { get; set; }
    public string UserId { get; set; }
    public decimal MC { get; set; }
    public int Quantity { get; set; }

}