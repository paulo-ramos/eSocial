namespace Ramos.eSocial.Shared.Versao_S_1._3.Domain.Entities;

public class IdePeriodo
{
    public IdePeriodo(DateTime iniValid, DateTime fimValid)
    {
        IniValid = iniValid;
        FimValid = fimValid;
    }

    public DateTime IniValid { get; private set; }
    public DateTime FimValid { get; private set; }
}