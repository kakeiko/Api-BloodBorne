﻿using System.ComponentModel.DataAnnotations;

namespace API_Bloodborne.Data.DTOs.Inimigos
{
    public class ReadInimigoDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string History { get; set; }

        public int Health { get; set; }

        public string Image { get; set; }
    }
}