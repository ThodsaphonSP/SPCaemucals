namespace SPCaemucals.Backend.Dto.Product;

public class ProductResponse {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}