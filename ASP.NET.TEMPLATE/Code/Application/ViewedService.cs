using ASP.NET.TEMPLATE.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET.TEMPLATE
{
    #region Interface

    public interface IViewedService
    {
        Task Log(int whatId, string whatType, string whatName);
        int[] GetIds(string whatType);
    }

    #endregion

    public class ViewedService : IViewedService
    {
        #region Dependency Injection

        private readonly ICurrentUser _currentUser;
        private readonly UltraContext _db;

        public ViewedService(UltraContext db, ICurrentUser currentUser)
        {
            _db = db;
            _currentUser = currentUser;
        }

        #endregion

        public async Task Log(int whatId, string whatType, string whatName)
        {
            // Logs a viewed record

            if (whatId != 0)
            {
                var viewed = await _db.Viewed.SingleOrDefaultAsync(v => v.WhatId == whatId && v.WhatType == whatType && v.UserId == _currentUser.Id);
                if (viewed != null)
                {
                    viewed.ViewDate = DateTime.Now;
                    _db.Viewed.Update(viewed);
                    await _db.SaveChangesAsync();
                }
                else
                {
                    viewed = new Viewed
                    {
                        UserId = _currentUser.Id,
                        WhatId = whatId,
                        WhatType = whatType,
                        WhatName = whatName,
                        ViewDate = DateTime.Now,
                        CreatedOn = DateTime.Now,
                        ChangedOn = DateTime.Now,
                        CreatedBy = _currentUser.Id,
                        ChangedBy = _currentUser.Id
                    };

                    await _db.Viewed.AddAsync(viewed);
                    await _db.SaveChangesAsync();
                }
            }
        }

        public int[] GetIds(string whatType)
        {
            // Get most recently viewed items of a given type for currentuser

            var whatIds = _db.Viewed
                .FromSqlInterpolated($"SELECT WhatId FROM Viewed WHERE UserId = {_currentUser.Id} AND WhatType = {whatType}")
                .Select(v => v.WhatId).ToArray();

            return whatIds;
        }
    }
}
