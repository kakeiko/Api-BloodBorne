using System.ComponentModel.DataAnnotations;

namespace API_Bloodborne.Data.DTOs.Roupas
{
    public class ReadRoupaDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public string Image { get; set; }
    }
}

