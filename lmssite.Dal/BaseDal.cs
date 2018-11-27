using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace lmssite.Dal
{
    public  class BaseDal<T> where T :class,new()
    {

        /// <summary>
        /// DB上下文对象
        /// </summary>
        private DbContext _dbc = DbContextFactory.Create();

        public void Add(T t)
        {
            _dbc.Set<T>().Add(t);
        }

        public void Update(T t)
        {
            _dbc.Set<T>().AddOrUpdate(t);
        }

        public void Delete(T t)
        {
            _dbc.Set<T>().Remove(t);
        }



        public IQueryable<T> GetModels(Expression<Func<T, bool>> where)
        {
            return _dbc.Set<T>().Where(where);
        }

        public IQueryable<T> GetModelsByPage<type>(int limit, int offset, bool isAsc, Expression<Func<T, type>> orderby, Expression<Func<T, bool>> where,out int totalCount)
        {
            
            if (isAsc)
            {
                totalCount = _dbc.Set<T>().Where(where).OrderBy(orderby).Count();
                return _dbc.Set<T>().Where(where).OrderBy(orderby).Skip(offset).Take(limit);
            }
            else
            {
                totalCount = _dbc.Set<T>().Where(where).OrderByDescending(orderby).Count();
                return _dbc.Set<T>().Where(where).OrderByDescending(orderby).Skip(offset).Take(limit);
            }
        }

        public bool SaveChanges()
        {
            return _dbc.SaveChanges() > 0;
        }
    }
}
