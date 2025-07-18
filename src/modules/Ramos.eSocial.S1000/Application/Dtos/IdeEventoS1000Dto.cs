using Ramos.eSocial.Shared.Domain.Enums;

namespace Ramos.eSocial.S1000.Application.Dtos;

public record class IdeEventoS1000Dto
{
    public EAmbiente TpAmb { get; init; }
    public EProcEmissao ProcEmi { get; init; }
    public string VerProc { get; init; } = string.Empty;
}