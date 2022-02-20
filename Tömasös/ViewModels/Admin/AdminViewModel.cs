using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Tömasös.Models;
using Tömasös.Services;

namespace Tömasös.ViewModels.Admin
{
    public class AdminViewModel
    {
        public List<AspNetUser> Users { get; set; }
        public DishViewModel DishVM { get; set; }
        public List<Order> Orders { get; set; }
    }
}
