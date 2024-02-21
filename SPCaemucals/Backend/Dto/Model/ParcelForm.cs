using SPCaemucals.Backend.Controllers;

namespace SPCaemucals.Backend.Dto.Model;

public class ParcelForm
{
    public SenderDetails Sender { get; set; }
    public ReceiverDetails Receive { get; set; }
}