using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeniesStory.Application.Dtos.Responses
{
    public class CommentResponseDto : BaseResponse
    {
        public Guid StoryId { get; set; }

        public string UserId { get; set; }

        public Guid AuthorId { get; set; }

        public Guid AdminId { get; set; }

        public string Text { get; set; }

        public bool IsApproved { get; set; }
    }
}
