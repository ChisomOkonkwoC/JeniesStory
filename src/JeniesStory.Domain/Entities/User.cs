using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeniesStory.Domain.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
       
        public byte[] ProfilePicture { get; set; }

        public string RefreshToken { get; set; }

        public DateTime ExpiryTime { get; set; }
        
        public string DeprecatedBy { get; set; }
        
        public DateTime CreatedAt { get; set; } 
        
        public DateTime ModifiedAt { get; set; }

        public DateTime DeprecatedAt { get; set; }

        public bool Status { get; set; } = true;

        public ICollection<Bookmark> Bookmarks { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
