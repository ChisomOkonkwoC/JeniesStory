using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeniesStory.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public int StoryId { get; set; }

        public string UserId { get; set; }

        public string AuthorId { get; set; }

        public string Text { get; set; }

        public DateTime CommentDate { get; set; }

        public bool IsApproved { get; set; }

        public User User { get; set; } 

        public Story Story { get; set; }
        
        public Author Author { get; set; }          
        
    }
}