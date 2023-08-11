using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeniesStory.Application.Dtos.Requests
{
    public class ApproveStoryRequestDto
    {
        public Guid StoryId { get; set; }

        public Guid AdminId { get; set; }

        public Guid RoleId { get; set; }
    }
}
