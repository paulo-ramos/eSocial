namespace DDDTemplate.SharedKernel.Primitives;

public class Entity
{
    public Guid Id { get; set; }

    protected Entity()
    {
    }

    protected Entity(Guid id)
    {
        Id = id;
    }
}