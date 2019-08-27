using System.Collections.Generic;
using System.Linq;

namespace MZcms.CommonModel
{
    public class Privileges
    {
        public Privileges()
        {
            Privilege = new List<GroupActionItem>();
        }
        public List<GroupActionItem> Privilege { get; set; }
    }

    /// <summary>
    /// 分组
    /// </summary>
    public class GroupActionItem
    {
        public GroupActionItem()
        {
            Children = new List<ActionItem>();
        }
        /// <summary>
        ///路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 组件
        /// </summary>
        public string Component { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { set; get; }
        public List<ActionItem> Children { get; set; }
    }

    public class ActionItem
    {
        public ActionItem()
        {
            Controllers = new List<Controllers>();
        }
        public string Name { get; set; }
        public string Path { get; set; }
        public List<Controllers> Controllers { set; get; }
        public int PrivilegeId { get; set; }

        public MZcms.CommonModel.AdminCatalogType Type { get; set; }

        /// <summary>
        /// 链接打开方式，blank,parent,self,top
        /// </summary>
        public string LinkTarget { get; set; }
    }
    public class Controllers
    {
        public Controllers()
        {
            ActionNames = new List<string>();
        }
        public string ControllerName { set; get; }
        public List<string> ActionNames { set; get; }
    }
}
