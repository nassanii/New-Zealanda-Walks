using System.ComponentModel.DataAnnotations;

namespace NZwalks.API.Models.DTO
{
    public class AddWalkRequestDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "The name has to be not nore than 100 characters")]
        public string Name { get; set; }
        [Required]
        [MaxLength(1000, ErrorMessage = "The discription has to be not more than 1000 characters")]
        [MinLength(20, ErrorMessage = "The discription has to be at least 20 characters")]
        public string Description { get; set; }
        [Required]
        public double LengthInKm { get; set; }
        public string? WalkImgUrl { get; set; }

        // FK
        [Required]
        public Guid DifficultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }
    }
}
