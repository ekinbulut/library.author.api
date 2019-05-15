using System;
using System.Collections.Generic;
using Library.Author.Service.Data.Entities;
using Library.Common.Data;

namespace Library.Author.Service.Data.Repositories
{
    public interface IAuthorRepository
    {
        void Delete(string id);
        void Update(EAuthor entity);
        void Insert(EAuthor entity);
        IEnumerable<EAuthor> SelectAll();
        
        [Obsolete]
        EAuthor Select(string id);
        EAuthor Select(string key, SearchType searchType);
    }
}