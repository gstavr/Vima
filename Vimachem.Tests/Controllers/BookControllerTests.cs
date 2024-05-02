using Moq;
using FluentAssertions;
using Vimachem.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Vimachem.Controllers;
using Vimachem.Controllers.v1;
using Vimachem.Models.API;
using Vimachem.Models.Domain;
using Vimachem.Models.Dto.Book;

namespace Vimachem.Tests.Controllers
{
    public class BookControllerTests
    {
        private readonly Mock<IBookRepository> _mockRepo;
        private readonly Mock<ICategoryRepository> _mockCategoryRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly BookController _controller;

        public BookControllerTests()
        {
            _mockRepo = new Mock<IBookRepository>();
            _mockCategoryRepo = new Mock<ICategoryRepository>();
            _mockMapper = new Mock<IMapper>();
            _controller = new BookController(_mockRepo.Object, _mockCategoryRepo.Object, _mockMapper.Object);
        }
        [Fact]
        public async Task GetBooks_ReturnsAllBooks()
        {
            // Arrange
            var books = new List<Book>
            {
                new () {Id = 1, Name = "Book One", CategoryId = 1},
                new () {Id = 2, Name = "Book Two", CategoryId = 2}
            };
            var bookDtos = new List<BookDTO>
            {
                new () {Id = 1, Name = "Book One", CategoryId = 1},
                new () {Id = 2, Name = "Book Two", CategoryId = 2}
            };

            _mockRepo.Setup(repo => repo.GetAllAsync(null)).ReturnsAsync(books);
            _mockMapper.Setup(mapper => mapper.Map<List<BookDTO>>(books)).Returns(bookDtos);

            // Act
            var result = await _controller.GetBooks();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var apiResponse = Assert.IsType<APIResponce>(okResult.Value);
            Assert.NotNull(apiResponse.Result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<List<BookDTO>>(apiResponse.Result);
            apiResponse.Result.Should().BeEquivalentTo(bookDtos); 
        }

        [Fact]
        public async Task GetBooks_ReturnsOkObjectResult_WithAListOfBookDTOs()
        {
            // Arrange
            var books = new List<Book>
            {
                new () {Id = 1, Name = "Book One", CategoryId = 1},
                new () {Id = 2, Name = "Book Two", CategoryId = 2},
                new () {Id = 3, Name = "Book Three", CategoryId = 3},
                new () {Id = 4, Name = "Book Four", CategoryId = 4},
                new () {Id = 5, Name = "Book Five", CategoryId = 5}
            };

            _mockRepo.Setup(repo => repo.GetAllAsync(null)).ReturnsAsync(books);
            var bookDtos = new List<BookDTO>
            {
                new () {Id = 1, Name = "Book One", CategoryId = 1},
                new () {Id = 2, Name = "Book Two", CategoryId = 2},
                new () {Id = 3, Name = "Book Three", CategoryId = 3},
                new () {Id = 4, Name = "Book Four", CategoryId = 4},
                new () {Id = 5, Name = "Book Five", CategoryId = 5}
            };

            _mockMapper.Setup(mapper => mapper.Map<List<BookDTO>>(It.IsAny<List<Book>>())).Returns(bookDtos);

            // Act
            var result = await _controller.GetBooks();

            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var response = okResult.Value.Should().BeAssignableTo<APIResponce>().Subject;
            response.Result.Should().BeEquivalentTo(bookDtos);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }

}

