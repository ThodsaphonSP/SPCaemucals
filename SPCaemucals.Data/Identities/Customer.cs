using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using SPCaemucals.Data.Enum;
using SPCaemucals.Data.Models;

namespace SPCaemucals.Data.Identities;

public class Customer
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public CustomerType CustomerType { get; set; }

    [Comment("คำนำหน้าชื่อ")]
    public int TitleId { get; set; }
    public virtual Title Title { get; set; }
    [Comment("เครดิต-วัน")]
    public int CreditDay { get; set; }
    [Column(TypeName = "decimal(18, 4)")]
    public decimal Discount { get; set; }
    [Column(TypeName = "decimal(18, 4)")]
    public decimal CreditLimit { get; set; }
    public string AccountNo { get; set; }
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [Phone]
    public string PhoneNo { get; set; }
    public virtual Address Addresses { get; set; }
        
    public virtual ICollection<Parcel> Parcels { get; set; }
}

public class Title
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Customer> Customers { get; set; }
}