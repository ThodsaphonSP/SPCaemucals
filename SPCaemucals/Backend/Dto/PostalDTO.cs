namespace SPCaemucals.Backend.Controllers;

public class PostalDTO
{
            

    public override string ToString()
    {
        return $"{{ Code = {Code}, SubDistrictId = {SubDistrictId}, Id = {Id} }}";
    }

    public string Code { get; init; }
    public int SubDistrictId { get; init; }
    public int Id { get; init; }

    public void Deconstruct(out string Code, out int SubDistrictId, out int Id)
    {
        Code = this.Code;
        SubDistrictId = this.SubDistrictId;
        Id = this.Id;
    }
}