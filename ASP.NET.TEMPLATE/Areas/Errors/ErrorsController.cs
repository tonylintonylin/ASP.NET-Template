﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ASP.NET.TEMPLATE.Areas.Errors
{
    [Authorize(Roles = "Admin")]
    [Menu("Admin")]
    [Route("admin/errors")]
    [AdminMenu("Errors")]
    public class ErrorsController : Controller
    {
        #region Pages

        [HttpGet]
        public async Task<IActionResult> List(List model) => await model.GetAsync();

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(Delete model) => await model.PostAsync();

        #endregion
    }
}
