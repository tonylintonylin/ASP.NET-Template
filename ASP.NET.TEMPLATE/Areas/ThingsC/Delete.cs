using ASP.NET.TEMPLATE.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ASP.NET.TEMPLATE.Areas.ThingsC
{
    public class Delete : BaseModel
    {
        #region Data

        public int Id { get; set; }

        #endregion

        #region Handlers

        public override async Task<IActionResult> PostAsync()
        {
            var thingC = await _db.ThingC.SingleOrDefaultAsync(c => c.Id == Id);

            _db.ThingC.Remove(thingC);
            await _db.SaveChangesAsync();

            await SettleDeleteAsync(thingC);

            return Json(true);
        }

        #endregion

        #region Helpers

        private async Task SettleDeleteAsync(ThingC thingC)
        {
            _cache.DeleteThingC(thingC);
            await _db.Database.ExecuteSqlInterpolatedAsync(
                     $"DELETE FROM Viewed WHERE WhatId = {thingC.Id} AND WhatType = 'ThingC';");

            await _rollup.RollupUserAsync(thingC.OwnerId);
        }

        #endregion
    }
}
