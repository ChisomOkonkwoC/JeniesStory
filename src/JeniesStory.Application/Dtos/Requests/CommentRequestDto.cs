using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeniesStory.Application.Dtos.Requests
{
    public class CommentRequestDto
    {
        public Guid StoryId { get; set; }

        public string UserId { get; set; }

        public string Text { get; set; }
    }
}
