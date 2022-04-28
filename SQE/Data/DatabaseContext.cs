using Microsoft.EntityFrameworkCore;

namespace SQE.Data
{
    public class DatabaseContext : DbContext
    {
        public  DatabaseContext(DbContextOptions option) : base(option)
        {

        }
    }
}
