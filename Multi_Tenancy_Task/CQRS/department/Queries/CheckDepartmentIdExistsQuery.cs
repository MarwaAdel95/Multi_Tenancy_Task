using MediatR;
using Multi_Tenancy_Task.Entities;
using Multi_Tenancy_Task.Repositories;

namespace Multi_Tenancy_Task.CQRS.department.Queries
{
    public record CheckDepartmentIdExistsQuery(int Id,string tenantId) : IRequest<bool>;
    public class CheckDepartmentIdExistsQueryHandler : IRequestHandler<CheckDepartmentIdExistsQuery, bool>
    {
        private readonly IBaseRepository<Department> _repository;
        public CheckDepartmentIdExistsQueryHandler(IBaseRepository<Department> repository)
        {
            _repository = repository;
        }
        public async Task<bool> Handle(CheckDepartmentIdExistsQuery request, CancellationToken cancellationToken)
        {
            var DepartmentIdExists = _repository.Any(t => t.Id == request.Id && t.TenantId == request.tenantId);
            if (!DepartmentIdExists) return false;
            return true;
        }
    }
}
