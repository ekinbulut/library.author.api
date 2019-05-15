using Library.Author.Service.Requests;
using Library.Author.Service.Responses;
using Library.Common.Data;

namespace Library.Author.Service
{
    public interface IAuthorService : IAuthorSearchService
    {
        GetAuthorsServiceResponse GetAuthors(int offset, int limit);
        GetAuthorServiceResponse GetAuthor(string id);
        void DeleteAuthor(string id);
        void UpdateAuthor(AuthorServiceRequest authorServiceRequest);
        void InsertAuthor(InsertAuthorServiceRequest insertAuthorServiceRequest);
    }

    public interface IAuthorSearchService
    {
        GetAuthorServiceResponse GetAuthor(string key, SearchType searchType);
    }
}