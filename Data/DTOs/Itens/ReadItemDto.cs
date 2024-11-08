using System.ComponentModel.DataAnnotations;

namespace API_Bloodborne.Data.DTOs.Itens
{
    public class ReadItemDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public string Image { get; set; }
    }
}

