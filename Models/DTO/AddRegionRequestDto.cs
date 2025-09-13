using System.ComponentModel.DataAnnotations;

namespace NZwalks.API.Models.DTO;

public class AddRegionRequestDto
{
    [Required]
    [MinLength(3, ErrorMessage = "The code has to be 3 charracters")]
    [MaxLength(3, ErrorMessage = "The code has to be 3 charracters")]
    public string Code { get; set; }
    [Required]
    [MaxLength(100, ErrorMessage = "The max length of the name is 100 charracters")]
    public string Name { get; set; }
    public string? RegionImgURL { get; set; }
}