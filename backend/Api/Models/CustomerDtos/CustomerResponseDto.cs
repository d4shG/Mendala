namespace Api.Models.CustomerDtos;

public record CustomerResponseDto(
	Guid Id,
	string Name,
	string Email,
	string Phone,
	Address.Address Address
	);