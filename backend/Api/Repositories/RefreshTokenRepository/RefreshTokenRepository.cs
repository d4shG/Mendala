using Api.Data.Context;
using Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories.RefreshTokenRepository;

public class RefreshTokenRepository(MendalaApiContext context) : IRefreshTokenRepository
{
	public async Task<string> AddRefreshTokenAsync(RefreshToken refreshToken)
	{
		context.RefreshTokens.Add(refreshToken);
		await context.SaveChangesAsync();
		
		return refreshToken.Token;
	}

	public async Task<RefreshToken?> GetByTokenAsync(string refreshToken) => 
		await context.RefreshTokens.SingleOrDefaultAsync(r => r.Token == refreshToken);

	public async Task UpdateAsync(RefreshToken refreshToken)
	{
		context.RefreshTokens.Update(refreshToken);
		await context.SaveChangesAsync();
	}
}