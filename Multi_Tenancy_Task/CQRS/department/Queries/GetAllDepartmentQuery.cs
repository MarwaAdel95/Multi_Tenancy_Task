using MediatR;
using Multi_Tenancy_Task.CQRS.Tenancy.Queries;
using Multi_Tenancy_Task.Entities;
using Multi_Tenancy_Task.Repositories;
using Multi_Tenancy_Task.Services;

namespace Multi_Tenancy_Task.CQRS.department.Queries
{
    public record GetAllDepartmentQuery() : IRequest<IEnumerable<Department>>;
    public class GetAllDepartmentQueryHandler : IRequestHandler<GetAllDepartmentQuery, IEnumerable<Department>>
    {
        private readonly IBaseRepository<Department> _repository;
        private readonly IMediator _mediator;
        private readonly TenantService _tenantService;
        public GetAllDepartmentQueryHandler(IBaseRepository<Department> repository,
            IMediator mediator,
            TenantService tenantService)
        {
            _repository = repository;
            _mediator = mediator;
            _tenantService = tenantService;
        }
        public async Task<IEnumerable<Department>> Handle(GetAllDepartmentQuery request, CancellationToken cancellationToken)
        {
            var tenantId = _tenantService.GetTenantIdFromHeader();
            var tenantIdExist = await _mediator.Send(new CheckTenantIdExistsQuery(tenantId));
            if (!tenantIdExist)
            {
                throw new Exception("Invalid Tenant Id");
            }

            var departments = _repository.Get(d=>d.TenantId== tenantId).ToList();
            //var departments = _repository.GetAll().ToList();
            if (departments.Count() == 0)
            {
                throw new Exception("No Departments in this Tenant");
            }
            return departments;
        }
    }
}
