using System.Diagnostics.CodeAnalysis;

namespace Library.Author.Service.Requests
{
    [ExcludeFromCodeCoverage]
    public class GetAuthorHttpRequest
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}