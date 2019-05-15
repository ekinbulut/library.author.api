using System;
using System.Collections.Generic;
using FluentValidation;
using Library.Author.Service.Data.Entities;
using Library.Author.Service.Data.Repositories;
using Library.Author.Service.Responses;
using Library.Author.Service.ServiceModels;
using Library.Common.Data;
using Moq;
using Xunit;

namespace Library.Author.Service.Test.AuthorServiceTests
{
    public class GetAuthorServiceTest
    {
        public GetAuthorServiceTest()
        {
            _authorRepositoryMock = new Mock<IAuthorRepository>();

            _authorRepositoryMock.Setup(t => t.SelectAll()).Returns(() => new List<EAuthor>
            {
                new EAuthor
                {
                    Bio = "",
                    Birthday = DateTime.Now,
                    Books = null,
                    Dead = null,
                    Id = "1",
                    ImageUrl = "",
                    Location = "",
                    Name = "name"
                }
            });

            _authorRepositoryMock.Setup(t => t.Select(It.IsAny<string>(), SearchType.ID)).Returns(() => new EAuthor
            {
                Id = "id",
                Bio = "bio",
                Dead = null,
                Name = "name",
                Books = new[] {"book"},
                Birthday = DateTime.Now,
                Location = "Location",
                ImageUrl = string.Empty
            });

            _sut = new AuthorService(_authorRepositoryMock.Object);
        }

        private readonly Mock<IAuthorRepository> _authorRepositoryMock;
        private AuthorService _sut;

        [Fact]
        public void If_GetAuthor_Id_IsNull_Throw_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _sut.GetAuthor(string.Empty));
        }

        [Fact]
        public void If_GetAuthor_Key_IsNull_Throw_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _sut.GetAuthor(string.Empty, SearchType.TEXT));
        }

        [Fact]
        public void If_GetAuthors_Limit_Is_GreaterThan_Than_25_Throw_ValidationException()
        {
            Assert.Throws<ValidationException>(() => _sut.GetAuthors(0, 26));
        }

        [Fact]
        public void If_GetAuthors_Limit_Is_Less_Than_Zero_Throw_ValidationException()
        {
            Assert.Throws<ValidationException>(() => _sut.GetAuthors(0, -1));
        }

        [Fact]
        public void If_GetAuthors_Offset_Is_Less_Than_Zero_Throw_ValidationException()
        {
            Assert.Throws<ValidationException>(() => _sut.GetAuthors(-1, 25));
        }

        [Fact]
        public void When_GetAuthorById_Returns_Valid_Response()
        {
            var actual = _sut.GetAuthor(Guid.NewGuid().ToString());

            Assert.NotNull(actual);
            Assert.IsType<GetAuthorServiceResponse>(actual);
            Assert.IsType<AuthorModel>(actual.Author);
        }

        [Fact]
        public void When_GetAuthors_Returns_Valid_Response()
        {
            var actual = _sut.GetAuthors(0, 10);

            Assert.Equal(1, actual.Total);
            Assert.IsType<GetAuthorsServiceResponse>(actual);
        }

        [Fact]
        public void When_Select_ReturnsNull_Response_Returns_Null()
        {
            _authorRepositoryMock.Setup(t => t.Select(It.IsAny<string>(), SearchType.ID)).Returns(() => null);

            var actual = _sut.GetAuthor(Guid.NewGuid().ToString());

            Assert.Null(actual);
        }

        [Fact]
        public void When_SelectAll_ReturnsNull_Response_Returns_Null()
        {
            _authorRepositoryMock.Setup(t => t.SelectAll()).Returns(() => null);
            _sut = new AuthorService(_authorRepositoryMock.Object);

            var actual = _sut.GetAuthors(0, 10);

            Assert.Null(actual);
        }
    }
}