using MZcms.Model;
using System;
using System.Collections.Generic;

namespace MZcms.Entity.Entities
{
    public partial class UserGroups : BaseModel
    {
        public UserGroups()
        {
            Users = new HashSet<Users>();
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public int? Grade { get; set; }
        public int? UpgradeExp { get; set; }
        public decimal? Amount { get; set; }
        public int? Point { get; set; }
        public bool IsDefault { get; set; }
        public bool IsUpgrade { get; set; }
        public bool IsLock { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}
