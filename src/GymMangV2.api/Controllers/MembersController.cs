using GymMangV2.Application.DTOs.Members;
using GymMangV2.Application.Interfaces;
using GymMangV2.Application.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace GymMangV2.api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MembersController : ControllerBase
{
    private readonly IMemberService _memberService;

    public MembersController(IMemberService memberService, IMemoryCache cache)
    {
        _memberService = memberService;

    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateMemberRequestDto request)
    {
        var member = await _memberService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = member.Id }, member);
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _memberService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await _memberService.GetByIdAsync(id));

    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateMemberRequestDto request)
    {
        await _memberService.UpdateAsync(id, request);
        return Ok();
    }
    [HttpDelete("id")]
    public async Task<IActionResult> Delete(int id)
    {
        await _memberService.DeleteAsync(id);
        return NoContent();
    }
}