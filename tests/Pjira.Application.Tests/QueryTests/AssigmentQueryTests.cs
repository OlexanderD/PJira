using Moq;
using Moq.EntityFrameworkCore;
using Pjira.Application.Assigments.Queries.GetAllAssigments;
using Pjira.Application.Common.Interfaces;
using Pjira.Core.Models;
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

            var mockPjiraContext = new Mock<IPjiraDbContext>();
           

            mockPjiraContext.Setup(x => x.Assignments).ReturnsDbSet(assignments);
           
            var query = new GetAllAssigmentQuery();

            var handler = new GetAllAssigmentsQueryHandler(mockPjiraContext.Object);

            var result = await handler.Handle(query,CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);

            mockPjiraContext.Verify(db => db.Assignments, Times.Once);

        }

    }
}
