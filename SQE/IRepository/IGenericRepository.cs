using NPOI.SS.Formula.Functions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SQE.IRepository
{
    public interface IGenericRepository <T> where T : class
    {
        Task<IList<T>> GetAll(
            Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
            List<string> includes = null
            );
        Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes = null);
        Task<T> Insert(T entity);
        Task<T> InsertRage(IEnumerable<T> entities);
        Task Delete(int id);
        Task<T> DeleteRage(IEnumerable<T> entities);
        Task<T> Update(T entity);

    }
}
