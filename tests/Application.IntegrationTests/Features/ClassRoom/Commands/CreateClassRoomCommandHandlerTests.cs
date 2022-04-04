using System.Threading;
using System.Threading.Tasks;
using Application.Features.ClassRoom.Commands;
using Application.Features.ClassRoom.Maps;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Application.IntegrationTests.Features.ClassRoom.Commands
{
    public class CreateClassRoomCommandHandlerTests : IClassFixture<TestFixture>
    {
        private TestFixture Fixture { get; }


        public CreateClassRoomCommandHandlerTests(TestFixture fixture)
        {
            Fixture = fixture;
        }

        [Fact]
        public async Task Handle_WhenGivenRequestIsValid_ShouldCreateNewClassRoom()
        {
            // Arrange
            await using var context = Fixture.CreateContext();
            var sut = new CreateClassRoomCommandHandler(context);
            var existingClassRoom = await context.ClassRoom.CountAsync();
            
            // Act
            await sut.Handle(ValidClassRoomCommand(), CancellationToken.None);
            var result = await context.ClassRoom.ToListAsync();

            // Assert
            result.Should().HaveCount(existingClassRoom + 1);
        }


        private static CreateClassRoomCommand ValidClassRoomCommand()
        {
            return new CreateClassRoomCommand {Grade = 6, Group = "D"};
        }
    }
}