using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Features.ClassRoom.Maps;
using Application.Features.ClassRoom.Queries;
using AutoMapper;
using FluentAssertions;
using Xunit;

namespace Application.IntegrationTests.Features.ClassRoom.Queries
{
    public class GetClassRoomByIdHandlerTests: IClassFixture<TestFixture>
    {
        private readonly IMapper _mapper;
        private TestFixture Fixture { get; }


        public GetClassRoomByIdHandlerTests(TestFixture fixture)
        {
            Fixture = fixture;
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ClassRoomMappingProfile>();
            });
            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public async Task Handle_WhenGivenIdInDatabase_ShouldReturnIndividualClassRoom()
        {
            var classRoom = new Domain.Entities.ClassRoom {Grade = 6, Group = "B"};
            await using var context = Fixture.CreateContext();
            await context.ClassRoom.AddAsync(classRoom);
            await context.SaveChangesAsync();
            var sut = new GetClassRoomByIdQueryHandler(context, _mapper);
            // Act
            var result = await sut.Handle(new GetClassRoomByIdQuery(classRoom.Id), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Grade.Should().Be(6);
        }

        [Fact]
        public async Task Handle_WhenGivenIdNotInDatabase_ShouldThrowNotFoundException()
        {
            //Arrange
            await using var context = Fixture.CreateContext();
            var sut = new GetClassRoomByIdQueryHandler(context, _mapper);
            
            // Act
            Func<Task> result = async () => await sut.Handle(new GetClassRoomByIdQuery(Guid.NewGuid()),CancellationToken.None);
            
            // Assert
            await result.Should().ThrowAsync<NotFoundException>();
        }
    }
}