using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Data.DbModels
{
    public class User
    {
        public Guid Id { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "First Name must be at least 3 characters.")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Last Name must be at least 3 characters.")]
        public string LastName { get; set; }

        [NotMapped] 
        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string PasswordHash { get; set; }        
        
        [Required]
        public string Salt { get; set; }

        [RegularExpression(@"\(\d{3}\) \d{3}-\d{4}", ErrorMessage = "Phone Number must be in the format (999) 999-9999.")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        public string? EmailConfirmationToken { get; set; }

        public DateTime? TokenExpirationDate { get; set; }

        public bool? IsEmailConfirmed { get; set; }

        public string? RefreshToken { get; set; }

        public List<Class>? Classes { get; set; }
    }
}
