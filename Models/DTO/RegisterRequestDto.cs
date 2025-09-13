using System.ComponentModel.DataAnnotations;

namespace NZwalks.API.Models.DTO
{
    public class RegisterRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }

        public string? Roles { get; set; }
    }
}
