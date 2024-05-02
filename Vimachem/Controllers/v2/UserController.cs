using Microsoft.AspNetCore.Mvc;
using Vimachem.Models.Domain;
using AutoMapper;
using Vimachem.Repository.IRepository;
using Vimachem.Models.API;
using Vimachem.Models.Dto.User;
using System.Net;
using Vimachem.Models.Dto;
using Asp.Versioning;

namespace Vimachem.Controllers.v2
{
    
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    public class UserController : ControllerBase
    {

        protected APIResponce _responce;
        private readonly IUserRepository _dbUser;
        private readonly IRoleRepository _dbRole;
        private readonly IUserRoleRepository _dbUserRole;
        private readonly IMapper _mapper;


        public UserController(IUserRepository dbUser, IRoleRepository dbRole, IUserRoleRepository dbUserRole, IMapper mapper)
        {
            _dbUser = dbUser;
            _dbRole = dbRole;
            _dbUserRole = dbUserRole;
            _mapper = mapper;
            _responce = new();

        }

        // GET: api/User
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTO))]
        public async Task<ActionResult<APIResponce>> GetUsers()
        {
            IEnumerable<User> list = await _dbUser.GetAllAsync(includePaths: "UserRoles.Role");

            _responce.Result = _mapper.Map<List<UserDTO>>(list);

            return Ok(_responce);
        }

        [HttpGet("{id:int}", Name = "GetUser")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponce>> GetUser(int id)
        {
            if (id.Equals(0))
            {
                _responce.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_responce);
            }

            var user = await _dbUser.GetAsync(u => u.Id == id, includePaths: "UserRoles.Role");

            if (user is null)
            {
                _responce.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_responce);
            }

            _responce.Result = _mapper.Map<UserDTO>(user);

            return Ok(_responce);
        }

        // POST: api/User
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponce>> CreateUser([FromBody] UserCreateDTO createUserDTO)
        {

            if (await _dbUser.GetAsync(x => x.Name.ToLower() == createUserDTO.Name.ToLower().Trim() && x.Surname.ToLower() == createUserDTO.Surname.ToLower().Trim()) != null)
            {
                ModelState.AddModelError("CustomError", "User already exists!");
                return BadRequest(ModelState);
            }

            if (await _dbRole.GetAsync(x => x.Id.Equals(createUserDTO.RoleId)) is null)
            {
                ModelState.AddModelError("CustomError", "Role is does not exists!");
                return BadRequest(ModelState);
            }

            var user = _mapper.Map<User>(createUserDTO);

            user.CreatedDate = DateTime.Now;
            await _dbUser.CreateAsync(user);
            await _dbUser.SaveUserRoleAsync(user);

            // TODO: Find a better way in order not hit the database again
            var userRes = await _dbUser.GetAsync(u => u.Id == user.Id, includePaths: "UserRoles.Role", track: false);
            _responce.Result = _mapper.Map<UserDTO>(userRes);
            _responce.StatusCode = HttpStatusCode.Created;

            return CreatedAtRoute("GetUser", new { id = user.Id }, _responce);
        }


        [HttpPut("updateRole", Name = "UpdateUserRole")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserDTO))]
        public async Task<IActionResult> UpdateUserRoles([FromBody] UserRoleDTO updateUserRoleDTO)
        {
            var userRole = await _dbUserRole.GetAsync(u => u.UserId == updateUserRoleDTO.UserId && u.RoleId == updateUserRoleDTO.ExistingRoleId);

            var role = await _dbRole.GetAsync(x => x.Id.Equals(updateUserRoleDTO.RoleId), track: false);
            if (userRole is null)
            {
                _responce.StatusCode = HttpStatusCode.BadRequest;
                _responce.ErrorMessages = new() { $"User with this role doesn't exists!" };
                return BadRequest(_responce);
            }

            if (role is null)
            {
                _responce.StatusCode = HttpStatusCode.BadRequest;
                _responce.ErrorMessages = new() { $"New role doesn't exists!" };
                return BadRequest(_responce);
            }

            // TODO: If i have more time i will have a way to update not make 2 calls to the database
            await _dbUserRole.DeleteAsync(userRole);
            var createUserRole = _mapper.Map<UserRole>(updateUserRoleDTO);
            await _dbUserRole.CreateAsync(createUserRole);


            _responce.Result = createUserRole;

            return CreatedAtRoute("GetUser", new { id = createUserRole.UserId }, _responce);
        }

        [HttpPut("{id:int}", Name = "UpdateUser")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<APIResponce>> UpdateUser([FromBody] UserUpdateDTO updateUserDto)
        {

            if (await _dbUser.GetAsync(x => x.Id.Equals(updateUserDto.Id)) is null)
            {
                ModelState.AddModelError("CustomError", "User doesn't exists!");
                return BadRequest(ModelState);
            }

            User user = _mapper.Map<User>(updateUserDto);

            await _dbUser.UpdateAsync(user);

            _responce.StatusCode = HttpStatusCode.NoContent;
            _responce.IsSuccess = true;


            return CreatedAtRoute("GetUser", new { id = user.Id }, _responce);

        }

        [HttpDelete("{id:int}", Name = "DeleteUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponce>> DeleteUser(int id)
        {
            if (id.Equals(0))
                return BadRequest();

            var user = await _dbUser.GetAsync(c => c.Id == id);

            if (user == null)
                return NotFound();

            await _dbUser.DeleteAsync(user);


            _responce.StatusCode = HttpStatusCode.NoContent;

            return Ok(_responce);
        }
    }

}
