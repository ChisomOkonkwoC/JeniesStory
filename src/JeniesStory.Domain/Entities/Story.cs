using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeniesStory.Domain.Entities
{
    public class Story : BaseEntity
    {
        public string AuthorId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime PublishDate { get; set; }        

        public Author Author { get; set; }    
        
        public ICollection<Comment> Comments { get; set; }     
        
        public ICollection<Bookmark> Bookmarks { get; set; }
    }
}