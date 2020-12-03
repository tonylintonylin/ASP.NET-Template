using AutoMapper;
using ASP.NET.TEMPLATE.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ASP.NET.TEMPLATE
{
    public abstract class BaseProfile : Profile
    {
        // Base class to all Profiles

        #region Dependency Injection

        // ** Lazy DI pattern

        private static HttpContext HttpContext => ServiceLocator.Resolve<IHttpContextAccessor>().HttpContext;

        // Singleton lifetime
        private ICurrentUser currentUser;
        protected ICurrentUser _currentUser => currentUser ??= HttpContext.RequestServices.GetService<ICurrentUser>();

        // Scoped lifetime
        protected ICache _cache => HttpContext.RequestServices.GetService<ICache>();
        protected UltraContext _db => HttpContext.RequestServices.GetService<UltraContext>();

        #endregion
    }
}
