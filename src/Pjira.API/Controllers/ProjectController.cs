using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pjira.Application.Projects.Commands.CreateProject;
using Pjira.Application.Projects.Commands.DeleteProject;
using Pjira.Application.Projects.Commands.UpdateProject;
using Pjira.Application.Projects.Queries.GetAllProjects;
using Pjira.Application.Projects.Queries.GetProjectById;
using Pjira.Application.Tasks.Commands.CreateTask;

namespace Pjira.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            var query = await _mediator.Send(new GetAllProjectsQuery());

            return Ok(query);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(Guid id)
        {
            var project = await _mediator.Send(new GetProjectByIdQuery(id));

            return Ok(project);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProject(CreateProjectCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProject(Guid Id)
        {
            await _mediator.Send(new DeleteProjectCommand { Id = Id });

            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProject(UpdateProjectCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }


    }
}
