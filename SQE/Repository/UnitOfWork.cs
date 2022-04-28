using SQE.Data;
using SQE.IRepository;
using System;
using System.Threading.Tasks;

namespace SQE.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;
        private IGenericRepository<ApiUser> _users;
        public UnitOfWork(DatabaseContext context)
        {
            this._context = context;
        }
        public IGenericRepository<ApiUser> Users => _users ??= new GenericRepository<ApiUser>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }


        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
