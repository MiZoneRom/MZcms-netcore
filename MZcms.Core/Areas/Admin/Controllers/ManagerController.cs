using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MZcms.Common.Helper;
using MZcms.Core.Framework.BaseControllers;
using MZcms.Entity.Entities;

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
        [Authorize]
        public ActionResult<object> Get()
        {
            return Json(new { managerModel = CurrentManager });
        }

    }
}