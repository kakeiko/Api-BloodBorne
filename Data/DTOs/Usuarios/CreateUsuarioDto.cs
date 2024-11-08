using System.ComponentModel.DataAnnotations;

namespace API_Bloodborne.Data.DTOs.Usuarios
{
    public class CreateUsuarioDto
    {

        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
