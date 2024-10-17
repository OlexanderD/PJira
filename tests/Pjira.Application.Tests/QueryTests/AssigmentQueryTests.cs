using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using Pjira.Application.Assigments.Queries.GetAllAssigments;
using Pjira.Core.Models;
using Pjira.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pjira.Application.Tests.QueryTests
{
    public class AssigmentQueryTests
    {
        [Fact]
        public async void Should_GetAll_Assigments()
        {
            List<Assignment> assignments = new List<Assignment>
            {
             new Assignment { Id = Guid.NewGuid() },
             new Assignment { Id = Guid.NewGuid() }
            };

            var mockPjiraContext = new Mock<PjiraDbContext>();
           

            mockPjiraContext.Setup(db => db.Assignments).ReturnsDbSet(assignments);
           
            var query = new GetAllAssigmentQuery();

            var handler = new GetAllAssigmentsQueryHandler(mockPjiraContext.Object);

            var result = await handler.Handle(query,CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            
            mockPjiraContext.Verify(db => db.Assignments.ToListAsync(It.IsAny<CancellationToken>()), Times.Once);

        }

    }
}
