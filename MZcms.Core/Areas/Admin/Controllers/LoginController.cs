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
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

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

            JwtTokenHelper jwtTokenHelper = new JwtTokenHelper();

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, managerModel.UserName),
                new Claim(ClaimTypes.Role, managerModel.RoleId.ToString()),
                new Claim(JwtRegisteredClaimNames.Sid, managerModel.Id.ToString()),
            };

            string token = jwtTokenHelper.GetToken(claims);
            string refreshToken = jwtTokenHelper.RefreshToken();

            _manager.AddRefeshToken(token, refreshToken, managerModel.Id, 1);

            return SuccessResult<object>(new { token = token, refreshToken = refreshToken, userName = managerModel.UserName });
        }

        public ActionResult<object> RefreshToken(string accessToken, string refreshToken)
        {

            if (CurrentManager == null)
            {
                return ErrorResult<int>("用户登录过期");
            }

            return ErrorResult<int>("用户登录过期");

        }

    }
}