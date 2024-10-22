using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using Pjira.Application.Assigments.Commands.DeleteAssigment;
using Pjira.Application.Assigments.Commands.UpdateAssigment;
using Pjira.Application.Common.Interfaces;
using Pjira.Application.Projects.Commands.CreateProject;
using Pjira.Application.Projects.Commands.DeleteProject;
using Pjira.Application.Projects.Commands.UpdateProject;
using Pjira.Application.Tasks.Commands.CreateTask;
using Pjira.Core.Enums;
using Pjira.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pjira.Application.Tests.CommandTests
{
    public class ProjectCommandTests
    {
        [Fact]
        public async void Should_Add_Projects()
        {
            var mockDbContext = new Mock<IPjiraDbContext>();

            var mockDbSet = new Mock<DbSet<Project>>();

            mockDbContext.Setup(x => x.Projects).Returns(mockDbSet.Object);

            var command = new CreateProjectCommand
            {
                Name = "Test",

                IsActive = true,
            };

            var handler = new CreateProjectCommandHandler(mockDbContext.Object);

            var result = await handler.Handle(command,CancellationToken.None);


            Assert.IsType<Guid>(result);


            mockDbSet.Verify(dbSet => dbSet.Add(It.IsAny<Project>()), Times.Once);


            mockDbContext.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
        [Fact]
        public async void Should_Delete_Assigment()
        {
            List<Project> projects = new List<Project>
            {
             new Project { Id = Guid.NewGuid() },
             new Project { Id = Guid.NewGuid() }
             };

            var projectToDelete = projects[0];

            var command = new DeleteProjectCommand { Id = projectToDelete.Id };


            var mockDbContext = new Mock<IPjiraDbContext>();

            mockDbContext.Setup(repo => repo.Projects).ReturnsDbSet(projects);


            mockDbContext.Setup(repo => repo.Projects.Remove(projectToDelete))
                         .Callback(() => projects.Remove(projectToDelete));


            var handler = new DeleteProjectCommandHandler(mockDbContext.Object);


            await handler.Handle(command, CancellationToken.None);


            mockDbContext.Verify(mock => mock.Projects.Remove(projectToDelete), Times.Once);
            mockDbContext.Verify(mock => mock.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

            Assert.Equal(1, projects.Count);
        }
        [Fact]
        public async void Should_Update_Project()
        {
            var mockDbContext = new Mock<IPjiraDbContext>();

            var projectId = Guid.NewGuid();

            var existingProject = new Project
            {
                Id = projectId,

                Name = "Test",

                IsActive = true,

            };

            List<Project> projects = new List<Project> { existingProject };


            var command = new UpdateProjectCommand
            {
                Id = projectId,
                Name = "TestUpdated",
                IsActive = false,             
            };


            mockDbContext.Setup(x => x.Projects).ReturnsDbSet(projects);


            var handler = new UpdateProjectCommandHandler(mockDbContext.Object);


            await handler.Handle(command, CancellationToken.None);



            Assert.Equal("TestUpdated", existingProject.Name);
            Assert.True(existingProject.IsActive == false);


            mockDbContext.Verify(mock => mock.Projects.Update(existingProject), Times.Once);
            mockDbContext.Verify(mock => mock.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
