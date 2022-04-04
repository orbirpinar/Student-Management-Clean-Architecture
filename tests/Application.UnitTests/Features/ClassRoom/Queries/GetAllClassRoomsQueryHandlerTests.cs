using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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
    public class GetAllClassRoomsQueryHandlerTests
    {
        private readonly GetAllClassRoomQueryHandler _sut;
        private readonly Mock<IApplicationDbContext> _mockDbContext = new();

        public GetAllClassRoomsQueryHandlerTests()
        {
            var mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ClassRoomMappingProfile());
            }).CreateMapper();
            _sut = new GetAllClassRoomQueryHandler(_mockDbContext.Object, mapper);
        }

        [Fact]
        public async Task Handle_Called_ShouldReturnListOfClassRoomViewDto()
        {
            //Arrange

            _mockDbContext.Setup(context => context.ClassRoom)
                .ReturnsDbSet(GetClassRooms());
            // Act
            var result = await _sut.Handle(new GetAllClassRoomsQuery(), CancellationToken.None);

            // Assert
            result.Should().BeOfType<List<ClassRoomViewDto>>();
            result.Count.Should().Be(2);
        }

        private static IEnumerable<Domain.Entities.ClassRoom> GetClassRooms()
        {
            return new List<Domain.Entities.ClassRoom> {new() {Id = Guid.NewGuid(), Grade = 8, Group = "A", Created = DateTime.Now}, new() {Id = Guid.NewGuid(), Grade = 7, Group = "A", Created = DateTime.Now}};
        }
    }
}