using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET.TEMPLATE.Areas._Related
{
    public class _ThingsB 
    {
        public int TotalThingsB { get; set; }
        public List<_ThingB> ThingsB { get; set; } = new List<_ThingB>();
        public int ParentId { get; set; }
        public string ParentIdName { get; set; }
        public string ParentName { get; set; }
    }

    public class _ThingB
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lookup { get; set; }
        public string Money { get; set; }
        public string Status { get; set; }
        public int TotalThingsA { get; set; }

    }
}
