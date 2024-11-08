using System.ComponentModel.DataAnnotations;

namespace API_Bloodborne.Data.DTOs.Armas
{
    public class UpdateArmaDto
    {
        [Key]
        [Required]
        public string Id { get; set; }

        [Required(ErrorMessage = "Nome obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Descrição obrigatório")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Tipo obrigatório")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Imagem obrigatório")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Dano obrigatório")]
        public string Damage { get; set; }
    }
}
