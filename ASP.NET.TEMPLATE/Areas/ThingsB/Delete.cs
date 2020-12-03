using ASP.NET.TEMPLATE.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ASP.NET.TEMPLATE.Areas.ThingsB
{
    public class Delete : BaseModel
    {
        #region Data

        public int Id { get; set; }

        #endregion

        #region Handlers

        public override async Task<IActionResult> PostAsync()
        {
            var thingB = await _db.ThingB.SingleOrDefaultAsync(c => c.Id == Id);

            _db.ThingB.Remove(thingB);
            await _db.SaveChangesAsync();

            await SettleDeleteAsync(thingB);

            return Json(true);
        }

        #endregion

        #region Helpers

        private async Task SettleDeleteAsync(ThingB thingB)
        {
            _cache.DeleteThingB(thingB);
            await _db.Database.ExecuteSqlInterpolatedAsync(
                   $"DELETE FROM Viewed WHERE WhatId = {thingB.Id} AND WhatType = 'ThingB';");

            await _rollup.RollupUserAsync(thingB.OwnerId);
        }
        #endregion
    }      
}
