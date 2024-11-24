using System.Net.Http.Headers;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Multi_Tenancy_Task.Data;

namespace Multi_Tenancy_Task.Services
{
    public class TenantService 
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TenantService(IHttpContextAccessor httpContextAccessor
            )
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetTenantIdFromHeader()
        {
            var tenantId = _httpContextAccessor.HttpContext?.Request.Headers["TenantId"].FirstOrDefault();
            //var tenantId1 = GetValues("TenantId");

            if (tenantId is null)   throw new Exception("Tenant Id doesn't exist");
            return tenantId;// ?? "dadad" ;
        }

    }
}
