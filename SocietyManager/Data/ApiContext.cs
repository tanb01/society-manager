using Microsoft.EntityFrameworkCore;
using SocietyManager.Data.Entities;

namespace SocietyManager.Data
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
        }

        public DbSet<Society> Societies { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SocietyStudent>().HasKey(x => new { x.SocietyId, x.StudentId });
            modelBuilder.Entity<SocietyStudent>()
                .HasOne(x => x.Society)
                .WithMany(x => x.Members)
                .HasForeignKey(x => x.SocietyId);

            modelBuilder.Entity<SocietyStudent>()
                .HasOne(x => x.Student)
                .WithMany(x => x.Societies)
                .HasForeignKey(x => x.StudentId);

            modelBuilder.Entity<SocietyEvent>().HasKey(x => new { x.SocietyId, x.EventId });
            modelBuilder.Entity<SocietyEvent>()
                .HasOne(x => x.Society)
                .WithMany(x => x.Events)
                .HasForeignKey(x => x.SocietyId);

            modelBuilder.Entity<SocietyEvent>()
                .HasOne(x => x.Event)
                .WithMany(x => x.Societies)
                .HasForeignKey(x => x.EventId);

            modelBuilder.Entity<Society>(
                sb =>
                {
                    sb.Property(s => s.SocietyId).IsRequired();
                    sb.Property(s => s.Name).HasMaxLength(40).IsUnicode(false).IsRequired();
                    sb.Property(s => s.Description).HasMaxLength(200).IsUnicode(false).IsRequired();
                    sb.Property(s => s.CreationDate).IsRequired();
                });

            modelBuilder.Entity<Student>(
                sb =>
                {
                    sb.Property(s => s.StudentId).IsRequired();
                    sb.Property(s => s.FirstName).HasMaxLength(20).IsUnicode(false).IsRequired();
                    sb.Property(s => s.LastName).HasMaxLength(20).IsUnicode(false).IsRequired();
                    sb.Property(s => s.BirthDate).IsRequired();
                    sb.Property(s => s.Email).HasMaxLength(30).IsUnicode(false).IsRequired();
                });

            modelBuilder.Entity<Event>(
                eb =>
                {
                    eb.Property(e => e.EventId).IsRequired();
                    eb.Property(e => e.Name).HasMaxLength(40).IsUnicode(false).IsRequired();
                    eb.Property(e => e.EventDate).IsRequired();
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
