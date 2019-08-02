using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MZcms.Common.Helper;
using MZcms.Core.Framework.BaseControllers;
using MZcms.IServices;
using MZcms.Entity.Entities;

namespace MZcms.Core.Areas.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class LoginController : BaseController
    {

        private readonly IConfiguration _configuration;
        private readonly IManagerService _manager;

        public LoginController(IConfiguration configuration, IManagerService manager)
        {
            _configuration = configuration;
            _manager = manager;
        }

        [HttpGet]
        public ActionResult<object> Get(string username, string password)
        {

            Managers managerModel = _manager.Login(username, password);

            if (managerModel == null)
            {
                return ErrorResult<int>("用户名或密码错误");
            }

            JwtTokenHelper jwtTokenUtil = new JwtTokenHelper(_configuration);
            string token = jwtTokenUtil.GetToken("admin");

            return SuccessResult<object>(new { token = token });
        }

    }
}