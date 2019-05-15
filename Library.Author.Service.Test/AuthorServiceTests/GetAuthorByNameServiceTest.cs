using System;
using Library.Author.Service.Data.Entities;
using Library.Author.Service.Data.Repositories;
using Library.Common.Data;
using Moq;
using Xunit;

namespace Library.Author.Service.Test.AuthorServiceTests
{
    public class GetAuthorByNameServiceTest
    {
        private readonly Mock<IAuthorRepository> _authorRepository;
        private readonly AuthorService _sut;

        public GetAuthorByNameServiceTest()
        {
            _authorRepository = new Mock<IAuthorRepository>();
            _sut = new AuthorService(_authorRepository.Object);

            _authorRepository.Setup(s => s.Select(It.IsAny<string>(), It.IsAny<SearchType>()))
                .Returns(() => new EAuthor()
                               {
                                   Id = "string", Bio = "", Dead = null, Name = "name", Books = null
                                   , Birthday = DateTime.Now, Location = "location", ImageUrl = "imgurl"
                               });
        }

        [Fact]
        public void If_Repository_Returns_Throw_ArgumentNullException()
        {
            _authorRepository.Setup(s => s.Select(It.IsAny<string>(), It.IsAny<SearchType>()))
                .Returns(() => null);

            Assert.Throws<ArgumentNullException>(()=> _sut.GetAuthor("name", SearchType.TEXT));
        }

        [Fact]
        public void If_Repository_Returns_ValidResponse_ResponseIsNotNull()
        {
            var actual = _sut.GetAuthor("name", SearchType.TEXT);

            Assert.NotNull(actual);
        }
    }
}