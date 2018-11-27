using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace lmssite.Dal
{
   public class DalFactory
    {
        /// <summary>
        /// IOC容器
        /// </summary>
        private static IContainer _container = null;

        /// <summary>
        /// 创建IOC反转对象实例
        /// </summary>
        /// <typeparam name="IT"></typeparam>
        /// <typeparam name="OT"></typeparam>
        /// <returns></returns>
        public static OT CreateDalInstance<IT, OT>()
        {
            OT instance;
            try
            {
                if (_container == null)
                {
                    ContainerInit<IT, OT>();
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("IOC容器实例化对象异常：" + ex.Message);
            }
            using (var scope = _container.BeginLifetimeScope())
            {
                instance = scope.Resolve<OT>();
            }
            return instance;
        }

        /// <summary>
        /// IOC容器初始化
        /// </summary>
        /// <typeparam name="IT"></typeparam>
        /// <typeparam name="OT"></typeparam>
        private static void ContainerInit<IT,OT>()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<IT>().As<OT>().InstancePerLifetimeScope();
            _container = builder.Build();
        }
    }
}
