using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.ClassRoom.Dtos;
using Application.Features.ClassRoom.Maps;
using Application.Features.ClassRoom.Queries;
using AutoMapper;
using FluentAssertions;
using Xunit;

namespace Application.IntegrationTests.Features.ClassRoom.Queries
{
    public class GetAllClassRoomsQueryHandlerTests: IClassFixture<TestFixture>
    {
        private readonly IMapper _mapper;
        private TestFixture Fixture { get; }


        public GetAllClassRoomsQueryHandlerTests(TestFixture fixture)
        {
            Fixture = fixture;
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ClassRoomMappingProfile>();
            });
            _mapper = configuration.CreateMapper();
        }
        
        
        [Fact]
        public async Task WhenHandleMethodCalled_ShouldReturnAllClassRoom()
        {
            // Arrange
            await using var context = Fixture.CreateContext();
            var sut = new GetAllClassRoomQueryHandler(context, _mapper);
            
            // Act
            var result = await sut.Handle(new GetAllClassRoomsQuery(), CancellationToken.None);
            
            // Assert
            result.Should().BeOfType<List<ClassRoomViewDto>>();
            result.Should().HaveCount(10);
        }
        
    }
}