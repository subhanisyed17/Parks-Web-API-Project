using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyWeb.Models
{
    public class Trail
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Double Distance { get; set; }
        public enum DifficultyType { Easy, Moderate, Diifcult, Expert }
        public DifficultyType Difficult { get; set; }

        public int NationalParkId { get; set; }

        public NationalPark NationalPark { get; set; }
    }
}
