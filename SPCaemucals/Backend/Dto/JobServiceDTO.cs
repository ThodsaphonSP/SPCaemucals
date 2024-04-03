using SPCaemucals.Data.Identities;

namespace SPCaemucals.Backend.Controllers;

public class JobServiceDTO
{
    public int Id { get; set; }
    public string JobServiceName { get; set; }
    public virtual IEnumerable<Job>? Jobs { get; set; }
}