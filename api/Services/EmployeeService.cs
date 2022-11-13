using System.Security.Claims;

namespace api.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EmployeeService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetMyName()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }
            return result;
        }
    }
}
