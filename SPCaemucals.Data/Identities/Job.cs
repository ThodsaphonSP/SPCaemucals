using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SPCaemucals.Data.Identities;

public class Job
{
    public int Id { get; set; }

    public int JobServiceId { get; set; }
    public virtual JobService JobService { get; set; }

    public virtual JobType JobType { get; set; }
    public int JobTypeId { get; set; }
    [Column(TypeName = "decimal(18, 4)")] public decimal WorkValue { get; set; }
    public string UserId { get; set; }
    public virtual ApplicationUser User { get; set; }
  
    [Column(TypeName = "decimal(18, 4)")] public decimal MC { get; set; }
    public int Quantity { get; set; }
    [Column(TypeName = "decimal(18, 4)")] public decimal Total { get; set; }
}

