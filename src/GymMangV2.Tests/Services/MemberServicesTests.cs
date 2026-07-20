using AutoMapper;
using AutoMapper.Execution;
using Castle.Core.Logging;
using FluentAssertions;
using GymMangV2.Application.DTOs.Members;
using GymMangV2.Application.Exceptions;
using GymMangV2.Application.Interfaces;
using GymMangV2.Application.Service;
using Microsoft.Extensions.Caching.Memory;
using Moq;

public class MemberServicesTests
{
    private readonly Mock<IMemberRepository> _memberRepo;
    private readonly Mock<IMapper> _mapper;
    private readonly MemberService _service;
    private readonly Mock<ILogger> _logger;
    private readonly Mock<IMemoryCache> _cache;

    public MemberServicesTests()
    {
        _memberRepo = new Mock<IMemberRepository>();
        _mapper = new Mock<IMapper>();
        _logger = new Mock<ILogger>();
        _cache = new Mock<IMemoryCache>();
        // _service = new MemberService(
        //     _memberRepo.Object,
        //     _mapper.Object,
        //     _logger.Object,
        //     _cache.Object
        // );
    }

    public async Task CreateAsync_ShouldCreateMember_WhenPhoneDoesNotExist()
    {
        //Arange 
        var request = new CreateMemberRequestDto
        {
            FirstName = "Rahul",
            LastName = "Sharma",
            Phone = "1111111111"
        };

        var member = new Member();

        var response = new MemberResponseDto
        {
            FullName = "Rahul"
        };

        _memberRepo
        .Setup(x => x.PhoneExistAsync(request.Phone))
        .ReturnsAsync(false);

        _mapper
        .Setup(x => x.Map<Member>(request))
        .Returns(member);

        _mapper
        .Setup(x => x.Map<MemberResponseDto>(member))
        .Returns(response);


        //Act

        var result = await _service.CreateAsync(request);

        //Assert
        result.Should().NotBeNull();
        result.FullName.Should().Be("Rahul");

        //_memberRepo.Verify(x => x.AddAsync(member), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_ShouldThrowException_WhenPhoneAlreadyExists()
    {
        // Arrange

        var request = new CreateMemberRequestDto
        {
            Phone = "9999999999"
        };

        _memberRepo
            .Setup(x => x.PhoneExistAsync(request.Phone))
            .ReturnsAsync(true);

        // Act

        Func<Task> action = () => _service.CreateAsync(request);

        // Assert

        await action.Should()
            .ThrowAsync<BusinessException>();

        // _memberRepo.Verify(x =>
        //     x.AddAsync(It.IsAny<Member>()),
        //     Times.Never);
    }
    [Fact]
    public async Task GetByIdAsync_ShouldReturnMember_WhenExists()
    {
        // Arrange

        var member = new Member();

        // _memberRepo
        //     .Setup(x => x.GetByIdAsync(1))
        //     .ReturnsAsync(member);

        _mapper
            .Setup(x => x.Map<MemberResponseDto>(member))
            .Returns(new MemberResponseDto());

        // Act

        var result = await _service.GetByIdAsync(1);

        // Assert

        result.Should().NotBeNull();

        _memberRepo.Verify(x =>
            x.GetByIdAsync(1),
            Times.Once);
    }
}