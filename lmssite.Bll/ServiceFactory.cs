using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;

namespace lmssite.Bll
{
   public class ServiceFactory
    {
        /// <summary>
        /// IOC容器
        /// </summary>
        private static IContainer _container = null;

        /// <summary>
        /// 获取服务对象实例
        /// </summary>
        /// <typeparam name="IT"></typeparam>
        /// <typeparam name="OT"></typeparam>
        /// <returns></returns>
        public static OT GetService<IT, OT>()
        {

            OT service;
            try
            {
                if (_container == null)
                {
                    ContainerInit<IT, OT>();
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("IOC容器创建服务对象异常：" + ex.Message);
            }
            using (var scope = _container.BeginLifetimeScope())
            {
                service = scope.Resolve<OT>();
            }
            return service;
        }

        /// <summary>
        /// IOC容器初始化
        /// </summary>
        /// <typeparam name="IT"></typeparam>
        /// <typeparam name="OT"></typeparam>
        public static void ContainerInit<IT,OT>()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<IT>().As<OT>().InstancePerLifetimeScope();
            _container = builder.Build();
        }

        public static DES Mapping<SRC, DES>(SRC sourceObject)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<SRC, DES>());
            var mapper = config.CreateMapper();
            DES desObject = mapper.Map<DES>(sourceObject);
            return desObject;
        }

        public static EditModel NewForEdit()
        {
            EditModel model = new EditModel();
            model.IsNew = true;
            return model;
        }
    }
}
