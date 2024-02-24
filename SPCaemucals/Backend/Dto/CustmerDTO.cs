using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using SPCaemucals.Data.Enum;
using SPCaemucals.Data.Identities;
using SPCaemucals.Data.Models;

namespace SPCaemucals.Backend.Dto;

public class CustmerDTO
{
    public int Id { get; set; }
    public CustomerType CustomerType { get; set; }
    
    public int? TitleId { get; set; }
    public virtual TitleDTO Title { get; set; }
    
   
    public int CreditDay { get; set; }
    
   
    public decimal Discount { get; set; }
    
  
    public decimal CreditLimit { get; set; }
    
    public string? AccountNo { get; set; }
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
  
    public string PhoneNo { get; set; }
    public virtual AddressDTO Addresses { get; set; }
    public int AddressId { get; set; }
}