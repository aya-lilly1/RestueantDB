

using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace RestueantDB.Service
{
    public class UserServices : IUserServices
    {
        private IHttpContextAccessor _httpContext;
        public UserServices(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        public string GetUserId()
        {
            return _httpContext.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        public bool IsAuthenticated()
        {
            return _httpContext.HttpContext.User.Identity.IsAuthenticated;
        }
    }
}
