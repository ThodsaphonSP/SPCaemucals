namespace SPCaemucals.Backend.Models;

public class JobModel
{

    public int Id { get; set; }
    public int JobServiceId { get; set; }
    public int JobTypeId { get; set; }
    public decimal WorkValue { get; set; }
    public int Quantity { get; set; }
    public decimal MC { get; set; }
}