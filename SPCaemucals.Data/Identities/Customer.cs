using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using SPCaemucals.Data.Enum;
using SPCaemucals.Data.Models;

namespace SPCaemucals.Data.Identities;

public class Customer:IEqualityComparer<Customer>
{
    public int Id { get; set; }
    public CustomerType CustomerType { get; set; }

    [Comment("คำนำหน้าชื่อ")]
    public int? TitleId { get; set; }
    public virtual Title Title { get; set; }
    [Comment("เครดิต-วัน")]
    public int CreditDay { get; set; }
    [Column(TypeName = "decimal(18, 4)")]
    public decimal Discount { get; set; }
    [Column(TypeName = "decimal(18, 4)")]
    public decimal CreditLimit { get; set; }
    public string? AccountNo { get; set; }
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [Phone]
    public string PhoneNo { get; set; }
    public virtual Address Addresses { get; set; }
        
    public virtual ICollection<Parcel> Parcels { get; set; }
    public int AddressId { get; set; }

    public bool Equals(Customer x, Customer y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null)) return false;
        if (ReferenceEquals(y, null)) return false;
        if (x.GetType() != y.GetType()) return false;
        return x.CustomerType == y.CustomerType 
               && x.FirstName == y.FirstName 
               && x.LastName == y.LastName 
               && x.PhoneNo == y.PhoneNo;
    }

    public int GetHashCode(Customer obj)
    {
        return HashCode.Combine((int)obj.CustomerType, obj.FirstName, obj.LastName, obj.PhoneNo);
    }
}

public class Title
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Customer> Customers { get; set; }
}