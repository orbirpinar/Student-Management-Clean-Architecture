using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Features.ClassRoom.Dtos;
using Application.Features.ClassRoom.Maps;
using Application.Features.ClassRoom.Queries;
using Application.Interfaces;
using AutoMapper;
using FluentAssertions;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

namespace Application.UnitTests.Features.ClassRoom.Queries
{
    public class GetClassRoomByIdQueryHandlerTests
    {
        private readonly GetClassRoomByIdQueryHandler _sut;
        private readonly Mock<IApplicationDbContext> _mockDbContext = new();

        public GetClassRoomByIdQueryHandlerTests()
        {
            var mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ClassRoomMappingProfile());
            }).CreateMapper();
            _sut = new GetClassRoomByIdQueryHandler(_mockDbContext.Object, mapper);
        }

        [Fact]
        public async Task Handle_WhenGivenIdExists_ShouldReturnAClassRoomViewDtoObject()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockDbContext.Setup(context => context.ClassRoom)
                .ReturnsDbSet(GetClassRoom(id));

            // Act
            var result = await _sut.Handle(new GetClassRoomByIdQuery(id),CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ClassRoomViewDto>();
            result.Id.Should().Be(id);
        }

        [Fact]
        public async Task Handle_WhenGivenIdNotExists_ShouldThrowNotFoundException()
        {
            // Arrange
            _mockDbContext.Setup(context => context.ClassRoom)
                .ReturnsDbSet(GetClassRoom(Guid.NewGuid()));

            // Act
            Func<Task> result = async () => await _sut.Handle(new GetClassRoomByIdQuery(Guid.NewGuid()),CancellationToken.None);
            
            // Assert
            await result.Should().ThrowAsync<NotFoundException>();
        }

        private static List<Domain.Entities.ClassRoom> GetClassRoom(Guid id)
        {
            
            return new List<Domain.Entities.ClassRoom> {new() {Id = id, Created = DateTime.Now, Grade = 6, Group = "B"}};
        }
    }
}