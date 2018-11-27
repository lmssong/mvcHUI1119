using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Runtime.Remoting.Messaging;
using lmssite.Model;


namespace lmssite.Dal
{
   public class DbContextFactory
    {
        /// <summary>
        /// 创建唯一EF上下文对象，存在就获取，不存在就创建
        /// </summary>
        /// <returns></returns>
        public static DbContext Create()
        {
            DbContext dbContext = CallContext.GetData("DbContext") as DbContext;
            if (dbContext == null) {
                dbContext = new LmsSiteEntities();
                CallContext.SetData("DbContext", dbContext);
            }
            return dbContext;

        }
    }
}
