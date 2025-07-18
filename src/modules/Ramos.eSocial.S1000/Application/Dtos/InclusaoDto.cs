namespace Ramos.eSocial.S1000.Application.Dtos;

public record class InclusaoDto
{
    public IdePeriodoDto IdePeriodo { get; init; } = new();
    public InfoCadastroDto InfoCadastro { get; init; } = new();
}