using Api.Data.Entities;

namespace Api.Repositories.RefreshTokenRepository;

public interface IRefreshTokenRepository
{
	Task<string> AddRefreshTokenAsync(RefreshToken refreshToken);
	Task<RefreshToken?> GetByTokenAsync(string refreshToken);
	Task UpdateAsync(RefreshToken refreshToken);
}