using System;
using System.Collections.Generic;

namespace MZcms.Entity.Entities
{
    public partial class ManagerLog
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string ActionType { get; set; }
        public string Remark { get; set; }
        public string UserIp { get; set; }
        public DateTime AddDate { get; set; }

        public virtual Managers User { get; set; }
    }
}
