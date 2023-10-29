using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Services.Models
{
    public class UserModel
    {
        public Guid? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public string? DateOfBirth { get; set; }
        public string? PasswordHash { get; set; }
        public string? Salt { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? EmailConfirmationToken { get; set; }
        public DateTime? TokenExpirationDate { get; set; }
        public bool? IsEmailConfirmed { get; set; }
        public string? RefreshToken { get; set; }
        public List<ClassModel>? Classes { get; set; }
    }
}
