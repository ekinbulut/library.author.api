using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Library.Author.Service.Data.Entities;
using Library.Author.Service.Data.Repositories;
using Library.Author.Service.Requests;
using Library.Author.Service.Responses;
using Library.Author.Service.ServiceModels;
using Library.Author.Service.Validations;
using Library.Common.Data;

namespace Library.Author.Service
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        /// <summary>
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public GetAuthorsServiceResponse GetAuthors(int offset, int limit)
        {
            if (offset < 0)
                throw new ValidationException("Offset value can not be less than 0");

            if (limit > 25 || limit < 0)
                throw new ValidationException("Limit can not be greater than 25 or less than 0");

            var authors = _authorRepository.SelectAll()?.Skip(offset).Take(limit);
            var total = _authorRepository.SelectAll()?.Count();
            if (authors == null) return null;

            return new GetAuthorsServiceResponse
            {
                Total = total, Authors = authors.SelectMany(t => new List<AuthorModel>
                {
                    new AuthorModel
                    {
                        Id = t.Id, Name = t.Name, Data = new AuthorModelMetaData
                        {
                            Birthday = t.Birthday, 
                            Bio = t.Bio, 
                            Dead = t.Dead, 
                            Books = t.Books, 
                            Location = t.Location,
                            ImageUrl = t.ImageUrl
                        }
                    }
                })
            };
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GetAuthorServiceResponse GetAuthor(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("Id can not be null", new Exception());

            var author = _authorRepository.Select(id, SearchType.ID);

            if (author == null) return null;

            return new GetAuthorServiceResponse
            {
                Author = new AuthorModel
                {
                    Id = author.Id, Name = author.Name, Data = new AuthorModelMetaData
                    {
                        Birthday = author.Birthday, Bio = author.Bio, Dead = author.Dead, Books = author.Books,
                        Location = author.Location, ImageUrl = author.ImageUrl
                    }
                }
            };
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        public void DeleteAuthor(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("Id can not be null", new Exception());

            _authorRepository.Delete(id);
        }

        public void UpdateAuthor(AuthorServiceRequest authorServiceRequest)
        {
            var authServiceRequestValidator = new AuthorServiceRequestValidator();
            authServiceRequestValidator.ValidateAndThrow(authorServiceRequest);

            _authorRepository.Update(new EAuthor
            {
                Id = authorServiceRequest.Id, Name = authorServiceRequest.Author.Name,
                Bio = authorServiceRequest.Author.Data.Bio, Dead = authorServiceRequest.Author.Data.Dead,
                Books = authorServiceRequest.Author.Data.Books, Birthday = authorServiceRequest.Author.Data.Birthday,
                Location = authorServiceRequest.Author.Data.Location,
                ImageUrl = authorServiceRequest.Author.Data.ImageUrl
            });
        }

        /// <summary>
        /// </summary>
        /// <param name="insertAuthorServiceRequest"></param>
        public void InsertAuthor(InsertAuthorServiceRequest insertAuthorServiceRequest)
        {
            var insertAuthorServiceRequestValidator = new InsertAuthorServiceRequestValidator();
            insertAuthorServiceRequestValidator.ValidateAndThrow(insertAuthorServiceRequest);

            _authorRepository.Insert(new EAuthor
            {
                Name = insertAuthorServiceRequest.Author.Name, 
                Bio = insertAuthorServiceRequest.Author.Data.Bio,
                Dead = insertAuthorServiceRequest.Author.Data.Dead,
                Books = insertAuthorServiceRequest.Author.Data.Books,
                Birthday = insertAuthorServiceRequest.Author.Data.Birthday,
                Location = insertAuthorServiceRequest.Author.Data.Location,
                ImageUrl = insertAuthorServiceRequest.Author.Data.ImageUrl
            });
        }


        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="searchType"></param>
        /// <returns>GetAuthorServiceResponse</returns>
        public GetAuthorServiceResponse GetAuthor(string key, SearchType searchType)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("Search Key cannot be empty", new Exception());

            var author = _authorRepository.Select(key, searchType);

            if (author == null)
                throw new ArgumentNullException("Author not found", new Exception());

            return new GetAuthorServiceResponse
            {
                Author = new AuthorModel
                {
                    Id = author.Id, Name = author.Name, Data = new AuthorModelMetaData
                    {
                        Birthday = author.Birthday, Bio = author.Bio, Dead = author.Dead, Books = author.Books,
                        Location = author.Location, ImageUrl = author.ImageUrl
                    }
                }
            };
        }
    }
}