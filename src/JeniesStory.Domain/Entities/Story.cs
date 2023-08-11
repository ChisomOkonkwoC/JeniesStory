using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeniesStory.Domain.Entities
{
    public class Story : BaseEntity
    {
        public Guid AuthorId { get; set; }

        public Guid? AdminId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public bool IsApproved { get; set; }

        public bool IsPublished { get; set; }

        public DateTime PublishDate { get; set; }   
        
        public DateTime ApprovedDate { get; set; }        

        public Author Author { get; set; } 
        
        public Admin Admin { get; set; }    
        
        public ICollection<Comment> Comments { get; set; }     
        
        public ICollection<Bookmark> Bookmarks { get; set; }
    }
}