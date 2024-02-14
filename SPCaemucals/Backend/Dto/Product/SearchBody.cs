namespace SPCaemucals.Backend.Dto.Product;

public class SearchBody
{
    public string? DocNo { get; set; }
    public DateTime? DocDate { get; set; } 
    public int? CategoryId { get; set; } 
    public string? CostCenter { get; set; }
    public string? InternalOrder { get; set; }
}