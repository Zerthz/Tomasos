using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tömasös.Models
{
    public partial class Product
    {
        public Product()
        {
            Dishes = new HashSet<Dish>();
        }

        public int ProductId { get; set; }

        [Display(Name ="Ingrediensnamn")]
        [RegularExpression("^[a-zA-Z0-9åäöÅÄÖ]*$", ErrorMessage = "Namnet får bara innehålla bokstäver och siffror")]        
        public string ProductName { get; set; } = null!;

        public virtual ICollection<Dish> Dishes { get; set; }
    }
}
