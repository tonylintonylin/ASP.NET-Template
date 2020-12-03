using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET.TEMPLATE.Areas._Related
{
    public class _ThingsE 
    {
        public int TotalThingsE { get; set; }
        public List<_ThingE> ThingsE { get; set; } = new List<_ThingE>();
        public int ParentId { get; set; }
        public string ParentIdName { get; set; }
        public string ParentName { get; set; }
    }

    public class _ThingE
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lookup { get; set; }
        public string Money { get; set; }
        public string Date { get; set; }
    }
}
