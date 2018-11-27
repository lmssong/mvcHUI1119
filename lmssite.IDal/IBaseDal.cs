using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace lmssite.Dal
{
   public interface IBaseDal<T> where T:class,new()
    {
        void Add(T t);

        void Update(T t);

        void Delete(T t);

        IQueryable<T> GetModels(Expression<Func<T,bool>> where);

        IQueryable<T> GetModelsByPage<type>(int limit, int offset, bool isAsc, Expression<Func<T, type>> orderby,Expression<Func<T,bool>> where, out int totalCount);

        bool SaveChanges();

    }
}
