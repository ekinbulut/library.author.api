using System;
using System.Diagnostics.CodeAnalysis;

namespace Library.Author.Service.Requests
{
    [ExcludeFromCodeCoverage]
    public class PostAuthorHttpRequest
    {
        public string Name { get; set; }
        public string Bio { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime? Dead { get; set; }
        public string Location { get; set; }
        public string ImageUrl { get; set; }
        public string[] Books { get; set; }
    }
}