namespace Ramos.eSocial.S1000.Application.Dtos;

public record class EventoS1000ExclusaoDto
{
    public string Id { get; init; } = string.Empty;
    public IdeEventoS1000Dto IdeEvento { get; init; } = new();
    public IdeEmpregadorDto IdeEmpregador { get; init; } = new();
    public ExclusaoDto Exclusao { get; init; } = new();
}