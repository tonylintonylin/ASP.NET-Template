using ASP.NET.TEMPLATE.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ASP.NET.TEMPLATE.Areas.ThingsE
{
    public class Delete : BaseModel
    {
        #region Data

        public int Id { get; set; }

        #endregion

        #region Handlers

        public override async Task<IActionResult> PostAsync()
        {
            var thingE = await _db.ThingE.SingleOrDefaultAsync(c => c.Id == Id);

            _db.ThingE.Remove(thingE);
            await _db.SaveChangesAsync();

            await SettleDeleteAsync(thingE);

            return Json(true);
        }

        #endregion

        #region Helpers

        private async Task SettleDeleteAsync(ThingE thingE)
        {
            _cache.DeleteThingE(thingE);
            await _db.Database.ExecuteSqlInterpolatedAsync(
                $"DELETE FROM Viewed WHERE WhatId = {thingE.Id} AND WhatType = 'ThingE';");

            await _rollup.RollupThingAAsync(thingE.ThingAId);
            await _rollup.RollupThingDAsync(thingE.ThingDId);
            await _rollup.RollupUserAsync(thingE.OwnerId);
        }

        #endregion
    }
}
