using System.ComponentModel.DataAnnotations;

namespace API_Bloodborne.Data.DTOs.Armas
{
    public class ReadArmaDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public string Image { get; set; }

        public string Damage { get; set; }
    }
}
