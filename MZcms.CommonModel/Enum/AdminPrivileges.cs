using MZcms.CommonModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MZcms.CommonModel
{
    public enum AdminPrivilege
    {
        //所有权限
        /*商品*/
        [Privilege("系统", "网站设置", 2001, "/SiteSettings", "category", "manage/SiteSettings")]
        CategoryManage = 2001,
    }
}
