using AutoMapper;
using SocietyManager.Data.Entities;
using SocietyManager.Models;
using System.Linq;

namespace SocietyManager.Data
{
    public class SocietyProfile : Profile
    {
        public SocietyProfile()
        {
            this.CreateMap<Society, CreateSocietyDto>().ReverseMap();
            this.CreateMap<Society, UpdateSocietyDto>().ReverseMap();
            this.CreateMap<Society, GetSocietyDto>()
                .ForMember(s => s.Members, x => x.MapFrom(c => c.Members.Select(a => a.Student)))
                .ForMember(s => s.Events, x => x.MapFrom(c => c.Events.Select(a => a.Event)));
        }
    }
}
