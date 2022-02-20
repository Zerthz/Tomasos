using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tömasös.Models
{
    public partial class AspNetUser
    {
        public AspNetUser()
        {
            Orders = new HashSet<Order>();
        }

        public string Id { get; set; } = null!;

        [StringLength(256, ErrorMessage = "Ditt användarnamn är för långt")]
        [RegularExpression("^[a-zA-Z0-9åäöÅÄÖ]*$", ErrorMessage = "Namnet får bara innehålla bokstäver och siffror")]
        [DisplayName("Användarnamn")]
        public string? UserName { get; set; }
        public string? NormalizedUserName { get; set; }

        [Required(ErrorMessage = "Email är obligatoriskt")]
        [StringLength(256, ErrorMessage = "Din mail är för lång.")]
        [EmailAddress(ErrorMessage = "Felaktig emailadress.")]
        public string? Email { get; set; }
        public string? NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string? PasswordHash { get; set; }
        public string? SecurityStamp { get; set; }
        public string? ConcurrencyStamp { get; set; }

        [DisplayName("Telefonnummer")]
        public string? PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }

        [DisplayName("Stad")]
        [Required(ErrorMessage = "Stad är obligatoriskt")]
        [StringLength(100, ErrorMessage = "Din stad är för lång.")]
        public string City { get; set; } = null!;

        [DisplayName("Namn")]
        [Required(ErrorMessage = "Namn är obligatoriskt")]
        [RegularExpression("^[a-zA-ZåäöÅÄÖ]*$", ErrorMessage = "Namnet får bara innehålla bokstäver")]
        [StringLength(100, ErrorMessage = "Max 100tecken")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Lösenord är obligatoriskt")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{5,}$",
         ErrorMessage = "Lösenordet möter inte kraven")]
        [DisplayName("Lösenord")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Address är obligatoriskt")]
        [StringLength(50, ErrorMessage = "Din address är för lång.")]
        [DisplayName("Address")]
        public string StreetAddress { get; set; } = null!;

        [Required(ErrorMessage = "Postnummer är obligatoriskt")]
        [StringLength(20, ErrorMessage = "Ditt postnummer är för långt.")]
        [DisplayName("Postnummer")]
        public string ZipCode { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; }
    }
}
