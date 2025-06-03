using System.Security.Claims;
using Api.Models.UserModels;
using Api.Services.UserService;
using LadleMeThis.Models.AuthContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.UserController;

[ApiController]
public class UserController(IUserService userService) : ControllerBase
{
	[Authorize]
	[HttpGet("/user")]
	public async Task<IActionResult> GetUserById()
	{
		var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		var user = await userService.GetUserByIdAsync(userId);
		return Ok(user);
	}

	[Authorize]
	[HttpPut("/user")]
	public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDto userUpdateDto)
	{
		var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		await userService.UpdateUserAsync(userId, userUpdateDto);
		return NoContent();
	}

	[Authorize]
	[HttpDelete("/user")]
	public async Task<IActionResult> DeleteUser()
	{
		var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		await userService.DeleteUserAsync(userId);
		return NoContent();
	}

	[HttpPost("register")]
	public async Task<IActionResult> Register(RegistrationRequest registrationRequest)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		const string role = "User";
		await userService.RegisterAsync(registrationRequest, role);
		
		return Created();
	}

	[HttpPost("login")]
	public async Task<IActionResult> Authenticate([FromBody] AuthRequest request)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);


		var tokenDto = await userService.LoginAsync(request);

		var accessCookieOptions = new CookieOptions
		{
			Expires = DateTime.UtcNow.AddMinutes(tokenDto.AccessTokenExpirationMinutes),
		};
		var refreshCookieOptions = new CookieOptions
		{
			Expires = DateTime.UtcNow.AddMinutes(tokenDto.RefreshTokenExpirationDays),
		};

		Response.Cookies.Append("AccessToken", tokenDto.AccessToken, accessCookieOptions);
		Response.Cookies.Append("RefreshToken", tokenDto.RefreshToken, refreshCookieOptions);

		return Ok();
		
	}
	
	[HttpPost("refresh")]
	public async Task<IActionResult> Authenticate()
	{
		
		var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		var refreshToken = HttpContext.Request.Cookies["RefreshToken"];

		if (refreshToken == null)
			return Unauthorized("Unauthorized access.");
		
		var tokenDto = await userService.RefreshTokenAsync(userId, refreshToken);

		var accessCookieOptions = new CookieOptions
		{
			Expires = DateTime.UtcNow.AddMinutes(tokenDto.AccessTokenExpirationMinutes),
		};
		var refreshCookieOptions = new CookieOptions
		{
			Expires = DateTime.UtcNow.AddMinutes(tokenDto.RefreshTokenExpirationDays),
		};
		

		Response.Cookies.Append("AccessToken", tokenDto.AccessToken, accessCookieOptions);
		Response.Cookies.Append("RefreshToken", tokenDto.RefreshToken, refreshCookieOptions);

		return Ok();
		
	}

	[HttpPost("logout")]
	public IActionResult Logout()
	{
		Response.Cookies.Delete("AuthToken");
		return Ok("Logged out successfully");
	}
}