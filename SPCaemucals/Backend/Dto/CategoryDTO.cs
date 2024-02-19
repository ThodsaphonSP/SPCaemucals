namespace SPCaemucals.Backend.Dto;

public class CategoryDTO
{
    public string Name { get; set; }
    
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CreatedById { get; set; }
    public DateTime UpdatedDate { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsActive { get; set; }
}