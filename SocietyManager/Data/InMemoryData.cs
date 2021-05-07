using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SocietyManager.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocietyManager.Data
{
    public class InMemoryData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new ApiContext(serviceProvider.GetRequiredService<DbContextOptions<ApiContext>>());
            // Verify whether data was already seeded
            if (context.Societies.Any() || context.Students.Any() || context.Events.Any())
            {
                return;
            }
            // Societies
            Society firstSociety = new Society() { SocietyId = 1, Name = "ECE International", Description = "Welcoming, guiding and accompanying international students from all over the world.", CreationDate = new DateTime(1980, 02, 11) };
            Society secondSociety = new Society() { SocietyId = 2, Name = "Club Alma", Description = "Conferences, TED talks, you want it, we make it.", CreationDate = new DateTime(2002, 06, 26) };
            Society thirdSociety = new Society() { SocietyId = 3, Name = "Good Games", Description = "Dungeons & Dragons, classic board games and much more!", CreationDate = new DateTime(2010, 09, 05) };
            Society fourthSociety = new Society() { SocietyId = 4, Name = "NGA", Description = "Weekly chill gaming, gaming tournaments, anything or everything gaming really.", CreationDate = new DateTime(2013, 07, 18) };

            // Students
            Student firstStudent = new Student() { StudentId = 1, FirstName = "Ben", LastName = "Tan", Email = "ben.tan@edu.ece.fr", BirthDate = new DateTime(2000, 01, 01) };
            Student secondStudent = new Student() { StudentId = 2, FirstName = "Camille", LastName = "Eung", Email = "cam.eung@edu.ece.fr", BirthDate = new DateTime(1998, 06, 30) };
            Student thirdStudent = new Student() { StudentId = 3, FirstName = "Noor", LastName = "Kardache", Email = "nor.kardache@edu.ece.fr", BirthDate = new DateTime(1999, 04, 28) };
            Student fourthStudent = new Student() { StudentId = 4, FirstName = "Simon", LastName = "Zhang", Email = "sim.zhang@edu.ece.fr", BirthDate = new DateTime(1999, 10, 28) };

            // Events
            Event firstEvent = new Event() { EventId = 1, Name = "Stroll along the Seine", EventDate = new DateTime(2021, 01, 16) };
            Event secondEvent = new Event() { EventId = 2, Name = "Multicultural Quiz", EventDate = new DateTime(2021, 03, 10) };
            Event thirdEvent = new Event() { EventId = 3, Name = "Super Smash Bros. Melee Tournament", EventDate = new DateTime(2021, 04, 20) };
            Event fourthEvent = new Event() { EventId = 4, Name = "TEDxECE", EventDate = new DateTime(2021, 03, 5) };

            // SocietyStudent
            firstSociety.Members = new List<SocietyStudent>()
                {
                    new SocietyStudent() { Society = firstSociety, Student = firstStudent },
                    new SocietyStudent() { Society = firstSociety, Student = secondStudent }
                };
            secondSociety.Members = new List<SocietyStudent>()
                {
                    new SocietyStudent() { Society = secondSociety, Student = firstStudent },
                };
            thirdSociety.Members = new List<SocietyStudent>()
                {
                    new SocietyStudent() { Society = thirdSociety, Student = fourthStudent },
                };
            fourthSociety.Members = new List<SocietyStudent>()
                {
                    new SocietyStudent() { Society = fourthSociety, Student = fourthStudent },
                };

            // SocietyEvent
            firstSociety.Events = new List<SocietyEvent>()
                {
                    new SocietyEvent() { Society = firstSociety, Event = firstEvent },
                    new SocietyEvent() { Society = firstSociety, Event = secondEvent },
                    new SocietyEvent() { Society = firstSociety, Event = thirdEvent }
                };
            secondSociety.Events = new List<SocietyEvent>()
                {
                    new SocietyEvent() { Society = secondSociety, Event = secondEvent },
                    new SocietyEvent() { Society = secondSociety, Event = fourthEvent },
                };
            thirdSociety.Events = new List<SocietyEvent>()
                {
                    new SocietyEvent() { Society = thirdSociety, Event = thirdEvent },
                };
            fourthSociety.Events = new List<SocietyEvent>()
                {
                    new SocietyEvent() { Society = fourthSociety, Event = thirdEvent },
                };

            context.Societies.AddRange(firstSociety, secondSociety, thirdSociety, fourthSociety);
            context.Students.AddRange(firstStudent, secondStudent, thirdStudent, fourthStudent);
            context.Events.AddRange(firstEvent, secondEvent, thirdEvent, fourthEvent);

            context.SaveChanges();
        }
    }
}
