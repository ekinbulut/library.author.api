using System.Collections.Generic;
using Library.Author.Service.ServiceModels;

namespace Library.Author.Service.Responses
{
    public class GetAuthorsServiceResponse
    {
        public int? Total { get; set; }
        public IEnumerable<AuthorModel> Authors { get; set; } = new List<AuthorModel>();
    }
}