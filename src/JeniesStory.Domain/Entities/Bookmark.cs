using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeniesStory.Domain.Entities
{
    public class Bookmark : BaseEntity
    {              
        public string UserId { get; set; }

        public int StoryId { get; set; }

        public User User { get; set; }           
       
        public Story Story { get; set; }
    }
}