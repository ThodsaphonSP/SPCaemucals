namespace SPCaemucals.Backend.Dto;

public class ProductParcelDTO
{
    public int Id { get; set; }
    
    public int Quantity { get; set; }
    public  ProductDTO? Product { get; set; }
    public int ProductId { get; set; }
    public  ParcelDTO? Parcel { get; set; }
    public int ParcelId { get; set; }
}