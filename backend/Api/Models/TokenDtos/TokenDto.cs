namespace Api.Models.TokenDtos;

public record TokenDto(string AccessToken, string RefreshToken, int AccessTokenExpirationMinutes, int RefreshTokenExpirationDays);

	
