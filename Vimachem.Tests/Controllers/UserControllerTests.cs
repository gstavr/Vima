using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq.Expressions;
using Vimachem.Repository.IRepository;
using Vimachem.Controllers;
using Vimachem.Models.Domain;
using Vimachem.Models.Dto.User;
using Vimachem.Models.API;
using Vimachem.Controllers.v1;

namespace Vimachem.Tests.Controllers
{
    public class UserControllerTests
    {
        private readonly UserController _controller;
        private readonly Mock<IUserRepository> _mockUserRepo;
        private readonly Mock<IRoleRepository> _mockRoleRepo;
        private readonly Mock<IUserRoleRepository> _mockUserRoleRepo;
        private readonly Mock<IMapper> _mockMapper;

        public UserControllerTests()
        {   
            _mockUserRepo = new Mock<IUserRepository>();
            _mockRoleRepo = new Mock<IRoleRepository>();
            _mockUserRoleRepo = new Mock<IUserRoleRepository>();
            _mockMapper = new Mock<IMapper>();
            _controller = new UserController(_mockUserRepo.Object, _mockRoleRepo.Object, _mockUserRoleRepo.Object, _mockMapper.Object);
        }


        [Fact]
        public async Task GetUsers_ReturnsAllUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new () { Id = 1, Name = "John Doe", UserRoles = new List<UserRole>() },
                new () { Id = 2, Name = "Jane Doe", UserRoles = new List<UserRole>() }
            };
            var userDtos = new List<UserDTO>
            {
                new () { Id = 1, Name = "John Doe" },
                new () { Id = 2, Name = "Jane Doe" }
            };

            _mockUserRepo.Setup(repo => repo.GetAllAsync( null, "UserRoles.Role"))
                .ReturnsAsync(users);
            _mockMapper.Setup(mapper => mapper.Map<List<UserDTO>>(users))
                .Returns(userDtos);

            // Act
            var result = await _controller.GetUsers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var apiResponse = Assert.IsType<APIResponce>(okResult.Value);
            var returnedUsers = Assert.IsType<List<UserDTO>>(apiResponse.Result);
            Assert.Equal(2, returnedUsers.Count);
            Assert.Equal(userDtos, returnedUsers); 
        }


        [Fact]
        public async Task CreateUser_UserExists_ReturnsBadRequest()
        {
            // Arrange
            var userDto = new UserCreateDTO { Name = "John", Surname = "Doe", RoleId = 1 };
            _mockUserRepo.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), false))
                         .ReturnsAsync(new User()); 
            // Act
            var result = await _controller.CreateUser(userDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.IsType<SerializableError>(badRequestResult.Value); 
        }

        [Fact]
        public async Task DeleteUser_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            int userId = 1;
            _mockUserRepo.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<User, bool>>>(),false))
                .ReturnsAsync((User)null);

            // Act
            var result = await _controller.DeleteUser(userId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task DeleteUser_InvalidId_ReturnsBadRequest()
        {
            // Arrange
            int invalidId = 0;  

            // Act
            var result = await _controller.DeleteUser(invalidId);

            // Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }
        
    }

}
