namespace Api.Models.UserModels
{
	public record UserResponseDto(
		string UserId,
		string Username,
		string Email,
		DateTime DateCreated
	);
}