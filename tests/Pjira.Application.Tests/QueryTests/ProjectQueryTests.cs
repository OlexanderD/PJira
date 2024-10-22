using Moq;
using Moq.EntityFrameworkCore;
using Pjira.Application.Assigments.Queries.GetAllAssigments;
using Pjira.Application.Common.Interfaces;
using Pjira.Application.Projects.Queries.GetAllProjects;
using Pjira.Application.Projects.Queries.GetProjectById;
using Pjira.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Pjira.Application.Tests.QueryTests
{
    public class ProjectQueryTests
    {
        [Fact]
        public async void Should_GetAllProjects()
        {

            List<Project> projects = new List<Project>
            {
             new Project { Id = Guid.NewGuid() },
             new Project { Id = Guid.NewGuid() }
            };

            var mockPjiraContext = new Mock<IPjiraDbContext>();


            mockPjiraContext.Setup(x => x.Projects).ReturnsDbSet(projects);

            var query = new GetAllProjectsQuery();

            var handler = new GetAllProjectsQueryHandler(mockPjiraContext.Object);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);

            mockPjiraContext.Verify(db => db.Projects, Times.Once);
        }
        [Fact]
        public async void Should_GetProjectById()
        {
            var mockPjiraContext = new Mock<IPjiraDbContext>();

            var projectId = Guid.NewGuid();

            var project = new Project
            {
                Id = projectId,

            };

            mockPjiraContext.Setup(x => x.Projects).ReturnsDbSet(new List<Project> { project });

            var query = new GetProjectByIdQuery(projectId);

            var handler = new GetProjectByIdQueryHandler(mockPjiraContext.Object);

            var result = await handler.Handle(query, CancellationToken.None);


            Assert.NotNull(result);

            Assert.Equal(projectId, result.Id);

            mockPjiraContext.Verify(db => db.Projects, Times.Once);

        }
    }
}