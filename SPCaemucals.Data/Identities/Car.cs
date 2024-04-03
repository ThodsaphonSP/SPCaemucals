namespace SPCaemucals.Data.Identities;

public class Car
{
    public int Id { get; set; }
    public string DeliveryManId { get; set; }

    public virtual ApplicationUser DeliveryMan { get; set; }
    public IEnumerable<Parcel>? Parcel { get; set; }
}