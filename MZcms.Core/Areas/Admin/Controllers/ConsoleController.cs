using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MZcms.Web.Framework;

namespace MZcms.Core.Areas.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class ConsoleController : Controller
    {
        [HttpGet("Navigation")]
        public ActionResult<object> Navigation()
        {
            var navs = PrivilegeHelper.AdminPrivilegesDefault.Privilege;
            return Json(new { router = navs });
        }
    }
}