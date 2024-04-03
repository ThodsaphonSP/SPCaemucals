using AutoMapper;
using SPCaemucals.Backend.Controllers;
using SPCaemucals.Backend.Dto.Role;
using SPCaemucals.Backend.Filters;
using SPCaemucals.Data.Identities;

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



        CreateMap<UnitOfMeasurement, UnitOfMeasurementDTO>().ReverseMap();

        CreateMap<Data.Identities.Product, SPCaemucals.Backend.Dto.ProductDTO>()
            .ReverseMap()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        
        
        CreateMap<Company, SPCaemucals.Backend.Dto.CompanyDTO.Company>().ReverseMap();

        CreateMap<DeliveryVendor, DeliveryVendorDTO>().ReverseMap();

        CreateMap<Customer, CustmerDTO>().ReverseMap();

        CreateMap<DeliveryVendor, DeliveryVendorDTO>().ReverseMap();
        
        CreateMap<ProductParcel, ProductParcelDTO>()
            
            .ReverseMap();
        
        CreateMap<Parcel, ParcelDTO>()
            .ForMember(dest=>dest.ParcelStatusName,opt=>opt.MapFrom(src=>src.ParcelStatus.ToString()))
            .ReverseMap();
        CreateMap<Address, AddressDTO>().ReverseMap();
        CreateMap<Title, TitleDTO>().ReverseMap();

        CreateMap<ProductMoveHistory, ProductMoveHistoryDTO>().ReverseMap();

        CreateMap<Job, JobDTO>().ReverseMap()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        
        
        CreateMap<JobService, JobServiceDTO>().ReverseMap();
        
        
        CreateMap<JobType, JobTypeDTO>().ReverseMap();
        
        

    }
}