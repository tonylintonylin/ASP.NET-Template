using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET.TEMPLATE.Areas._Related
{
    public class _ThingsC 
    {
        public int TotalThingsC { get; set; }
        public List<_ThingC> ThingsC { get; set; } = new List<_ThingC>();
        public int ParentId { get; set; }
        public string ParentIdName { get; set; }
        public string ParentName { get; set; }
    }

    public class _ThingC
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? Boolean { get; set; }
        public string Status { get; set; }
        public string Money { get; set; }
        public int TotalThingsA { get; set; }

    }
}
