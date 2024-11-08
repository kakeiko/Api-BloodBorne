using System.ComponentModel.DataAnnotations;

namespace API_Bloodborne.Data.DTOs.Runas
{
    public class ReadRunaDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Effect { get; set; }

        public string Image { get; set; }
    }
}

