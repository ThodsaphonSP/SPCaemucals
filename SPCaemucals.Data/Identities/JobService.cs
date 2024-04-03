using System.ComponentModel.DataAnnotations.Schema;
using SPCaemucals.Data.Enum;

namespace SPCaemucals.Data.Identities;

public class JobService
{
    public int Id { get; set; }
    public string JobServiceName { get; set; }
    public virtual IEnumerable<Job>? Jobs { get; set; }
}

public class JobType
{
    public int Id { get; set; }
    public string JobTypeName { get; set; }
    public virtual IEnumerable<Job>? Jobs { get; set; }
}