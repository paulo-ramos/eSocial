using System.Diagnostics;
using System.Text.Json.Serialization;
using FluentValidation;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ramos.eSocial.S1000.Shared.Entities.Base;

[BsonIgnoreExtraElements]
public abstract class Entity
{
    public DateTime CreatedAt { get; private set; }
    
    public DateTime UpdatedAt { get; private set; }
    
    [BsonIgnore] 
    [JsonIgnore]
    public bool IsValid { get; private set; }
    
    [JsonIgnore]
    [BsonIgnore] 
    public List<string> ValidationErrors { get; set; }


    protected Entity()
    {
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