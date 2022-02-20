using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tömasös.Models
{
    public partial class DishType
    {
        public DishType()
        {
            Dishes = new HashSet<Dish>();
        }

        [Range(1, double.MaxValue, ErrorMessage = "Kategori är obligatorisk")]
        [Required]
        public int DishType1 { get; set; }
        public string Description { get; set; } = null!;

        public virtual ICollection<Dish> Dishes { get; set; }
    }
}
