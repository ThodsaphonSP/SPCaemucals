using SPCaemucals.Data.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPCaemucals.Data.Models;

public class Category : BaseModel
{
    public string Name { get; set; }
    public virtual ICollection<Product> Products { get; set; }
    public virtual ICollection<ProductMoveHistory> ProductMoveHistories { get; set; }
}
