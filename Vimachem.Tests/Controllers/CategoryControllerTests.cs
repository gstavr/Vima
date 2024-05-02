using System.Linq.Expressions;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Vimachem.Models.API;
using Vimachem.Models.Domain;
using Vimachem.Repository.IRepository;
using AutoMapper;
using System.Net;
using Vimachem.Controllers;
using Vimachem.Controllers.v1;



namespace Vimachem.Tests.Controllers
{
    public class CategoryControllerTests
    {
        private readonly Mock<ICategoryRepository> _mockCategoryRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CategoryController _controller;

        public CategoryControllerTests()
        {
            _mockCategoryRepo = new Mock<ICategoryRepository>();
            _mockMapper = new Mock<IMapper>();
            _controller = new CategoryController(_mockCategoryRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task DeleteCategory_InvalidId_ReturnsBadRequest()
        {
            // Act
            var result = await _controller.DeleteCategory(0);

            // Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public async Task DeleteCategory_CategoryNotFound_ReturnsNotFound()
        {
            // Arrange
            _mockCategoryRepo.Setup(repo => repo.GetAsync(null, true))
                             .ReturnsAsync((Category)null);

            // Act
            var result = await _controller.DeleteCategory(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task DeleteCategory_ValidId_DeletesCategoryAndReturnsOk()
        {
            // Arrange
            var category = new Category { Id = 1, Name = "Test Category" };
            _mockCategoryRepo.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Category, bool>>>(), true))
                             .ReturnsAsync(category);
            _mockCategoryRepo.Setup(repo => repo.DeleteAsync(category)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteCategory(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var apiResponse = Assert.IsType<APIResponce>(okResult.Value);
            Assert.Equal(HttpStatusCode.NoContent, apiResponse.StatusCode);
            _mockCategoryRepo.Verify(repo => repo.DeleteAsync(category), Times.Once);
        }
    }

}
