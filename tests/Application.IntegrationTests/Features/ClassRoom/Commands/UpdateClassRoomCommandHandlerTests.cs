using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Features.ClassRoom.Commands;
using FluentAssertions;
using SQLitePCL;
using Xunit;

namespace Application.IntegrationTests.Features.ClassRoom.Commands
{
    public class UpdateClassRoomCommandHandlerTests : IClassFixture<TestFixture>
    {
        private TestFixture Fixture { get; }


        public UpdateClassRoomCommandHandlerTests(TestFixture fixture)
        {
            Fixture = fixture;
        }


        [Theory]
        [InlineData(8, "A")]
        [InlineData(6,"B")]
        public async Task Handle_WhenGivenIdInDatabase_ShouldUpdateExistingClassRoom(byte grade, string group)
        {
            // Arrange
            await using var context = Fixture.CreateContext();
            var sut = new UpdateClassRoomCommandHandler(context);
            var existingClassRoom = new Domain.Entities.ClassRoom {Grade = 7, Group = "A"};
            await context.ClassRoom.AddAsync(existingClassRoom);
            await context.SaveChangesAsync();
            var updateCommand = new UpdateClassRoomCommand {Id = existingClassRoom.Id, Grade = grade, Group = group};

            // Act
            await sut.Handle(updateCommand, CancellationToken.None);

            existingClassRoom.Grade.Should().Be(grade);
            existingClassRoom.Group.Should().Be(group);
        }

        [Fact]
        public async Task Handle_WhenGivenIdNotInDatabase_ShouldThrowNotFoundException()
        {
            //Arrange
            await using var context = Fixture.CreateContext();
            var sut = new UpdateClassRoomCommandHandler(context);
            var updateCommand = new UpdateClassRoomCommand {Id = Guid.NewGuid(),Grade = 8,Group = "B"};
            
            
            // Act
            Func<Task> result = async () => await sut.Handle(updateCommand,CancellationToken.None);
            
            // Assert
            await result.Should().ThrowAsync<NotFoundException>();
            
        }
    }
}