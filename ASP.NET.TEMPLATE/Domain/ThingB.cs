using System;
using System.Collections.Generic;

namespace ASP.NET.TEMPLATE.Domain
{
    public partial class ThingB : IAuditable
    {
        public ThingB()
        {
            ThingA = new HashSet<ThingA>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string Lookup { get; set; }
        public decimal? Money { get; set; }
        public DateTime? DateTime { get; set; }
        public string Status { get; set; }
        public int? Integer { get; set; }
        public bool? Boolean { get; set; }
        public string LongText { get; set; }
        public int TotalThingsA { get; set; }
        public int OwnerId { get; set; }
        public string OwnerAlias { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime ChangedOn { get; set; }
        public int? ChangedBy { get; set; }

        public virtual User Owner { get; set; }
        public virtual ICollection<ThingA> ThingA { get; set; }
    }
}
