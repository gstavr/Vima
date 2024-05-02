using AutoMapper;
using Vimachem.Models.Domain;
using Vimachem.Models.Dto;
using Vimachem.Models.Dto.Book;
using Vimachem.Models.Dto.Category;
using Vimachem.Models.Dto.Role;
using Vimachem.Models.Dto.User;

namespace Vimachem.Mapping
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {   
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Category, CategoryUpdateDTO>().ReverseMap();
            CreateMap<Category, CategoryCreateDTO>().ReverseMap();

            CreateMap<Book, BookDTO>().ReverseMap();
            CreateMap<Book, BookUpdateDTO>().ReverseMap();
            CreateMap<Book, BookCreateDTO>().ReverseMap();

            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserCreateDTO>().ReverseMap();
            CreateMap<User, UserUpdateDTO>().ReverseMap();
            
            CreateMap<Role, RoleDTO>().ReverseMap();
            CreateMap<UserRole, UserRoleDTO>().ReverseMap();

            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.Roles,
                    opt => opt.MapFrom(src =>
                        src.UserRoles.Select(ur => new RoleDTO {Id = ur.RoleId, Name = ur.Role.Name})));

            
        }
    }
}
