using AutoMapper;
using SPCaemucals.Backend.Controllers;
using SPCaemucals.Backend.Dto.Role;
using SPCaemucals.Backend.Filters;
using SPCaemucals.Data.Models;

namespace SPCaemucals.Backend.Dto.AutoMapperProfiel;

public class MapProFile:Profile
{
    public MapProFile()
    {
        CreateMap<ApplicationUser, UserDto>()
            .ForMember(dto => dto.Company, opt =>
                opt.MapFrom(src => src.Company))
            .ForMember(dto => dto.Roles, opt => opt.MapFrom(src => src.UserRoles.Select(ur => new UserRoleDto { RoleId = ur.RoleId, RoleName = ur.Role.Name ?? string.Empty })))
            
            .ReverseMap();

        CreateMap<Province, ProvinceDTO>().ReverseMap();
        
        CreateMap<PostalCode, PostalDTO>().ReverseMap();

        CreateMap<Category, CategoryDTO>();

        
        CreateMap<SubDistrict, SubDistrictDTO>()
            .ForMember(dto => dto.PostalCodes, opt => opt.MapFrom(src => src.PostalCodes));

        CreateMap<District, DistrictDTO>()
            .ForMember(dto => dto.SubDistricts, opt => opt.MapFrom(src => src.SubDistricts));

            
            
        
        
        
        CreateMap<SPCaemucals.Data.Models.Company, SPCaemucals.Backend.Dto.CompanyDTO.Company>().ReverseMap();
        
    }
}