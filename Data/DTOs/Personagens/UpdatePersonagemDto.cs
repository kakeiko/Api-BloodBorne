using System.ComponentModel.DataAnnotations;

namespace API_Bloodborne.Data.DTOs.Personagens
{
    public class UpdatePersonagemDto
    {
        [Key]
        [Required(ErrorMessage = "Id obrigatório")]
        public string Id { get; set; }

        [Required(ErrorMessage = "Nome obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Descrição obrigatório")]
        public string Description { get; set; }

        [Required(ErrorMessage = "História obrigatório")]
        public string History { get; set; }

        [Required(ErrorMessage = "Imagem obrigatório")]
        public string Image { get; set; }
    }
}
