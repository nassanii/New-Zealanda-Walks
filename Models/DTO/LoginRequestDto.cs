using System.ComponentModel.DataAnnotations;

namespace NZwalks.API.Models.DTO
{
    public class LoginRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string uersname { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
