using SPCaemucals.Data.Identities;

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