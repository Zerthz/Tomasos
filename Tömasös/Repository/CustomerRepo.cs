using Microsoft.EntityFrameworkCore;
using Tömasös.Models;

namespace Tömasös.Repository
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly TomasosDbContext _context;

        public CustomerRepo(TomasosDbContext context)
        {
            _context = context;
        }

        public List<AspNetUser> GetUsers()
        {
            List<AspNetUser> output = _context.AspNetUsers.Select(u => new AspNetUser
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    UserName = u.UserName,
                    PhoneNumber = u.PhoneNumber,
                    StreetAddress = u.StreetAddress,
                    City = u.City,
                    ZipCode = u.ZipCode,
                }).ToList(); ;

            return output;  
            
        }

        public AspNetUser GetUserById(string id)
        {
            return _context.AspNetUsers
            .Include(u => u.Orders)
            .ThenInclude(u => u.OrderDishes)
            .ThenInclude(u=> u.Dish)
            .Select(u => new AspNetUser
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                UserName = u.UserName,
                PhoneNumber = u.PhoneNumber,
                StreetAddress = u.StreetAddress,
                City = u.City,
                ZipCode = u.ZipCode,
                Orders = u.Orders,
            }).SingleOrDefault(x=> x.Id == id) ?? throw new Exception("Användaren finns inte i databasen.");
        }
    }
}
