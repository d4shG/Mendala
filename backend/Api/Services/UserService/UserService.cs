using System.Security.Authentication;
using Api.Data.Entities;
using Api.Exceptions;
using Api.Models.AuthContracts;
using Api.Models.TokenDtos;
using Api.Models.UserModels;
using Api.Services.TokenService;
using LadleMeThis.Models.AuthContracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Api.Services.UserService
{
	public class UserService(
		UserManager<User> userManager,
		RoleManager<IdentityRole> roleManager,
		ITokenService tokenService)
		: IUserService
	{
		public async Task<UserResponseDto> GetUserByIdAsync(string userId)
		{
			var user = await userManager.FindByIdAsync(userId);

			if (user == null) 
				throw new NotFoundException("User with given id not found!");


			return new UserResponseDto(user.Id, user.UserName, user.Email, user.CreatedAt);
			
		}
		

		public async Task<IdentityResult> UpdateUserAsync(string userId, UserUpdateDto userUpdateDto)
		{
			var user = await userManager.FindByIdAsync(userId);

			if (user == null) throw new KeyNotFoundException("User with given id not found!");

			if (!string.IsNullOrEmpty(userUpdateDto.Username))
				user.UserName = userUpdateDto.Username;

			if (!string.IsNullOrEmpty(userUpdateDto.Email))
				user.Email = userUpdateDto.Email;

			if (!string.IsNullOrEmpty(userUpdateDto.NewPassword))
			{
				var passwordValidator = new PasswordValidator<User>();
				var passwordValidationResult =
					await passwordValidator.ValidateAsync(userManager, user, userUpdateDto.NewPassword);

				if (!passwordValidationResult.Succeeded)
					return passwordValidationResult;

				user.PasswordHash = userManager.PasswordHasher.HashPassword(user, userUpdateDto.NewPassword);
			}
			
			var result = await userManager.UpdateAsync(user);
			return result;
		}

		public async Task<IdentityResult> DeleteUserAsync(string userId)
		{
			var user = await userManager.FindByIdAsync(userId);

			if (user == null) throw new KeyNotFoundException("User with given id not found!");


			return await userManager.DeleteAsync(user);
		}

		public async Task RegisterAsync(RegistrationRequest request, string role)
		{
			var user = new User
			{
				UserName = request.Username,
				Email = request.Email,
			};

			var roleExists = await roleManager.RoleExistsAsync(role);
			if (!roleExists)
				throw new ArgumentException("The specified role does not exist.");
				
			
			var result = await userManager.CreateAsync(user, request.Password);

			if (!result.Succeeded)
				throw new ArgumentException("The specified user already exists.");
			
			
			await userManager.AddToRoleAsync(user, role);
		}


		public async Task<TokenDto> LoginAsync(AuthRequest authRequest)
		{
			var user = await userManager.FindByEmailAsync(authRequest.EmailOrUsername) ??
			           await userManager.FindByNameAsync(authRequest.EmailOrUsername);

			if (user == null)
				throw new AuthenticationException("User with given email or username not found!");


			var isPasswordValid = await userManager.CheckPasswordAsync(user, authRequest.Password);
			if (!isPasswordValid)
				throw new AuthenticationException("Password is incorrect!");


			return await tokenService.CreateTokens(user);
		}

		public async Task<TokenDto> RefreshTokenAsync(string id, string refreshToken)
		{
			var user = await userManager.FindByIdAsync(id);

			if (user == null)
				throw new AuthenticationException("User with given id not found!");
			
			await tokenService.MarkTokenForUsed(refreshToken, id);
			
			return await tokenService.CreateTokens(user);
		}
	}
}