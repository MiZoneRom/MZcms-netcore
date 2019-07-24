using System;
using System.Collections.Generic;

namespace MZcms.Entity.Entities
{
    public partial class Managers
    {
        public Managers()
        {
            ManagerLog = new HashSet<ManagerLog>();
        }

        public long Id { get; set; }
        public long RoleId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Avatar { get; set; }
        public string RealName { get; set; }
        public string Mobile { get; set; }
        public DateTime AddDate { get; set; }

        public virtual ICollection<ManagerLog> ManagerLog { get; set; }
    }
}
