using MediatR;
using Microsoft.AspNetCore.Mvc;
using Multi_Tenancy_Task.CQRS.employee.Commands;
using Multi_Tenancy_Task.CQRS.employee.Queries;
using Multi_Tenancy_Task.Entities;
using Multi_Tenancy_Task.Repositories;
using Multi_Tenancy_Task.Services;
using Multi_Tenancy_Task.ViewModels;

namespace Multi_Tenancy_Task.Controllers
{

    [ApiController]
    [Route("api/employee")]
    public class EmployeeController :ControllerBase
    {
        private readonly IMediator _mediator;
        public EmployeeController(IMediator mediator)
        {
         _mediator = mediator;
        }
        [HttpPost]
        
        public async Task< IActionResult> InsertEmployee(InsertEmployeeViewModel viewModel)
        {
            try
            {
                var result = await _mediator.Send(new InsertEmployeeCommand(viewModel));
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
         
        }
        [HttpGet]

        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                var result = await _mediator.Send(new GetAllEmployeesQuery());
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

        }
    }
}
