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
        IGenericRepository<Skils> Skils { get; }
        IGenericRepository<PersonalSkils> PersonalSkils { get; }
        Task Save();
    }
}
