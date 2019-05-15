using System;
using Library.Author.Service.Data.Repositories;
using Moq;
using Xunit;

namespace Library.Author.Service.Test.AuthorServiceTests
{
    public class DeleteAuthorServiceTest
    {
        public DeleteAuthorServiceTest()
        {
            _authorRepositoryMock = new Mock<IAuthorRepository>();
            _sut = new AuthorService(_authorRepositoryMock.Object);
        }

        private readonly Mock<IAuthorRepository> _authorRepositoryMock;
        private readonly AuthorService _sut;

        [Fact]
        public void If_DeleteAuthor_Id_IsNull_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _sut.DeleteAuthor(string.Empty));
        }

        [Fact]
        public void Verify_DeleteAuthor()
        {
            _sut.DeleteAuthor(Guid.NewGuid().ToString());
            _authorRepositoryMock.Verify(t => t.Delete(It.IsAny<string>()), Times.Once);
        }
    }
}