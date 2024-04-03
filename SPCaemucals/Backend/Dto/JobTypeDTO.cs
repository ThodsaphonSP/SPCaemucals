using SPCaemucals.Data.Identities;

namespace SPCaemucals.Backend.Controllers;

public class JobTypeDTO
{
    public int Id { get; set; }
    public string JobTypeName { get; set; }
    public virtual IEnumerable<Job>? Jobs { get; set; }
}