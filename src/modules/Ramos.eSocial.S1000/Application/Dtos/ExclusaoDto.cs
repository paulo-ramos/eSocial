namespace Ramos.eSocial.S1000.Application.Dtos;

public record class ExclusaoDto
{
    public IdePeriodoDto IdePeriodo { get; init; } = new();
}