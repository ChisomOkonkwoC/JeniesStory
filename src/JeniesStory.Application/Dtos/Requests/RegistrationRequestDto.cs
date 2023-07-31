using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeniesStory.Application.Dtos.Requests
{
    public class RegistrationRequestDto
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        [RegularExpression(@"^[A-Z]{1}[a-z]{2,}$", ErrorMessage = "FirstName should begin with a capital letter, followed by small letters")]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[A-Z]{1}[a-z]{2,}$",
        ErrorMessage = "LastName should begin with a capital letter, followed by small letters")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        //[ValidEmailDomain(ErrorMessage = "Invalid domain name")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$",
         ErrorMessage = "Invalid password. Password should be minimum of 6 characters that include " +
                        "alphanumeric and at least one special characters (@,$,!,%,*,#,?,&,!,^,.)")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$",
         ErrorMessage = "Invalid confirm password. Confirm password should be minimum of 6 characters that include " +
                        "alphanumeric and at least one special characters (@,$,!,%,*,#,?,&,!,^,.) and must match the Password")]
        public string ConfirmPassword { get; set; }        

        public string RoleId { get; set; }

        [Required]
        public string RedirectUrl { get; set; }

    }
}
