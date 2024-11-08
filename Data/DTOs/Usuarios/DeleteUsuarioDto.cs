using System.ComponentModel.DataAnnotations;

namespace API_Bloodborne.Data.DTOs.Usuarios
{
    public class DeleteUsuarioDto
    {

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }


    }
}
