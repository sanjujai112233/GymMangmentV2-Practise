using FluentAssertions;
using GymMangV2.api.Controllers;
using GymMangV2.Application.DTOs.Members;
using GymMangV2.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Moq;

public class MemberControllerTests
{
    private readonly Mock<IMemberService> _service;

    private readonly MembersController _controller;
    private readonly Mock<IMemoryCache> _cache;

    public MemberControllerTests()
    {
        _service = new Mock<IMemberService>();
        _cache = new Mock<IMemoryCache>();

        _controller = new MembersController(
            _service.Object,
            _cache.Object
            );
    }

    [Fact]
    public async Task GetById_ShouldReturnOk_WhenMemberExists()
    {
        // Arrange

        _service
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(new MemberResponseDto());

        // Act

        var result = await _controller.GetById(1);

        // Assert

        result.Should()
            .BeOfType<OkObjectResult>();

        _service.Verify(
            x => x.GetByIdAsync(1),
            Times.Once);
    }

    // [Fact]
    // public async Task GetById_ShouldReturnNotFound_WhenMemberDoesNotExist()
    // {
    //     // Arrange

    //     _service
    //         .Setup(x => x.GetByIdAsync(1))
    //         .ReturnsAsync((MemberResponseDto?)null);

    //     // Act

    //     var result = await _controller.GetById(1);

    //     // Assert

    //     result.Should()
    //         .BeOfType<NotFoundResult>();
    // }


}