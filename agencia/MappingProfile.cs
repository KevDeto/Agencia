using agencia.Models.Dto;
using agencia.Models.Entity;
using AutoMapper;

namespace agencia
{
    public class MappingProfile : Profile
    {
        //Profiles
        public MappingProfile()
        {
            CreateMap<ClientDTO, Client>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Client, ClientDTO>();
            CreateMap<EmployeeDTO, Employee>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Employee, EmployeeDTO>();
            CreateMap<PackageDTO, Package>()
                .ForMember(dest => dest.services, opt => opt.Ignore()) // Manejamos esto manualmente
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // Para updates
            CreateMap<Package, PackageDTO>()
                .ForMember(dest => dest.serviceIDs,
                           opt => opt.MapFrom(src => src.services.Select(s => s.Id)));
            CreateMap<ServiceDTO, Service>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Service, ServiceDTO>();
            CreateMap<SaleDTO, Sale>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.service, opt => opt.Ignore()) // Manejamos esto manualmente
                .ForMember(dest => dest.package, opt => opt.Ignore()); // Manejamos esto manualmente
            CreateMap<Sale, SaleDTO>()
                .ForMember(dest => dest.serviceId,
                           opt => opt.MapFrom(src => src.service.Id))
                .ForMember(dest => dest.packageId,
                           opt => opt.MapFrom(src => src.package.Id));
        }
    }
}
