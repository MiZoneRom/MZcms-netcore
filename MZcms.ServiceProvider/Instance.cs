﻿using Autofac;
using MZcms.IServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MZcms.ServiceProvider
{
    public class Instance<T> where T : IService
    {

        public static T Create
        {
            get
            {
                var builder = new ContainerBuilder();
                IContainer container = null;
                GetServiceProviders();

                try
                {
                    T t;

                    //服务名称
                    string iserviceName = typeof(T).Name;

                    //类型全名
                    string fullName = typeof(T).FullName;

                    //命名空间
                    string namespaceName = fullName.Substring(0, fullName.LastIndexOf('.'));

                    string implementClass = ServiceProviders[namespaceName] as string;

                    if (implementClass == null)
                        throw new ApplicationException("未配置" + fullName + "的实现");

                    string nameSpace = implementClass.Split(',')[0];
                    string assembly = implementClass.Split(',')[1];
                    string implementName = iserviceName.Substring(1);
                    string className = string.Format("{0}.{1}, {2}", nameSpace, implementName, assembly);

                    Type implementType = Type.GetType(className);

                    if (implementType == null)
                        throw new NotImplementedException("未找到" + className);

                    builder.RegisterType(implementType).As<T>();
                    container = builder.Build();

                    using (var scope = container.BeginLifetimeScope())
                    {
                        t = scope.Resolve<T>();
                        return t;
                    }

                }
                catch (Exception ex)
                {
                    throw new ServiceInstacnceCreateException(typeof(T).Name + "服务实例创建失败", ex);
                }

            }

        }

        static object locker = new object();
        static Hashtable ServiceProviders = null;

        //获取服务列表
        static void GetServiceProviders()
        {
            if (ServiceProviders == null)
            {
                lock (locker)
                {
                    if (ServiceProviders == null)
                    {
                        ServiceProviders = new Hashtable();
                        ServiceProviders.Add("MZcms.IServices", "MZcms.Service" + "," + "MZcms.Service");
                    }
                }
            }
        }

    }
}
