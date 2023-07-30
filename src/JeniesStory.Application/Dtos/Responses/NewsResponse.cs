using JeniesStory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeniesStory.Application.Dtos.Responses
{
    public class NewsResponse
    {
        public string Status { get; set; }

        public int TotalResults { get; set; }

        public ICollection<NewsArticle> Articles { get; set; }
    }
}
