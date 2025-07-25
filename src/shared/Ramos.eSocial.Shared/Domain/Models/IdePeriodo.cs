namespace Ramos.eSocial.Shared.Domain.Models;

public class IdePeriodo
{
    public DateTime IniValid { get; }
    public DateTime FimValid { get; private set; }
    
    public IdePeriodo(DateTime iniValid, DateTime fimValid)
    {
        IniValid = iniValid.Date;
        SetFimValidade(fimValid);
    }
    
    public void SetFimValidade(DateTime fimValid)
    {
        if (fimValid < IniValid)
            throw new InvalidOperationException("Fim da vigência deve ser posterior à data inicial.");
        
        FimValid = fimValid == DateTime.MinValue ? DateTime.MaxValue.Date : fimValid.Date;

    }
}