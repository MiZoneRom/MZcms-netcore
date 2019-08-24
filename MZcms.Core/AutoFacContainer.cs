using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Autofac;
using MZcms.Core;
using Autofac.Configuration;
using MZcms.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.IO;

namespace MZcms.Core
{
    public class AutoFacContainer : IinjectContainer
    {
		#region 字段
		private ContainerBuilder builder;
		private IContainer container;
		#endregion

		#region 构造函数
		public AutoFacContainer()
		{
			builder = new ContainerBuilder();
			SetupResolveRules(builder);  //注入
			container = builder.Build();
		}
		#endregion

		#region IinjectContainer 成员
		public void RegisterType<T>()
		{
			builder.RegisterType<T>();
		}

		public T Resolve<T>()
		{
			return container.Resolve<T>();
		}

		public object Resolve(Type type)
		{
			return container.Resolve(type);
		}
		#endregion

		#region 私有方法
		private void SetupResolveRules(ContainerBuilder builder)
		{
			var services = Assembly.Load("MZcms.Service");
			builder.RegisterAssemblyTypes(services).Where(t => t.GetInterface(typeof(MZcms.IServices.IService).Name)!=null).AsImplementedInterfaces().InstancePerLifetimeScope();
            ConfigurationBuilder configBuild = new ConfigurationBuilder();
            configBuild.SetBasePath(Directory.GetCurrentDirectory());
            configBuild.Add(new JsonConfigurationSource { Path = "autofac.json", ReloadOnChange = true });
            IConfigurationRoot config = configBuild.Build();
            ConfigurationModule module = new ConfigurationModule(config);
            builder.RegisterModule(module);
		}
		#endregion
	}
}