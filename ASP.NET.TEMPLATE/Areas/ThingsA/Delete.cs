using ASP.NET.TEMPLATE.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ASP.NET.TEMPLATE.Areas.ThingsA
{
    public class Delete : BaseModel
    {
        #region Data

        public int Id { get; set; }

        #endregion

        #region Handlers

        public override async Task<IActionResult> PostAsync()
        {
            var thingA = await _db.ThingA.SingleOrDefaultAsync(c => c.Id == Id);
            
            _db.ThingA.Remove(thingA);
            await _db.SaveChangesAsync();

            await SettleDeleteAsync(thingA);

            return Json(true);
        }

        #endregion

        #region Helpers

        private async Task SettleDeleteAsync(ThingA thingA)
        {
            _cache.DeleteThingA(thingA);
            await _db.Database.ExecuteSqlInterpolatedAsync(
                 $"DELETE FROM Viewed WHERE WhatId = {thingA.Id} AND WhatType = 'ThingA';");

            await _rollup.RollupThingBAsync(thingA.ThingBId);
            await _rollup.RollupThingCAsync(thingA.ThingCId);
            await _rollup.RollupUserAsync(thingA.OwnerId);
        }

        #endregion
    }
}
