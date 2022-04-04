using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.ClassRoom.Commands;
using Application.Interfaces;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Application.UnitTests.Features.ClassRoom.Commands
{
    public class CreateClassRoomCommandHandlerTest
    {
        private readonly CreateClassRoomCommandHandler _sut;
        private readonly Mock<IApplicationDbContext> _mockDbContext = new();
        private readonly Mock<DbSet<Domain.Entities.ClassRoom>> _mockDbSet = new();

        public CreateClassRoomCommandHandlerTest()
        {
            _sut = new CreateClassRoomCommandHandler(_mockDbContext.Object);
        }

        [Fact]
        public async Task Handle_WhenGivenCommandIsValid_ShouldReturnNewClassRoomId()
        {
            // Arrange
            _mockDbContext.Setup(context => context.ClassRoom)
                .Returns(_mockDbSet.Object);
            _mockDbContext.Setup(context => context.ClassRoom.AddAsync(GetClassRoom(),CancellationToken.None));
            _mockDbContext.Setup(context => context.SaveChangesAsync(CancellationToken.None));

            // Act
            var result = await _sut.Handle(GetClassRoomCommand(), CancellationToken.None);

            // Assert
            _mockDbContext.Verify(context => context.ClassRoom.AddAsync(GetClassRoom(),CancellationToken.None),Times.Once);
        }

        private static CreateClassRoomCommand GetClassRoomCommand()
        {
            return new CreateClassRoomCommand
            {
                Grade = 8,
                Group = "A"
            };
            
        }

        private Domain.Entities.ClassRoom GetClassRoom()
        {
            return new Domain.Entities.ClassRoom {Grade = 8, Group = "A"};
        }
    }
}