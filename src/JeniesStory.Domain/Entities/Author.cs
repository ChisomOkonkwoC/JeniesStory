using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeniesStory.Domain.Entities
{
    public class Author : BaseEntity
    {
        public Guid RoleId { get; set; }

        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string ProfilePictureUrl { get; set; }

        public string Bio { get; set; }  
        
        public User User { get; set; }
        
        public ICollection<Story> Stories { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
