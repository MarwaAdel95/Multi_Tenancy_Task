using MediatR;
using Multi_Tenancy_Task.CQRS.Tenancy.Queries;
using Multi_Tenancy_Task.Entities;
using Multi_Tenancy_Task.Repositories;
using Multi_Tenancy_Task.Services;
using Multi_Tenancy_Task.ViewModels;

namespace Multi_Tenancy_Task.CQRS.department.Commands
{

        public record InsertDepartmentCommand(InsertDepartmentViewModel viewModel) : IRequest<int>;
        public class InsertDepartmentCommandHandler : IRequestHandler<InsertDepartmentCommand, int>
        {
            private readonly IBaseRepository<Department> _repository;
            private readonly IMediator _mediator;
            private readonly TenantService _tenantService;
            public InsertDepartmentCommandHandler(IBaseRepository<Department> repository,
                IMediator mediator,
                TenantService tenantService)
            {
                _repository = repository;
                _mediator = mediator;
                _tenantService = tenantService;
            }
            public async Task<int> Handle(InsertDepartmentCommand request, CancellationToken cancellationToken)
            {
                var tenantId = _tenantService.GetTenantIdFromHeader();
                var tenantIdExist = await _mediator.Send(new CheckTenantIdExistsQuery(tenantId));
                // if tenantid doesnt exist ==> create new one

                var department = new Department
                {
                    Name = request.viewModel.Name,
                    Description = request.viewModel.Description
                };
                var newDepartment = _repository.Insert(department);
                _repository.SaveChanges();
                return newDepartment.Id;
            }
        }
    }


