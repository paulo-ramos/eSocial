namespace Ramos.eSocial.S1000.Application.Dtos;

public record class EventoS1000InclusaoDto
{
    public string Id { get; init; } = string.Empty;
    public IdeEventoS1000Dto IdeEvento { get; init; } = new();
    public IdeEmpregadorDto IdeEmpregador { get; init; } = new();
    public InclusaoDto Inclusao { get; init; } = new();
}