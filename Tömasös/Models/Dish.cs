using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tömasös.Models
{
    public partial class Dish
    {
        public Dish()
        {
            OrderDishes = new HashSet<OrderDish>();
            Products = new HashSet<Product>();
        }

        public int DishId { get; set; }

        
        [RegularExpression(@"^[a-zA-Z0-9åäöÅÄÖ\s]*$", ErrorMessage = "Namnet får bara innehålla bokstäver och siffror")]
        [Required(ErrorMessage = "Namn är obligatoriskt")]
        public string DishName { get; set; } = null!;
        public string? Description { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Priset måste vara över 0.")]
        [Required]
        public int Price { get; set; }
        public int DishType { get; set; }

        public virtual DishType DishTypeNavigation { get; set; } = null!;
        public virtual ICollection<OrderDish> OrderDishes { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
