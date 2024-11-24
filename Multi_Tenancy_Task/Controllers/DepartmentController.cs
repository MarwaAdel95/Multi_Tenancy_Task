using MediatR;
using Microsoft.AspNetCore.Mvc;
using Multi_Tenancy_Task.CQRS.department.Commands;
using Multi_Tenancy_Task.CQRS.department.Queries;
using Multi_Tenancy_Task.ViewModels;

namespace Multi_Tenancy_Task.Controllers
{



    [ApiController]
    [Route("api/department")]
    public class DepartmentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DepartmentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]

        public async Task<IActionResult> InsertDepartment(InsertDepartmentViewModel viewModel)
        {
            try
            {
                var result = await _mediator.Send(new InsertDepartmentCommand(viewModel));
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

        }
        [HttpGet]

        public async Task<IActionResult> GetAllDepartments()
        {
            try
            {
                var result = await _mediator.Send(new GetAllDepartmentQuery());
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

        }
    }
}
