using System;
using System.Collections.Generic;

namespace MZcms.Entity.Entities
{
    public partial class ManagersRoles
    {
        public ManagersRoles()
        {
            ManagerPrivileges = new HashSet<ManagerPrivileges>();
        }

        public long Id { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ManagerPrivileges> ManagerPrivileges { get; set; }
    }
}
