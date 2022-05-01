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
        private IGenericRepository<Skils> _skils;
        private IGenericRepository<PersonalSkils> _personalSkils;
        public UnitOfWork(DatabaseContext context)
        {
            this._context = context;
        }
        public IGenericRepository<ApiUser> Users => _users ??= new GenericRepository<ApiUser>(_context);

        public IGenericRepository<PersonalDetails> PersonalDetails => _personalDetails ??= new GenericRepository<PersonalDetails>(_context);

        public IGenericRepository<Skils> Skils => _skils ??= new GenericRepository<Skils>(_context);
        public IGenericRepository<PersonalSkils> PersonalSkils => _personalSkils ??= new GenericRepository<PersonalSkils>(_context);

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
