namespace SPCaemucals.Data.Identities;

public class Category 
{
    public string Name { get; set; }
    public virtual ICollection<Product> Products { get; set; }
    public virtual ICollection<ProductMoveHistory> ProductMoveHistories { get; set; }
    
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CreatedById { get; set; }
    public DateTime UpdatedDate { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsActive { get; set; }

    public Category()
    {
        CreatedDate = DateTime.Now;
        IsActive = true;
    }
}
