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
        private IGenericRepository<Education> _education;
        private IGenericRepository<Experience> _experience;
        private IGenericRepository<UserProfilePicture> _profilePicture;
        public UnitOfWork(DatabaseContext context)
        {
            this._context = context;
        }
        public IGenericRepository<ApiUser> Users => _users ??= new GenericRepository<ApiUser>(_context);

        public IGenericRepository<PersonalDetails> PersonalDetails => _personalDetails ??= new GenericRepository<PersonalDetails>(_context);

        public IGenericRepository<Skills> Skills => _skills ??= new GenericRepository<Skills>(_context);
        public IGenericRepository<PersonalSkills> PersonalSkills => _personalSkills ??= new GenericRepository<PersonalSkills>(_context);

        public IGenericRepository<Education> Educations => _education ??= new GenericRepository<Education>(_context);

        public IGenericRepository<Experience> Experiences => _experience ??= new GenericRepository<Experience>(_context);

        public IGenericRepository<UserProfilePicture> ProfilePicture => _profilePicture ??=  new GenericRepository<UserProfilePicture>(_context);

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
