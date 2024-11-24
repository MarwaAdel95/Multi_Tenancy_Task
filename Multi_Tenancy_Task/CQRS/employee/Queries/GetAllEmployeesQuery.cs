using MediatR;
using Multi_Tenancy_Task.CQRS.Tenancy.Queries;
using Multi_Tenancy_Task.Entities;
using Multi_Tenancy_Task.Repositories;
using Multi_Tenancy_Task.Services;

namespace Multi_Tenancy_Task.CQRS.employee.Queries
{
    public record GetAllEmployeesQuery() : IRequest<IEnumerable<Employee>>;
    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, IEnumerable<Employee>>
    {
        private readonly IBaseRepository<Employee> _repository;
        private readonly IMediator _mediator;
        private readonly TenantService _tenantService;
        public GetAllEmployeesQueryHandler(IBaseRepository<Employee> repository,
            IMediator mediator,
            TenantService tenantService)
        {
            _repository = repository;
            _mediator = mediator;
            _tenantService = tenantService;
        }
        public async Task<IEnumerable<Employee>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var tenantId = _tenantService.GetTenantIdFromHeader();
            var tenantIdExist = await _mediator.Send(new CheckTenantIdExistsQuery(tenantId));
            if (!tenantIdExist)
            {
                throw new Exception("Invalid Tenant Id");
            }

           // if tenantid doesnt exist ==> create new one

            var employees = _repository.GetAll().ToList();
            if (employees.Count() == 0)
            {
                throw new Exception("No Employees in this Tenant");
            }
            return employees;
        }
    }
}
