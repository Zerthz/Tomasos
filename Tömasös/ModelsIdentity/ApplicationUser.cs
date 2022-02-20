using Microsoft.AspNetCore.Identity;

namespace Tömasös.ModelsIdentity
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string StreetAddress { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public int PremiumPoints { get; set; }
    }
}
