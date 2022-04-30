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
        private IGenericRepository<PersonalDetails> _personalDetails;
        public UnitOfWork(DatabaseContext context)
        {
            this._context = context;
        }
        public IGenericRepository<ApiUser> Users => _users ??= new GenericRepository<ApiUser>(_context);

        public IGenericRepository<PersonalDetails> PersonalDetails => _personalDetails ??= new GenericRepository<PersonalDetails>(_context);

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
