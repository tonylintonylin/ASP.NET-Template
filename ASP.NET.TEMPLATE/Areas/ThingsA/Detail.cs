using ASP.NET.TEMPLATE.Areas._Related;
using ASP.NET.TEMPLATE.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET.TEMPLATE.Areas.ThingsA
{
    public class Detail : BaseModel
    {
        #region Data

        public int Id { get; set; }
        public string Name { get; set; }

        public int? ThingBId { get; set; }
        public string ThingBName { get; set; }
        public int? ThingCId { get; set; }
        public string ThingCName { get; set; }

        public string Text { get; set; }
        public string Lookup { get; set; }
        public string Money { get; set; }
        public string DateTime { get; set; }
        public string Status { get; set; }
        public int? Integer { get; set; }
        public bool? Boolean { get; set; }
        public string LongText { get; set; }
        public int TotalThingsE { get; set; }

        public int? OwnerId { get; set; }
        public string OwnerAlias { get; set; }
        public string OwnerName { get; set; }

        // Related Lists
        public string Tab { get; set; } = "details";
        public _ThingsE ThingsE { get; set; } = new _ThingsE { ParentIdName = "ThingAId" };

        #endregion

        #region Handlers

        public override async Task<IActionResult> GetAsync()
        {
            var thingA = await _db.ThingA.SingleOrDefaultAsync(c => c.Id == Id);
            _mapper.Map(thingA, this);

            // Relateds

            var thingsE = await _db.ThingE.Where(o => o.ThingAId == thingA.Id)
                                   .OrderByDescending(o => o.Id).Take(4).ToListAsync();
            _related.Prepare(thingsE, ThingsE, thingA.TotalThingsE, thingA.Id, thingA.Name);

            await _viewedService.Log(Id, "ThingA", thingA.Name);

            return View(this);
        }

        #endregion

        #region Helpers

        #endregion

        #region Mapping

        public class MapperProfile : BaseProfile
        {
            public MapperProfile()
            {
                CreateMap<ThingA, Detail>()
                  .Map(dest => dest.Money, opt => opt.MapFrom(src => src.Money.ToCurrency()))
                  .Map(dest => dest.DateTime, opt => opt.MapFrom(src => src.DateTime.ToDate()))
                  .Map(dest => dest.OwnerName, opt => opt.MapFrom(src => _cache.Users[src.OwnerId].Name));
            }
        }

        #endregion
    }
}
