using Microsoft.AspNetCore.Mvc.Rendering;
using Tömasös.Models;

namespace Tömasös.ViewModels.Admin
{
    public class AdminDishViewModel
    {
        public Dish Dish { get; set; }
        public List<SelectListItem> AllIngredients { get; set; }
        public List<SelectListItem> DishCurrentIngredients { get; set; }
        public List<SelectListItem> Categories { get; set; }
        public string DishEditedCategoryName { get; set; }
        public string[] DishEditedIngredients { get; set; }


        public AdminDishViewModel()
        {
            AllIngredients = new List<SelectListItem>();
            DishCurrentIngredients = new List<SelectListItem>();
            Categories = new List<SelectListItem>
            {
                new SelectListItem("Pizza", "Pizza"),
                new SelectListItem("Sallad", "Sallad"),
                new SelectListItem("Pasta", "Pasta"),
                new SelectListItem("Grill", "Grill"),
                new SelectListItem("Dryck", "Dryck")
            };
        }
    }
}
