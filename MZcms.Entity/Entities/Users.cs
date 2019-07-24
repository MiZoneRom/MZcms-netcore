using System;
using System.Collections.Generic;

namespace MZcms.Entity.Entities
{
    public partial class Users
    {
        public long Id { get; set; }
        public long GroupId { get; set; }
        public string UserName { get; set; }
        public string Salt { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public string NickName { get; set; }
        public bool IsMale { get; set; }
        public DateTime? Birthday { get; set; }
        public string Area { get; set; }
        public string Address { get; set; }
        public string Qq { get; set; }
        public decimal? Amount { get; set; }
        public int? Point { get; set; }
        public int? Exp { get; set; }
        public int? Status { get; set; }
        public DateTime RegDate { get; set; }
        public string RegIp { get; set; }
        public DateTime? LastLoginDate { get; set; }

        public virtual UserGroups Group { get; set; }
    }
}
