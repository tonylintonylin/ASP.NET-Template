using ASP.NET.TEMPLATE.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET.TEMPLATE.Areas.Home
{
    public class List : PagedModel
    {
        #region Data

        public int BronzeCount { get; set; }
        public string BronzeAmount { get; set; }

        public int SilverCount { get; set; }
        public string SilverAmount { get; set; }

        public int GoldCount { get; set; }
        public string GoldAmount { get; set; }

        public int PlatinumCount { get; set; }
        public string PlatinumAmount { get; set; }

        public string BarChartLabels { get; set; }
        public string BarChartValues { get; set; }

        public string DealsByStage { get; set; }
        public string PieChartValues { get; set; }

        public List<_ThingA> ThingsA { get; } = new List<_ThingA>();
        public List<_Viewed> Vieweds { get; } = new List<_Viewed>();

        #endregion

        #region Handlers

        private Dictionary<string, Dictionary<string, int>> buckets = new Dictionary<string, Dictionary<string, int>>();

        public override async Task<IActionResult> GetAsync()
        {
            var thingsB = await _db.ThingB.ToListAsync();

            BronzeCount = thingsB.Count(o => o.Status == "Bronze");
            BronzeAmount = thingsB.Where(o => o.Status == "Bronze").Sum(o => o.Money).ToCurrency();

            SilverCount = thingsB.Count(o => o.Status == "Silver");
            SilverAmount = thingsB.Where(o => o.Status == "Silver").Sum(o => o.Money).ToCurrency();

            GoldCount = thingsB.Count(o => o.Status == "Gold");
            GoldAmount = thingsB.Where(o => o.Status == "Gold").Sum(o => o.Money).ToCurrency();

            PlatinumCount = thingsB.Count(o => o.Status == "Platinum");
            PlatinumAmount = thingsB.Where(o => o.Status == "Platinum").Sum(o => o.Money).ToCurrency();


            // Recently viewed records
            var viewed = _db.Viewed.Where(v => v.UserId == _currentUser.Id)
                                   .OrderByDescending(v => v.ViewDate)
                                   .Take(6);

            _mapper.Map(viewed, Vieweds);

            // Top Things A
            var thingsA = await _db.ThingA.OrderByDescending(o => o.Money)
                                            .Take(5).ToListAsync();

            _mapper.Map(thingsA, ThingsA);

            return View(this);
        }

        #endregion

        #region Mapping

        public class MapperProfile : BaseProfile
        {
            public MapperProfile()
            {
                CreateMap<Viewed, _Viewed>()
                    .Map(dest => dest.Url, opt => opt.MapFrom(src => _cache.MetaTypes[src.WhatType].Url + "/" + src.WhatId))
                    .Map(dest => dest.Icon, opt => opt.MapFrom(src => _cache.MetaTypes[src.WhatType].Icon))
                    .Map(dest => dest.Name, opt => opt.MapFrom(src => src.WhatName));

                CreateMap<ThingA, _ThingA>()
                    .Map(dest => dest.Money, opt => opt.MapFrom(src => src.Money.ToCurrency()))
                    .Map(dest => dest.DateTime, opt => opt.MapFrom(src => src.DateTime.ToDate()));
            }
        }

        #endregion
    }
}
