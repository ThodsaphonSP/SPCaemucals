using SPCaemucals.Data.Enum;
using SPCaemucals.Data.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPCaemucals.Data.Models;

public class ProductMoveHistory : BaseModel
{
    public Guid ProductId { get; set; }
    [ForeignKey(nameof(ProductId))]
    public virtual Product Product { get; set; }
    public Guid CategoryId { get; set; }
    [ForeignKey(nameof(CategoryId))]
    public virtual Category Category { get; set; }
    public int Change { get; set; }
    public int Remaining { get; set; }
    public MoveType MoveType { get; set; }
}