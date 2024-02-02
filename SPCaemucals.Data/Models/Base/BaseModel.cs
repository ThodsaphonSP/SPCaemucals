using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPCaemucals.Data.Models.Base;

public class BaseModel
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime UpdatedDate { get; set; }
    public Guid UpdatedBy { get; set; }
    public bool IsActive { get; set; }

    public BaseModel()
    {
        CreatedDate = DateTime.Now;
        IsActive = true;
    }
}