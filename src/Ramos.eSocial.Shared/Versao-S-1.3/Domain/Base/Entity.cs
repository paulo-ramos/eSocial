namespace Ramos.eSocial.Shared.Versao_S_1._3.Domain.Base;

public abstract class Entity : Notifiable
{
    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }


    protected Entity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
        IsValid = false;
        ValidationErrors = new List<string>();
    }
}