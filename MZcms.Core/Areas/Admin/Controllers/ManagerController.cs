using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MZcms.Common.Helper;
using MZcms.Core.Framework.BaseControllers;

namespace MZcms.Core.Areas.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class ManagerController : BaseController
    {

        private readonly IConfiguration _configuration;

        public ManagerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult<object> Get(string name)
        {

            JwtTokenHelper jwtTokenUtil = new JwtTokenHelper(_configuration);
            string token = jwtTokenUtil.GetToken("admin");

            return Json(new { token = token });
        }

    }
}