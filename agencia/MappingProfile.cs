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
        }
    }
}
