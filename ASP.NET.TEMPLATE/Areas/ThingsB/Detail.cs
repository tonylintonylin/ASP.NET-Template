using ASP.NET.TEMPLATE.Areas._Related;
using ASP.NET.TEMPLATE.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET.TEMPLATE.Areas.ThingsB
{
    public class Detail : BaseModel
    {
        #region Data

        public int Id { get; set; }

        public string Name { get; set; }
        public string Text { get; set; }
        public string Lookup { get; set; }
        public string Money { get; set; }
        public string DateTime { get; set; }
        public string Status { get; set; }
        public int? Integer { get; set; }
        public bool? Boolean { get; set; }
        public string LongText { get; set; }
        public int TotalThingsA { get; set; }

        public int? OwnerId { get; set; }
        public string OwnerAlias { get; set; }
        public string OwnerName { get; set; }

        // Related Lists
        public string Tab { get; set; } = "details";
        public _ThingsA ThingsA { get; set; } = new _ThingsA { ParentIdName = "ThingBId" };

        #endregion

        #region Handlers

        public override async Task<IActionResult> GetAsync()
        {
            var thingB = await _db.ThingB.SingleOrDefaultAsync(c => c.Id == Id);
            _mapper.Map(thingB, this);

            // Relateds

            var thingsA = await _db.ThingA.Where(o => o.ThingBId == thingB.Id).OrderByDescending(o => o.Id).Take(4).ToListAsync();
            _related.Prepare(thingsA, ThingsA, thingB.TotalThingsA, thingB.Id, thingB.Name);

            await _viewedService.Log(Id, "ThingB", thingB.Name);

            return View(this);
        }

        #endregion

        #region Mapping

        public class MapperProfile : BaseProfile
        {
            public MapperProfile()
            {
                CreateMap<ThingB, Detail>()
                  .Map(dest => dest.DateTime, opt => opt.MapFrom(src => src.DateTime.ToDate()))
                  .Map(dest => dest.OwnerName, opt => opt.MapFrom(src => _cache.Users[src.OwnerId].Name));
            }
        }

        #endregion
    }
}
