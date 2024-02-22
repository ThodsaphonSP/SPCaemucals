using SPCaemucals.Data.Identities;
using SPCaemucals.Data.Models;

namespace SPCaemucals.Backend.Dto.Model;

public class ReceiverDetails:BaseDetail
{
    public bool Cod { get; set; }

  

    public Customer GetCustomer()
    {
        Customer customer = new Customer();

        customer.PhoneNo = "0918131501";
        customer.FirstName = "ทศพล";
        customer.LastName = "sonthiphin";

        return customer;

    }
}