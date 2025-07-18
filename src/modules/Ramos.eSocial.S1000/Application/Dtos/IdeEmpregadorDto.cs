using Ramos.eSocial.Shared.Domain.Enums;

namespace Ramos.eSocial.S1000.Application.Dtos;

public record class IdeEmpregadorDto
{
    public ETipoInscricao TpInsc { get; init; }
    public string NrInsc { get; init; } = string.Empty;
}