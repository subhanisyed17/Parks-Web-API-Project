using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static ParkyAPI.models.Trail;

namespace ParkyAPI.models.DTOs
{
    public class TrailUpdateDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Double Distance { get; set; }

        public DifficultyType Difficult { get; set; }

        public int NationalParkId { get; set; }

    }
}
