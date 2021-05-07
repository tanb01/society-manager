using System;
using System.Collections.Generic;

namespace SocietyManager.Data.Entities
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public ICollection<SocietyStudent> Societies { get; set; }
    }
}
