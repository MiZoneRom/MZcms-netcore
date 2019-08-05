using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using MZcms.Common;
using MZcms.Core.Framework.BaseControllers;
using MZcms.IServices;

namespace MZcms.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : BaseController
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {

            //var services = new ServiceCollection();
            //var provider = services.BuildServiceProvider();
            //services.AddSingleton<IManagerService, Operation>();
            //// 自定义传入Guid空值
            //services.AddSingleton<IManagerService>(
            //  new Operation(Guid.Empty));
            //// 自定义传入一个New的Guid
            //services.AddSingleton<IManagerService>(
            //  new Operation(Guid.NewGuid()));
            //provider.GetService<IManagerService>().GetManagers("aaa");

            ServiceProvider.Instance<IManagerService>.Create.GetManagers("admin");

            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
