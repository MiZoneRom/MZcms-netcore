using MZcms.Model;
using System;
using System.Collections.Generic;

namespace MZcms.Entity.Entities
{
    public partial class SiteSettings : BaseModel
    {
        public long Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
