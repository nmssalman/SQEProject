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
        Task Save();
    }
}
