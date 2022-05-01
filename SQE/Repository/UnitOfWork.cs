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
        private IGenericRepository<Skills> _skills;
        private IGenericRepository<PersonalSkills> _personalSkills;
        public UnitOfWork(DatabaseContext context)
        {
            this._context = context;
        }
        public IGenericRepository<ApiUser> Users => _users ??= new GenericRepository<ApiUser>(_context);

        public IGenericRepository<PersonalDetails> PersonalDetails => _personalDetails ??= new GenericRepository<PersonalDetails>(_context);

        public IGenericRepository<Skills> Skills => _skills ??= new GenericRepository<Skills>(_context);
        public IGenericRepository<PersonalSkills> PersonalSkills => _personalSkills ??= new GenericRepository<PersonalSkills>(_context);

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
