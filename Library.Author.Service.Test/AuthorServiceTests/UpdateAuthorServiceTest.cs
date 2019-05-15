using System;
using System.Collections.Generic;
using FluentValidation;
using Library.Author.Service.Data.Entities;
using Library.Author.Service.Data.Repositories;
using Library.Author.Service.Requests;
using Library.Author.Service.ServiceModels;
using Moq;
using Xunit;

namespace Library.Author.Service.Test.AuthorServiceTests
{
    public class UpdateAuthorServiceTest
    {
        public UpdateAuthorServiceTest()
        {
            _authorRepositoryMock = new Mock<IAuthorRepository>();
            _authorService = new AuthorService(_authorRepositoryMock.Object);
        }

        private readonly Mock<IAuthorRepository> _authorRepositoryMock;
        private readonly AuthorService _authorService;

        [Theory]
        [MemberData(nameof(InvalidParameters))]
        public void WhenModelisNotValid_ValidationExceptionOccurs(AuthorServiceRequest input)
        {
            Assert.Throws<ValidationException>(() => _authorService.UpdateAuthor(input));
        }


        public static IEnumerable<object[]> InvalidParameters = new List<object[]>
                                                                {
                                                                    new object[] {new AuthorServiceRequest {Id = null}}
                                                                    , new object[]
                                                                      {
                                                                          new AuthorServiceRequest
                                                                          {
                                                                              Id = Guid.NewGuid().ToString()
                                                                              , Author = new AuthorModel
                                                                                         {
                                                                                             Data =
                                                                                                 new
                                                                                                     AuthorModelMetaData()
                                                                                         }
                                                                          }
                                                                      }
                                                                };

        [Fact]
        public void WhenUpdateAuthor_Success()
        {
            _authorService.UpdateAuthor(new AuthorServiceRequest
                                        {
                                            Id = Guid.NewGuid().ToString(), Author = new AuthorModel
                                                                                     {
                                                                                         Name = "Name"
                                                                                         , Data =
                                                                                             new AuthorModelMetaData()
                                                                                     }
                                        });

            _authorRepositoryMock.Verify(t => t.Update(It.IsAny<EAuthor>()), Times.Once);
        }
    }
}