using AutoMapper;
using SocietyManager.Data.Entities;
using SocietyManager.Models.Events;

namespace SocietyManager.Data
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            this.CreateMap<Event, CreateEventDto>().ReverseMap();
            this.CreateMap<Event, UpdateEventDto>().ReverseMap();
            this.CreateMap<Event, GetEventDto>();
        }
    }
}
