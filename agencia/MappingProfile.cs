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
        }
    }
}
