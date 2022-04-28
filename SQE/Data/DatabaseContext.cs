using Microsoft.EntityFrameworkCore;
using SQE.Models;

namespace SQE.Data
{
    public class DatabaseContext : DbContext
    {
        public  DatabaseContext(DbContextOptions option) : base(option)
        {

        }
        public DbSet<UserDOT> userDOTs { get; set; }
    }
}
