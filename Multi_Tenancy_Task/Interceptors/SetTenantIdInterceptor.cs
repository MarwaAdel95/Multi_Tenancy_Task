using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Multi_Tenancy_Task.Entities;
using Multi_Tenancy_Task.Services;

namespace Multi_Tenancy_Task.Interceptors
{
    public class SetTenantIdInterceptor :SaveChangesInterceptor
    {
        private readonly string TenantId;
        public SetTenantIdInterceptor(string tenantId)
        {
            TenantId = tenantId;
        }
        public override InterceptionResult<int> SavingChanges(
            DbContextEventData eventData,
            InterceptionResult<int> result)
        {
            if (eventData.Context is null) return result;

            var entries = eventData.Context.ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                //if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                if(entry.Entity is EntityBase entity)
                {
                    entity.TenantId = TenantId;
                }

            }
            return result;
        }
       
    }
}
