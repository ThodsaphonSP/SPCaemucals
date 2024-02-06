using AutoMapper;
using SPCaemucals.Data.Models;

namespace SPCaemucals.Backend.Dto.AutoMapperProfiel;

public class MapProFile:Profile
{
    public MapProFile()
    {
        CreateMap<ApplicationUser, UserDto>()
            .ForMember(dto => dto.Company, opt => 
                opt.MapFrom(src => src.Company)).ReverseMap();
        
        
        
        CreateMap<SPCaemucals.Data.Models.Company, SPCaemucals.Backend.Dto.CompanyDTO.Company>().ReverseMap();
        
    }
}