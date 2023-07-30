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

        public ICollection<NewsArticles> Articles { get; set; }
    }

    public class NewsArticles
    {        
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string UrlToImage { get; set; }
        public DateTime PublishedAt { get; set; }
        public string Content { get; set; }
        public NewsSource Source { get; set; }
    }



    public class NewsSource
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
