using SPCaemucals.Data.Identities;
using SPCaemucals.Data.Models;

namespace SPCaemucals.Backend.Dto.Model;

public class ReceiverDetails:BaseDetail
{
    public bool Cod { get; set; }

  

    public Customer GetCustomer()
    {
        Customer customer = new Customer();

        customer.PhoneNo = PhoneNo;
        customer.FirstName = Firstname;
        customer.LastName = Lastname;

        return customer;

    }
}