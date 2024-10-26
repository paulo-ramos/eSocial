using FluentValidation;

namespace Ramos.eSocial.Shared.Versao_S_1._3.Domain.Base;

public abstract class Notifiable
{
    public Notifiable()
    {
        ValidationErrors = new List<string>();
    }
    public bool IsValid { get; set; }
    public List<string> ValidationErrors { get; set; }
    
    public void Validate<T>(IValidator<T> validator) where T : class
    {
        var validationResult = validator.Validate(this as T);
        IsValid = validationResult.IsValid;
        ValidationErrors.Clear();
        ValidationErrors.AddRange(validationResult.Errors.Select(x => x.ErrorMessage));
    }

}