using Api.Models.AuditableEntity;

namespace Api.Data.Entities;

public class RefreshToken: IAuditable
{
	public string Token { get; set; } = Guid.NewGuid().ToString();
	public string UserId { get; set; }
	public DateTime Expiration { get; set; }
	public bool IsRevoked { get; set; } = false;
	public bool IsUsed { get; set; } = false;
	public string? ReplacedByToken { get; set; }

	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
}