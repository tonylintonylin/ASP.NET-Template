﻿using System;
using System.Collections.Generic;

namespace ASP.NET.TEMPLATE.Domain
{
    public partial class ThingA : IAuditable
    {
        public ThingA()
        {
            ThingE = new HashSet<ThingE>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? ThingBId { get; set; }
        public string ThingBName { get; set; }
        public int? ThingCId { get; set; }
        public string ThingCName { get; set; }
        public string Text { get; set; }
        public string Lookup { get; set; }
        public decimal? Money { get; set; }
        public DateTime? DateTime { get; set; }
        public string Status { get; set; }
        public int? Integer { get; set; }
        public bool? Boolean { get; set; }
        public string LongText { get; set; }
        
        public int TotalThingsE { get; set; }
        public DateTime CreatedDate { get; set; }
        public int OwnerId { get; set; }
        public string OwnerAlias { get; set; }

        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime ChangedOn { get; set; }
        public int? ChangedBy { get; set; }

        public virtual User Owner { get; set; }
        public virtual ThingB ThingB { get; set; }
        public virtual ThingC ThingC { get; set; }
        public virtual ICollection<ThingE> ThingE { get; set; }
    }
}
