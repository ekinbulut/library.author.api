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
    public class InsertAuthorServiceTest
    {
        public InsertAuthorServiceTest()
        {
            _authorRepositoryMock = new Mock<IAuthorRepository>();
            _authorService = new AuthorService(_authorRepositoryMock.Object);
        }

        private readonly Mock<IAuthorRepository> _authorRepositoryMock;
        private readonly AuthorService _authorService;

        [Theory]
        [MemberData(nameof(InvalidParameters))]
        public void When_Model_IsNotValid_ValidationExceptionOccurs(InsertAuthorServiceRequest input)
        {
            Assert.Throws<ValidationException>(() => _authorService.InsertAuthor(input));
        }


        public static IEnumerable<object[]> InvalidParameters = new List<object[]>
        {
            new object[]
            {
                new InsertAuthorServiceRequest
                {
                    Author = new AuthorModel
                        {Data = new AuthorModelMetaData()}
                }
            }
        };

        [Fact]
        public void When_InsertAuthor_Success_Verify_InsertMethod_Once()
        {
            _authorService.InsertAuthor(new InsertAuthorServiceRequest
            {
                Author = new AuthorModel
                {
                    Name = "Name", Data = new AuthorModelMetaData()
                }
            });

            _authorRepositoryMock.Verify(t => t.Insert(It.IsAny<EAuthor>()), Times.Once);
        }
    }
}