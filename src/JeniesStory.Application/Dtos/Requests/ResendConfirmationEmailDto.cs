using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeniesStory.Application.Dtos.Requests
{
    public class ResendConfirmationEmailDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        //[ValidEmailDomain(ErrorMessage = "Invalid domain name")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email")]
        public string Email { get; set; }

        [Required]
        public string RedirectUrl { get; set; }
    }
}
