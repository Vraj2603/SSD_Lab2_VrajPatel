using System.ComponentModel.DataAnnotations;

namespace SSD_Lab1.Models
{
    public class Company
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, Range(0, 200)]
        [Display(Name = "Years in Business")]
        public int YearsInBusiness { get; set; }

        [Required, Url]
        public string Website { get; set; }

        public string? Province { get; set; }
    }
}
