using System.Reflection.Metadata.Ecma335;
using GymMangV2.Application.DTOs.Auth;
using GymMangV2.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GymMangV2.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class Authcontroller : ControllerBase
{
    private readonly IAuthServices _authService;
    public Authcontroller(IAuthServices authServices)
    {
        _authService = authServices;
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] LoginRequest request)
    {
        await _authService.RegisterAsync(request);
        return Ok(new
        {
            Message = "Logout successfilly.." 
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var response = await _authService.LoginAsync(request);
        Console.WriteLine(response.GetType().FullName);
        return Ok(response);
        //   return Ok(new
        // {
        //    Message = "Logout successfilly.." 
        // });
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] LogoutRequest request)
    {
        await _authService.LogoutAsync(request);
        return Ok(new
        {
           Message = "Logout successfilly.." 
        });
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
         await _authService.RefreshTokenAsync(request);
         return Ok(new
         {
            Message = "Refresh Token generated successfully..." 
         });
    }

}