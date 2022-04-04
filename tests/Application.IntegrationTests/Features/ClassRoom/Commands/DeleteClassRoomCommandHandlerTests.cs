using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Features.ClassRoom.Commands;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Application.IntegrationTests.Features.ClassRoom.Commands
{
    public class DeleteClassRoomCommandHandlerTests : IClassFixture<TestFixture>
    {
        private TestFixture Fixture { get; }


        public DeleteClassRoomCommandHandlerTests(TestFixture fixture)
        {
            Fixture = fixture;
        }

        [Fact]
        public async Task Handle_WhenGivenIdExistsInDatabase_ShouldDeleteExistingClassRoom()
        {
            await using var context = Fixture.CreateContext();
            var sut = new DeleteClassRoomCommandHandler(context);
            var existingClassRoom = new Domain.Entities.ClassRoom {Grade = 7, Group = "A"};
            await context.ClassRoom.AddAsync(existingClassRoom);
            await context.SaveChangesAsync();
            var countOfClassRoom = await context.ClassRoom.CountAsync();

            await sut.Handle(new DeleteClassRoomCommand(existingClassRoom.Id), CancellationToken.None);
            var result = await context.ClassRoom.CountAsync();

            result.Should().Be(countOfClassRoom - 1);
        }

        [Fact]
        public async Task Handle_WhenGivenIdNotInDatabase_ShouldThrowNotFoundException()
        {
            //Arrange
            await using var context = Fixture.CreateContext();
            var sut = new DeleteClassRoomCommandHandler(context);


            // Act
            Func<Task> result = async () => await sut.Handle(new DeleteClassRoomCommand(Guid.NewGuid()), CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<NotFoundException>();
        }
    }
}