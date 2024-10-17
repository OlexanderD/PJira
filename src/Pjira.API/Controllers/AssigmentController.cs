using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pjira.Application.Assigments.Commands.DeleteAssigment;
using Pjira.Application.Assigments.Commands.UpdateAssigment;
using Pjira.Application.Assigments.Queries.GetAllAssigments;
using Pjira.Application.Tasks.Commands.CreateTask;

namespace Pjira.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssigmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AssigmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAssigments()
        {
            var query = await _mediator.Send(new GetAllAssigmentQuery());

            return Ok(query);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateAssigmentCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(UpdateAssigmentCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteAssigmentCommand { Id = id });
            return Ok();
        }
    }
}
