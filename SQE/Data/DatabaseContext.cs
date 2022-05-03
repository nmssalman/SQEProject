using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SQE.Configuration.Entity;
using SQE.Models;

namespace SQE.Data
{
    public class DatabaseContext : IdentityDbContext<ApiUser>
    {
        public  DatabaseContext(DbContextOptions option) : base(option)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new RoleConfiguration());
        }
        public DbSet<PersonalDetails> PersonalDetails { get; set; }
        public DbSet<Skills> Skills { get; set; }
        public DbSet<Education> Education { get; set; }
        public DbSet<Experience> Experience { get; set; }
        public DbSet<UserProfilePicture> UserProfilePictures { get; set; }
    }
}
