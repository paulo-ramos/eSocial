namespace Ramos.eSocial.Shared.Domain.Models;

public class IdePeriodo
{
    public DateTime IniValid { get; }
    public DateTime? FimValid { get; private set; }
    
    public IdePeriodo(DateTime iniValid, DateTime fimValid)
    {
        IniValid = iniValid;
        FimValid = fimValid != DateTime.MinValue ? fimValid : DateTime.MaxValue;
    }
    
    public void SetFimValidade(DateTime fimValid)
    {
        this.FimValid = fimValid != DateTime.MinValue ? fimValid : DateTime.MaxValue;
    }


}