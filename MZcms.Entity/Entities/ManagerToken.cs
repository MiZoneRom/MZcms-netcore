using MZcms.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MZcms.Entity.Entities
{
    public partial class ManagerToken : BaseModel
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }

        public DateTime Expires { get; set; }
    }
}
