using Library.Author.Service.ServiceModels;

namespace Library.Author.Service.Requests
{
    public class AuthorServiceRequest
    {
        public AuthorModel Author { get; set; }
        public string Id { get; set; }
    }
}