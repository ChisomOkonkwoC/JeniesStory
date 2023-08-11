using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeniesStory.Domain.Entities
{
    public class Admin : BaseEntity
    {        
           public string UserId { get; set; }

           public Guid RoleId { get; set; }

            public string Name { get; set; }

            public string Email { get; set; }

            public User User { get; set; }

            public ICollection<Story> ApprovedStories { get; set; }

            public ICollection<Comment> ApprovedComments { get; set; }
        
    }
}
