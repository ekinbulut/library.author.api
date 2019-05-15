using System.Diagnostics.CodeAnalysis;

namespace Library.Author.Service.Requests
{
    [ExcludeFromCodeCoverage]
    public class GetAuthorSearchHttpRequest
    {
        public string SearchText { get; set; }
    }
}