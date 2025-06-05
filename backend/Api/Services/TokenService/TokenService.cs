using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.Data.Entities;
using Api.Models.TokenDtos;
using Api.Repositories.RefreshTokenRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Api.Services.TokenService
{
    public class TokenService(IRefreshTokenRepository repository,IConfiguration configuration, UserManager<User> userManager)
        : ITokenService
    {

        private const int AccessTokenExpirationMinutes = 20;
        private const int RefreshTokenExpirationDays = 7;


        public async Task<TokenDto> CreateTokens(User user)
        {
            var accessTokenExpiration = DateTime.UtcNow.AddMinutes(AccessTokenExpirationMinutes);
            var claims = await CreateClaimsAsync(user, accessTokenExpiration);
            var signingCredentials = CreateSigningCredentials();
            var token = CreateJwtToken(claims, signingCredentials, accessTokenExpiration);
            var createRefreshToken = new RefreshToken
            {
                UserId = user.Id,
                Expiration = DateTime.UtcNow.AddDays(RefreshTokenExpirationDays), 
            };
            
            var refreshToken = await repository.AddRefreshTokenAsync(createRefreshToken);
            
            return new TokenDto(new JwtSecurityTokenHandler().WriteToken(token), refreshToken, AccessTokenExpirationMinutes, AccessTokenExpirationMinutes);
        }

        public async Task MarkTokenForUsed(string token, string userId)
        {
            var updatedRefresh = await repository.GetByTokenAsync(token);
            
            if (updatedRefresh == null || 
                updatedRefresh.IsUsed || 
                updatedRefresh.IsRevoked || 
                updatedRefresh.Expiration < DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("Invalid or expired refresh token.");
            }
            
            if (updatedRefresh.UserId != userId)
                throw new UnauthorizedAccessException("Id mismatch!");
            
            updatedRefresh.IsUsed = true;
            await repository.UpdateAsync(updatedRefresh);
        }

        private JwtSecurityToken CreateJwtToken(List<Claim> claims, SigningCredentials credentials,
            DateTime expiration) =>
            new(
                configuration["ValidIssuer"],
                configuration["ValidAudience"], 
                claims,
                expires: expiration,
                signingCredentials: credentials
            );

        private async Task<List<Claim>> CreateClaimsAsync(User user, DateTime expiration)
        {
            var claims = new List<Claim>
           {
                new(JwtRegisteredClaimNames.Sub, user.Id),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Iat, EpochTime.GetIntDate(DateTime.UtcNow).ToString(CultureInfo.InvariantCulture), ClaimValueTypes.Integer64),
                new(ClaimTypes.NameIdentifier, user.Id),
                new Claim("exp", ((DateTimeOffset)expiration).ToUnixTimeSeconds().ToString()),
           };



            var roles = await userManager.GetRolesAsync(user);


            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));
            

            return claims;
        }


        private SigningCredentials CreateSigningCredentials()
        {
            return new SigningCredentials(
                new SymmetricSecurityKey(

                    Encoding.UTF8.GetBytes(configuration["IssuerSigningKey"])
                ),
                SecurityAlgorithms.HmacSha256
            );
        }
    }
}
