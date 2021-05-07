using AutoMapper;
using SocietyManager.Data.Entities;
using SocietyManager.Models;

namespace SocietyManager.Data
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            this.CreateMap<Student, CreateStudentDto>().ReverseMap();
            this.CreateMap<Student, UpdateStudentDto>().ReverseMap();
            this.CreateMap<Student, GetStudentDto>();
        }
    }
}
