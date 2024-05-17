using LLMEducation.Data.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace LLMEducation.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Content> Contents { get; set; }
        public DbSet<TestQ> TestQs { get; set; }
        public DbSet<SurveyData> SurveyData { get; set; }
        public DbSet<User> Clients { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestQ>()
                .HasOne(e => e.content)
                .WithMany()
                .HasForeignKey(e => e.contentId)
                .IsRequired();

            modelBuilder.Entity<IdentityUserLogin<string>>().HasNoKey();
            modelBuilder.Entity<IdentityUserRole<string>>().HasNoKey();
            modelBuilder.Entity<IdentityUserToken<string>>().HasNoKey();
        }
    }
}