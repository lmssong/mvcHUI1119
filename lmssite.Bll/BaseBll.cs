using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using lmssite.Dal;
using AutoMapper;

namespace lmssite.Bll
{
    public abstract class BaseBll<T> where T : class, new()
    {
        public IBaseDal<T> Dal { get; set; }

        public BaseBll()
        {
            SetDal();
        }

        public abstract void SetDal();

        public bool Add(T t)
        {
            Dal.Add(t);
            return Dal.SaveChanges(); ;
        }

        public bool Update(T t)
        {
            Dal.Update(t);
            return Dal.SaveChanges();
        }

        public bool Delete(T t)
        {
            Dal.Delete(t);
            return Dal.SaveChanges();
        }

        public IQueryable<T> GetModels(Expression<Func<T, bool>> where)
        {
            return Dal.GetModels(where);
        }

        public IQueryable<T> GetModelsByPage<type>(int limit, int offset, bool isAsc, Expression<Func<T, type>> orderby, Expression<Func<T, bool>> where, out int totalCount)
        {
            return Dal.GetModelsByPage<type>(limit, offset, isAsc, orderby, where, out totalCount);
        }
    }
}
