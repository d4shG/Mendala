namespace Api.Models.AuditableEntity;

public interface IAuditable
{
	DateTime CreatedAt { get; set; }
	DateTime UpdatedAt { get; set; }
}