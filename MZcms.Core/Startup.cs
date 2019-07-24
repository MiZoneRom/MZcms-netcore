﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MZcms.Common;
using MZcms.Entity.Entities;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            ConfigDBContext(services);

            //注入全局异常捕获
            services.AddMvc(o =>
            {
                o.Filters.Add(typeof(BaseExceptions));
            });

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

            loggerFactory.AddNLog();
            env.ConfigureNLog("Config/config_nlog.config");

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
