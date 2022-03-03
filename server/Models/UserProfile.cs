using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Features.Models
{
    public class UserProfile
    {
        [Key]

        public int UserId { get; set; }

        [Required(ErrorMessage = "is required")]
        [MinLength(2, ErrorMessage = "must be at least 2 characters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "is required")]
        [MinLength(2, ErrorMessage = "must be at least 2 characters")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Address { get; set; }

        [Required]
        [Display(Name = "DOB")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        public string AboutMe { get; set; }

        [NotMapped]
        public object UserName { get; internal set; }
    }
}
