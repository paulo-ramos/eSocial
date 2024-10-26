using FluentValidation;

namespace Ramos.eSocial.S1000.Shared.Entities.Base;

public abstract class Entity
{
    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public bool IsValid { get; private set; }
    public List<string> ValidationErrors { get; set; }


    protected Entity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
        IsValid = false;
        ValidationErrors = new List<string>();
    }

    public void Validate<T>(IValidator<T> validator) where T : Entity
    {
        var validationResult = validator.Validate(this as T);
        IsValid = validationResult.IsValid;
        ValidationErrors.Clear();
        ValidationErrors.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));

    }
}