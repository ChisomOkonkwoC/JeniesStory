﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeniesStory.Application.Dtos.Requests
{
    public class StoryRequestDto
    {
        public Guid AuthorId { get; set; }

        public Guid RoleId { get; set; }


        public string Title { get; set; }

        public string Content { get; set; }
    }
}
