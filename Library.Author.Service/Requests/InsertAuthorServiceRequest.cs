using Library.Author.Service.ServiceModels;

namespace Library.Author.Service.Requests
{
    public class InsertAuthorServiceRequest
    {
        public AuthorModel Author { get; set; }
    }
}