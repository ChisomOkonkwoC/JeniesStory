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
        public string status { get; set; }

        public int totalResults { get; set; }

        public ICollection<NewsArticles> articles { get; set; }
    }

    public class NewsArticles
    {        
        public string author { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string Url { get; set; }
        public string urlToImage { get; set; }
        public DateTime publishedAt { get; set; }
        public string content { get; set; }
        public NewsSource source { get; set; }
    }



    public class NewsSource
    {
        public string id { get; set; }

        public string name { get; set; }
    }
}
