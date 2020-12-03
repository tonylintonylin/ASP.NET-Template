using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET.TEMPLATE.Areas._Related
{
    public class _ThingsA 
    {
        public int TotalThingsA { get; set; }
        public List<_ThingA> ThingsA { get; set; } = new List<_ThingA>();
        public int ParentId { get; set; }
        public string ParentIdName { get; set; }
        public string ParentName { get; set; }
    }

    public class _ThingA
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lookup { get; set; }
        public string Status { get; set; }
        public int? Integer { get; set; }
        public int TotalThingsE { get; set; }
    }
}
