using Api.Data.Entities;
using Api.Models.TokenDtos;

namespace Api.Services.TokenService
{
    public interface ITokenService
    {
        public Task<TokenDto> CreateTokens(User user);
        public Task MarkTokenForUsed(string token);
    }
}
