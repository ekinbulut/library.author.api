using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Library.Author.Service.ServiceModels;

namespace Library.Author.Service.Responses
{
    [ExcludeFromCodeCoverage]
    public class GetAuthorsHttpResponse
    {
        public int? Total { get; set; }
        public IEnumerable<AuthorModel> Authors { get; set; }
    }
}