using MZcms.Model;
using System;
using System.Collections.Generic;

namespace MZcms.Entity.Entities
{
    public partial class ManagerPrivileges : BaseModel
    {
        public long Id { get; set; }
        public int Privilege { get; set; }
        public long RoleId { get; set; }

        public virtual ManagersRoles Role { get; set; }
    }
}
