using System;

namespace Library.Author.Service.ServiceModels
{
    public class AuthorModelMetaData
    {
        public string Bio { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime? Dead { get; set; }
        public string Location { get; set; }
        public string[] Books { get; set; }
        public string ImageUrl { get; set; }
    }
}