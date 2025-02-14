﻿using ASP.NET.TEMPLATE.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET.TEMPLATE.Areas.Logins
{
    public class List : PagedModel<Detail>
    {
        #region Data

        public List() { Sort = "-LoginDate"; PageSize = 50; }

        #endregion

        #region Handlers

        public override async Task<IActionResult> GetAsync()
        {
            var query = BuildQuery();

            TotalRows = await query.CountAsync();
            var items = await query.Skip(Skip).Take(Take).ToListAsync();

            _mapper.Map(items, Items);

            return View(this);
        }

        #endregion

        #region Helpers

        protected IQueryable<Login> BuildQuery()
        {
            var query = _db.Login.AsQueryable();

            // Sorting

            query = Sort switch
            {
                "LoginDate" => query.OrderBy(u => u.LoginDate),
                "-LoginDate" => query.OrderByDescending(u => u.LoginDate),
                "LastName" => query.OrderBy(u => u.LastName),
                "-LastName" => query.OrderByDescending(u => u.LastName),
                "Email" => query.OrderBy(u => u.Email),
                "-Email" => query.OrderByDescending(u => u.Email),
                "IpAddress" => query.OrderBy(u => u.IpAddress),
                "-IpAddress" => query.OrderByDescending(u => u.IpAddress),
                "Result" => query.OrderBy(u => u.Result),
                "-Result" => query.OrderByDescending(u => u.Result),
                _ => query.OrderByDescending(u => u.Id),
            };

            return query;
        }

        #endregion

        #region Mapping

        public class MapperProfile : BaseProfile
        {
            public MapperProfile()
            {
                CreateMap<Login, Detail>()
                  .Map(dest => dest.LoginDate, opt => opt.MapFrom(src => src.LoginDate.ToDate()));
            }
        }

        #endregion
    }
}
