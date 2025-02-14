﻿using ASP.NET.TEMPLATE.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ASP.NET.TEMPLATE.Areas.ThingsA
{
    public class Edit : BaseModel
    {
        #region Data

        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public int? ThingBId { get; set; }
        public string ThingBName { get; set; }
        public int? ThingCId { get; set; }
        public string ThingCName { get; set; }

        public string Text { get; set; }
        public string Lookup { get; set; }
        public string Status { get; set; }
        public decimal? Money { get; set; }
        public string DateTime { get; set; }
        public int? Integer { get; set; }
        public bool? Boolean { get; set; }
        public string LongText { get; set; }

        public int? OwnerId { get; set; }
        public string OwnerAlias { get; set; }
        public string OwnerName { get; set; }

        #endregion

        #region Handlers

        public override async Task<IActionResult> GetAsync()
        {
            if (Id == 0)
            {
                OwnerId = _currentUser.Id;
                OwnerName = _currentUser.Name;
                ThingBId = HttpContext.Request.Query["ThingBId"].GetId();
                ThingBName = HttpContext.Request.Query["ThingBName"];
                ThingCId = HttpContext.Request.Query["ThingCId"].GetId();
                ThingCName = HttpContext.Request.Query["ThingCName"];
            }
            else
            {
                _mapper.Map(await _db.ThingA.SingleOrDefaultAsync(c => c.Id == Id), this);
            }

            return View(this);
        }

        public override async Task<IActionResult> PostAsync()
        {
            if (!ModelState.IsValid) return View(this);

            if (Id == 0) // new item
            {
                var thingA = _mapper.Map<ThingA>(this);

                _db.ThingA.Add(thingA);
                await _db.SaveChangesAsync();

                await SettleInsertAsync(thingA);
            }
            else
            {
                var thingA = await _db.ThingA.SingleOrDefaultAsync(c => c.Id == Id);
                var originalThingA = new OriginalThingA(thingA);

                _mapper.Map(this, thingA);

                _db.ThingA.Update(thingA);
                await _db.SaveChangesAsync();

                await SettleUpdateAsync(originalThingA, thingA);
            }

            return LocalRedirect(Referer ?? "/thingas");
        }

        #endregion

        #region Helpers

        private async Task SettleInsertAsync(ThingA thingA)
        {
            _cache.MergeThingA(thingA);

            await _rollup.RollupThingBAsync(thingA.ThingBId);
            await _rollup.RollupThingCAsync(thingA.ThingCId);
            await _rollup.RollupUserAsync(thingA.OwnerId);
        }

        private async Task SettleUpdateAsync(OriginalThingA original, ThingA thingA)
        {
            _cache.MergeThingA(thingA);

            if (original.ThingBId != thingA.ThingBId)
            {
                await _rollup.RollupThingBAsync(original.ThingBId);
                await _rollup.RollupThingBAsync(thingA.ThingBId);
            }
            if (original.ThingCId != thingA.ThingCId)
            {
                await _rollup.RollupThingCAsync(original.ThingCId);
                await _rollup.RollupThingCAsync(thingA.ThingCId);
            }
            if (original.OwnerId != thingA.OwnerId)
            {
                await _rollup.RollupUserAsync(original.OwnerId);
                await _rollup.RollupUserAsync(thingA.OwnerId);
            }
        }

        private class OriginalThingA
        {
            // Used to temporarily hold a copy of the relevant fields

            public int? ThingBId { get; set; }
            public int? ThingCId { get; set; }
            public int OwnerId { get; set; }

            public OriginalThingA(ThingA thingA)
            {
                ThingBId = thingA.ThingBId;
                ThingCId = thingA.ThingCId;
                OwnerId = thingA.OwnerId;
            }
        }

        #endregion

        #region Mapping

        public class MapperProfile : BaseProfile
        {
            public MapperProfile()
            {
                CreateMap<ThingA, Edit>()
                  .Map(dest => dest.DateTime, opt => opt.MapFrom(src => src.DateTime.ToDate()))
                  .Map(dest => dest.OwnerName, opt => opt.MapFrom(src => src.OwnerId == 0 ? "" : _cache.Users[src.OwnerId].Name));

                CreateMap<Edit, ThingA>()
                  .Map(dest => dest.DateTime, opt => opt.MapFrom(src => src.DateTime.TransformToDate()))
                  .Map(dest => dest.OwnerAlias, opt => opt.MapFrom(src => _cache.Users[src.OwnerId.Value].Alias))
                  .Map(dest => dest.ThingBName, opt => opt.MapFrom(src => src.ThingBId == null ? "" : _cache.ThingsB[src.ThingBId.Value].Name))
                  .Map(dest => dest.ThingCName, opt => opt.MapFrom(src => src.ThingCId == null ? "" : _cache.ThingsB[src.ThingCId.Value].Name));
            }
        }

        #endregion
    }
}
    