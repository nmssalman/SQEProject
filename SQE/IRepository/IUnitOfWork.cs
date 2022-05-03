using SQE.Data;
using SQE.Models;
using System;
using System.Threading.Tasks;

namespace SQE.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<ApiUser> Users { get; }
        IGenericRepository<PersonalDetails> PersonalDetails { get; }
        IGenericRepository<Skills> Skills { get; }
        IGenericRepository<PersonalSkills> PersonalSkills { get; }
        IGenericRepository<Education> Educations { get; }
        IGenericRepository<Experience> Experiences { get; }
        IGenericRepository<UserProfilePicture> ProfilePicture { get; }
        Task Save();
    }
}
