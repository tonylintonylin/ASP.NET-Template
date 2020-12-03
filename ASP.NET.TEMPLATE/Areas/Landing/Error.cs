using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ASP.NET.TEMPLATE.Areas.Landing
{
    public class Error : BaseModel
    {
        #region Handlers

        public override IActionResult Get()
        {
            try 
            { 
                return View(); 
            }
            catch 
            { 
                return LocalRedirect("/error.htm"); 
            }
        }

        #endregion
    }
}
