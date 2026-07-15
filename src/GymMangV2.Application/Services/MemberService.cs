using System.Net.Cache;
using System.Runtime.CompilerServices;
using AutoMapper;
using GymMangV2.Application.DTOs.Members;
using GymMangV2.Application.Exceptions;
using GymMangV2.Application.Interfaces;
using GymMangV2.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace GymMangV2.Application.Service;

public class MemberService : IMemberService
{
    private readonly IMemberRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<MemberService> _logger;
    private readonly IMemoryCache _cache;
    private const string MembershipPlansCacheKey =
    "membership-plans";
    public MemberService(IMemberRepository repository,
     IMapper mapper,
      ILogger<MemberService> logger,
      IMemoryCache cache)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
        _cache = cache;
    }

    public async Task<MemberResponseDto> CreateAsync(CreateMemberRequestDto request)
    {
        if (await _repository.EmailExistAsync(request.Email))
            throw new BusinessException("Email already exist");
        if (await _repository.PhoneExistAsync(request.Phone))
            throw new BusinessException("Phone already exist");

        var member = new Member
        {
            FullName = request.FirstName + request.LastName,
            Email = request.Email,
            Phone = request.Phone,
            Address = request.Address,
            Dob = request.DateOfBirth,
            TrainerId = request.TrainerId,
            UserId = request.userId
        };

        await _repository.AddAsync(member);
        //await _repository
        // return new MemberResponseDto
        // {
        //     Id = member.Id,
        //     FullName = $"{member.FullName}",
        //     Email = member.Email,
        //     Phone = member.Phone,
        //     TrainerName = ""
        // };


        _logger.LogInformation(
            "Member Created Successfullu. MemberId = {Member ID}",
            member.Id
        );
        _cache.Remove(MembershipPlansCacheKey);
        var memberdto = _mapper.Map<MemberResponseDto>(member);

        _cache.Set(
            MembershipPlansCacheKey,
            memberdto,
            TimeSpan.FromMinutes(30)
        );


        return memberdto;

    }


    public async Task<List<MemberResponseDto>> GetAllAsync()
    {
        if (_cache.TryGetValue(
            MembershipPlansCacheKey,
            out List<MemberResponseDto>? cachemembers
        ))
        {
            return cachemembers!;
        }

        var members = await _repository.GetAllAsync();
        // return members.Select(member => new MemberResponseDto
        // {
        //     Id = member.Id,
        //     FullName = $"{member.FullName}",
        //     Email = member.Email,
        //     Phone = member.Phone,
        //     TrainerName = member.Trainer?.FullName ?? ""
        // }).ToList();

        //return _mapper.Map<List<MemberResponseDto>>(members);
        var membersDto = _mapper.Map<List<MemberResponseDto>>(members);

        _cache.Set(
    MembershipPlansCacheKey,
    membersDto,
    TimeSpan.FromMinutes(30));


        return membersDto;
    }


    public async Task<MemberResponseDto?> GetByIdAsync(int id)
    {
        var member = await _repository.GetByIdAsync(id);
        return new MemberResponseDto
        {
            Id = member.Id,
            FullName = $"{member.FullName}",
            Email = member.Email,
            Phone = member.Phone,
            TrainerName = member.Trainer?.FullName ?? ""
        };
    }


    public async Task UpdateAsync(int id, UpdateMemberRequestDto request)
    {
        var member = await _repository.GetByIdAsync(id);
        if (member == null)
            throw new NotFoundException("Member Not found");

        // member.FullName = request.FirstName + request.LastName;
        // member.Email = request.Email;
        // member.Phone = request.Phone;
        // member.Address = request.Address;
        // member.Dob = request.DateOfBirth;
        // member.TrainerId = request.TrainerId;
        _mapper.Map(request, member);

        await _repository.UpdateAsync(member);
        //await _repository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var member = await _repository.GetByIdAsync(id);
        if (member == null)
            throw new Exception("Member Not Found");

        await _repository.DeleteAsync(member);
        //await _repository.SaveChangesAsync();


    }
}