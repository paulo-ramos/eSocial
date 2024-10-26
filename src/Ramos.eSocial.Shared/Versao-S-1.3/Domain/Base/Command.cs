namespace Ramos.eSocial.Shared.Versao_S_1._3.Domain.Base;

public abstract class Command : Notifiable
{
    protected Command()
    {
        IsValid = false;
        ValidationErrors = new List<string>();
    }
}
