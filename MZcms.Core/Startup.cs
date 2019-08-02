﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MZcms.Common;
using MZcms.Entity.Entities;
using MZcms.IServices;
using MZcms.Service;
using NLog.Extensions.Logging;
using NLog.Web;

namespace MZcms.Core
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public static IContainer ApplicationContainer;

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            //添加jwt验证：
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,//是否验证Issuer
                        ValidateAudience = true,//是否验证Audience
                        ValidateLifetime = true,//是否验证失效时间
                        ValidateIssuerSigningKey = true,//是否验证SecurityKey
                        ValidAudience = "jwttest",//Audience
                        ValidIssuer = "jwttest",//Issuer，这两项和前面签发jwt的设置一致
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecurityKey"]))//拿到SecurityKey
                    };
                });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            ConfigDBContext(services);

            //配置跨域处理，允许所有来源：
            services.AddCors(options =>
                options.AddPolicy("Any",
                p => p.AllowAnyOrigin())
            );

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMemoryCache();

            //services.AddTransient<IManagerService, ManagerService>();

            //注入全局异常捕获
            services.AddMvc(o =>
            {
                o.Filters.Add(typeof(BaseExceptions));
            });

            //实例化 AutoFac  容器   
            var builder = new ContainerBuilder();

            var assemblys = Assembly.Load("MZcms.Service");//MZcms.Service是继承接口的实现方法类库名称
            var baseType = typeof(IService);//IService 是一个接口（所有要实现依赖注入的借口都要继承该接口）

            builder.RegisterAssemblyTypes(assemblys)
                .Where(m => baseType.IsAssignableFrom(m) && m != baseType)
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.Populate(services);
            ApplicationContainer = builder.Build();
            return new AutofacServiceProvider(ApplicationContainer);//第三方IOC接管 core内置DI容器

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {

            }

            //设置log打印
            loggerFactory.AddNLog();
            env.ConfigureNLog("Config/config_nlog.config");

            //启用验证
            app.UseAuthentication();

            //启用跨越
            app.UseCors("Any");

            app.UseMvc();

        }

        /// <summary>
        /// 注册EF服务
        /// </summary>
        /// <param name="services"></param>
        public void ConfigDBContext(IServiceCollection services)
        {
            string defaultConnectionStr = Configuration.GetConnectionString("ProductConnection");
            DbContextOptionsBuilder<EntitiesContext> option = new DbContextOptionsBuilder<EntitiesContext>();
            option.UseSqlServer(defaultConnectionStr);
            services.AddScoped<EntitiesContext>(s => new EntitiesContext(option.Options));
        }

    }
}
