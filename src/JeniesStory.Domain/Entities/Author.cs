using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeniesStory.Domain.Entities
{
    public class Author : BaseEntity
    {        
        public string ProfilePictureUrl { get; set; }

        public string Bio { get; set; }       
        
        public ICollection<Story> Stories { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
