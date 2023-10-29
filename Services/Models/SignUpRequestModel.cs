using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public class SignUpRequestModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "First Name must be at least 3 characters.")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Last Name must be at least 3 characters.")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public string BirthDate { get; set; }

        [Required]
        [RegularExpression(@"\(\d{3}\) \d{3}-\d{4}", ErrorMessage = "Phone Number must be in the format (999) 999-9999.")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
