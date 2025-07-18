namespace Ramos.eSocial.S1000.Application.Dtos;

public record class AlteracaoDto
{
    public IdePeriodoDto IdePeriodo { get; init; } = new();
    public InfoCadastroDto InfoCadastro { get; init; } = new();
}