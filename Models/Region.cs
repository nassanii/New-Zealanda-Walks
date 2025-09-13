namespace NZwalks.API.Models;

public class Region
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string? RegionImgURL { get; set; }
}