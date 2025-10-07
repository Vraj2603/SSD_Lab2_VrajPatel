using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SSD_Lab1.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Phone]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }  

        [StringLength(100)]
        [Display(Name = "City")]
        public string? City { get; set; }         
    }
}
