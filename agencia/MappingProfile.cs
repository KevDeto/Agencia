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
            CreateMap<PersonDTO, Person>();
            CreateMap<Person, PersonDTO>();
        }
    }
}
