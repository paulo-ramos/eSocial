namespace Ramos.eSocial.S1000.Domain.ValueObjects;

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