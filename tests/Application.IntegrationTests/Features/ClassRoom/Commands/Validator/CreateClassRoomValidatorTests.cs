using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using Application.Features.ClassRoom.Commands;
using Application.Features.ClassRoom.Commands.Validators;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace Application.IntegrationTests.Features.ClassRoom.Commands.Validator
{
    public class CreateClassRoomValidatorTests
    {
        public CreateClassRoomValidator Validator { get; }

        public CreateClassRoomValidatorTests()
        {
            Validator = new CreateClassRoomValidator();
        }

        [Fact]
        public void Grade_ShouldNotEmpty()
        {
            var command = new CreateClassRoomCommand {Group = "A"};
            Validator.Validate(command).IsValid.Should().BeFalse();
        }

        [Theory]
        [InlineData(14,"A")]
        [InlineData(0,"A")]
        public void Grade_MustBeGreaterThanOneAndLessThanThirteen(byte grade,string group)
        {
            var command = new CreateClassRoomCommand {Grade = grade, Group = group};
            var result = Validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().BePositive();
        }

        [Fact]
        public void Group_ShouldNotBeEmpty()
        {
            var command = new CreateClassRoomCommand {Grade = 8};
            Validator.Validate(command).IsValid.Should().BeFalse();
        }

        [Theory]
        [InlineData(7,"2")]
        [InlineData(8,"AB")]
        [InlineData(7,"*")]
        public void Group_MustBeSingleLetter(byte grade,string group)
        {
            var command = new CreateClassRoomCommand {Grade = grade, Group = group};
            Validator.Validate(command).IsValid.Should().BeFalse();
        }

        [Theory]
        [InlineData(7,"A")]
        [InlineData(8,"B")]
        [InlineData(12,"D")]
        public void WhenGivenDataIsValid_ValidatorShouldReturnTrue(byte grade, string group)
        {
            var command = new CreateClassRoomCommand {Grade = grade, Group = group};
            Validator.Validate(command).IsValid.Should().BeTrue();
        }
    }
}