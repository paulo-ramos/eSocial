namespace Ramos.eSocial.S1000.Application.Dtos;

public record class IdePeriodoDto
{
    public DateTime IniValid { get; init; } = DateTime.Now;
    public DateTime FimValid { get; set; } = DateTime.MaxValue;
}