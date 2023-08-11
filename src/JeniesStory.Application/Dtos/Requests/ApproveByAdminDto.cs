using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeniesStory.Application.Dtos.Requests
{
    public class ApproveByAdminDto
    {
        public Guid CommentId { get; set; }

        public Guid AdminId { get; set; }

        public Guid RoleId { get; set; }

        public string UserId { get; set; }

        public string Text { get; set; }
    }
}
