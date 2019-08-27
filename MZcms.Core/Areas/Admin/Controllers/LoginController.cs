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
            var jwtSection = _configuration.GetSection("jwt");
            int tokenExpires = Convert.ToInt32(jwtSection.GetSection("TokenExpires").Value);
            int refreshTokenExpires = Convert.ToInt32(jwtSection.GetSection("RefreshTokenExpires").Value);

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
            string tokenExpired = StringHelper.GetTimeStamp(DateTime.UtcNow.AddMinutes(tokenExpires));
            string refreshToeknExpired = StringHelper.GetTimeStamp(DateTime.UtcNow.AddMinutes(refreshTokenExpires));

            _manager.AddRefeshToken(token, refreshToken, managerModel.Id, refreshTokenExpires);

            return SuccessResult<object>(new { token = token, refreshToken = refreshToken, userName = managerModel.UserName, expires = tokenExpired, refreshExpires = refreshToeknExpired });
        }

        [HttpPost("RefreshToken")]
        public ActionResult<object> RefreshToken([FromBody] TokenModel entity)
        {
            Managers managerModel = CurrentManager;

            if (managerModel == null)
            {
                return ErrorResult<int>("用户登录过期");
            }

            ManagerToken tokenModel = _manager.GetToken(managerModel.Id);

            if (tokenModel == null)
            {
                return ErrorResult<int>("认证过期");
            }

            _manager.RemoveToken(managerModel.Id);

            JwtTokenHelper jwtTokenHelper = new JwtTokenHelper();

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, managerModel.UserName),
                new Claim(ClaimTypes.Role, managerModel.RoleId.ToString()),
                new Claim(JwtRegisteredClaimNames.Sid, managerModel.Id.ToString()),
            };

            string newToken = jwtTokenHelper.GetToken(claims);
            string newRefreshToken = jwtTokenHelper.RefreshToken();

            _manager.AddRefeshToken(newToken, newRefreshToken, managerModel.Id, 60);

            return SuccessResult<object>(new { token = newToken, refreshToken = newRefreshToken, userName = managerModel.UserName });

        }

        public class TokenModel
        {
            string token { get; set; }
            string refresh_token { get; set; }
        }

    }
}