using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET.TEMPLATE.Areas._Related
{
    public class _ThingsD 
    {
        public int TotalThingsD { get; set; }
        public List<_ThingD> ThingsD { get; set; } = new List<_ThingD>();
        public int ParentId { get; set; }
        public string ParentIdName { get; set; }
        public string ParentName { get; set; }
    }

    public class _ThingD
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Integer { get; set; }
        public string Status { get; set; }
        public string Lookup { get; set; }
        public int TotalThingsE { get; set; }

    }
}
