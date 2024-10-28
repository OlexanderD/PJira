using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using Pjira.Application.Assigments.Commands.DeleteAssigment;
using Pjira.Application.Assigments.Commands.UpdateAssigment;
using Pjira.Application.Common.Interfaces;
using Pjira.Application.Tasks.Commands.CreateTask;
using Pjira.Core.Enums;
using Pjira.Core.Models;
using Pjira.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

namespace Pjira.Application.Tests.CommandTests
{
    public class AssigmentCommandsTests
    {
        [Fact]
        public async void Should_Succesfully_AddAssigment()
        {
            var mockDbContext = new Mock<IPjiraDbContext>();        
            var mockAssignmentDbSet = new Mock<DbSet<Assignment>>();


            mockDbContext.Setup(x => x.Assignments).Returns(mockAssignmentDbSet.Object);

           
            var command = new CreateAssigmentCommand
            {
                Title = "Test Task",
                Description = "Test Description",
                Status = AssigmentStatus.New
            };

            
            var handler = new CreateAssigmentCommandHandler(mockDbContext.Object);

            
            var result = await handler.Handle(command, CancellationToken.None);

           
            Assert.IsType<Guid>(result); 

           
            mockAssignmentDbSet.Verify(dbSet => dbSet.Add(It.IsAny<Assignment>()), Times.Once);

            
            mockDbContext.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
        [Fact]
        public async void Should_Delete_Assigment()
        {

            List<Assignment> assignments = new List<Assignment>
            {
             new Assignment { Id = Guid.NewGuid() },
             new Assignment { Id = Guid.NewGuid() }
             };

            var assignmentToDelete = assignments[0];

            var command = new DeleteAssigmentCommand { Id = assignmentToDelete.Id };

          
            var mockDbContext = new Mock<IPjiraDbContext>();

           
            mockDbContext.Setup(repo => repo.Assignments).ReturnsDbSet(assignments);

           
            mockDbContext.Setup(repo => repo.Assignments.Remove(assignmentToDelete))
                         .Callback(() => assignments.Remove(assignmentToDelete));

           
            var handler = new DeleteAssigmentCommandHandler(mockDbContext.Object);

           
            await handler.Handle(command, CancellationToken.None);

            
            mockDbContext.Verify(mock => mock.Assignments.Remove(assignmentToDelete), Times.Once);
            mockDbContext.Verify(mock => mock.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

            Assert.Equal(1, assignments.Count);


        }
        [Fact]
        public async void Should_Update_Assigment()
        {
            var mockDbContext = new Mock<IPjiraDbContext>();
            


            var assigmentId = Guid.NewGuid();

            var existingAssigment = new Assignment
            {
                Id = assigmentId,

                Title = "Test",

                Description = "Test",

                Status = AssigmentStatus.New,
            };

            List<Assignment> assignments = new List<Assignment> { existingAssigment };


            var command = new UpdateAssigmentCommand
            {
                Id = assigmentId,
                Title = "TestUpdated",
                Description = "Test!",
                Status = AssigmentStatus.InProgress,
            };


            mockDbContext.Setup(x => x.Assignments).ReturnsDbSet(assignments);

               
            var handler = new UpdateAssigmentCommandHandler(mockDbContext.Object);


            await handler.Handle(command, CancellationToken.None);

           

            Assert.Equal("TestUpdated", existingAssigment.Title);
            Assert.Equal("Test!", existingAssigment.Description);
            Assert.True(existingAssigment.Status == AssigmentStatus.InProgress);

          
            mockDbContext.Verify(mock => mock.Assignments.Update(existingAssigment), Times.Once);
            mockDbContext.Verify(mock => mock.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
