using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeniesStory.Application.Dtos.Responses
{
    public class AuthorResponseDto : BaseResponse
    {
        public string roleId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string ProfilePictureUrl { get; set; }

        public string Bio { get; set; }
    }
}
