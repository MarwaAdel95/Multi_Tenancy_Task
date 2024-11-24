using MediatR;
using Multi_Tenancy_Task.CQRS.department.Queries;
using Multi_Tenancy_Task.CQRS.Tenancy.Queries;
using Multi_Tenancy_Task.Entities;
using Multi_Tenancy_Task.Repositories;
using Multi_Tenancy_Task.Services;
using Multi_Tenancy_Task.ViewModels;

namespace Multi_Tenancy_Task.CQRS.employee.Commands
{
    public record InsertEmployeeCommand(InsertEmployeeViewModel viewModel) :IRequest<int>;
    public class InsertEmployeeCommandHandler : IRequestHandler<InsertEmployeeCommand, int>
    {
        private readonly IBaseRepository<Employee> _repository;
        private readonly IMediator _mediator;
        private readonly TenantService _tenantService;
        public InsertEmployeeCommandHandler(IBaseRepository<Employee> repository,
            IMediator mediator,
            TenantService tenantService)
        {
            _repository = repository;   
            _mediator = mediator;
            _tenantService = tenantService;
        }
        public async Task<int> Handle(InsertEmployeeCommand request, CancellationToken cancellationToken)
        {
            var departmentDto = request.viewModel;
            var tenantId = _tenantService.GetTenantIdFromHeader();
            //check of tenant id exist in db
            var tenantIdExist = await _mediator.Send(new CheckTenantIdExistsQuery(tenantId));
            if (!tenantIdExist)
            {
                throw new Exception("Invalid Tenant Id");
            }
            //check of department id exist in the same tenant
            var departmentIdExist = await _mediator.Send(new CheckDepartmentIdExistsQuery(departmentDto.DepartmentId, tenantId));
             if (!departmentIdExist)
            {
                throw new Exception("Invalid Department Id");
            }
            var emp = new Employee
            {
                Name = departmentDto.Name,
                Age = departmentDto.Age,
                DepartmentId = departmentDto.DepartmentId,
            };
           var newEmployee =  _repository.Insert(emp);
            _repository.SaveChanges();
            return newEmployee.Id;
        }
    }
}
