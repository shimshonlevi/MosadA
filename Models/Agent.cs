﻿using System.ComponentModel.DataAnnotations;

namespace Mosad1.Models
{
    public class Agent
    {
        [Key]
        public int ID { get; set; }
        public string ImageUrl { get; set; }
        public string Alias { get; set; }

        public Location ? Location { get; set; }
        public string Status { get; set; }
    }
}
