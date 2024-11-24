using Microsoft.EntityFrameworkCore;
using Multi_Tenancy_Task.CQRS.Tenancy.Queries;
using Multi_Tenancy_Task.Entities;
using Multi_Tenancy_Task.Interceptors;
using Multi_Tenancy_Task.Services;

namespace Multi_Tenancy_Task.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly TenantService _tenantService;
        private readonly string TenantId;
       
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, TenantService tenantService) :base(options) 
        {
            _tenantService = tenantService;
            TenantId = _tenantService?.GetTenantIdFromHeader();
        }
       
        DbSet<Employee> Employees { get; set; }
        DbSet<Department > Departments { get; set; }
        DbSet<Tenant> Tenancy { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.AddInterceptors(new SetTenantIdInterceptor(TenantId));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
                modelBuilder.Entity<Employee>().HasQueryFilter(e => e.TenantId == TenantId);
                modelBuilder.Entity<Department>().HasQueryFilter(e => e.TenantId == TenantId);
            
        }
    }
}
