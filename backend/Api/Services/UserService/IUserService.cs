using Api.Models.AuthContracts;
using Api.Models.TokenDtos;
using Api.Models.UserModels;
using LadleMeThis.Models.AuthContracts;
using Microsoft.AspNetCore.Identity;

namespace Api.Services.UserService
{
    public interface IUserService
    {
        Task<UserResponseDto> GetUserByIdAsync(string id);
        Task<IdentityResult> UpdateUserAsync(string id, UserUpdateDto userUpdateDto);
        Task<IdentityResult> DeleteUserAsync(string id);
        Task RegisterAsync(RegistrationRequest registerRequest, string role);
        Task<TokenDto> LoginAsync(AuthRequest authRequest);
        Task<TokenDto> RefreshTokenAsync(string id, string refreshToken);
    }
}
