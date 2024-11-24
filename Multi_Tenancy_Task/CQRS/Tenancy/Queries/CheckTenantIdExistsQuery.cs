using MediatR;
using Multi_Tenancy_Task.Entities;
using Multi_Tenancy_Task.Repositories;

namespace Multi_Tenancy_Task.CQRS.Tenancy.Queries
{
    public record CheckTenantIdExistsQuery(string Id) : IRequest<bool>;
    public class CheckTenantExistsQueryHandler : IRequestHandler<CheckTenantIdExistsQuery, bool>
    {
        private readonly IBaseRepository<Tenant> _repository;
        public CheckTenantExistsQueryHandler(IBaseRepository<Tenant> repository)
        {
            _repository = repository;
        }
        public async Task<bool> Handle(CheckTenantIdExistsQuery request, CancellationToken cancellationToken)
        {
            var TenantIdExists = _repository.Any(t => t.TenantId == request.Id);
            if (!TenantIdExists) return false;
            return true;
        }
    }
}
