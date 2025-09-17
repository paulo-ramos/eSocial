using System.Linq.Expressions;
using DDDTemplate.SharedKernel.Primitives;

namespace DDDTemplate.Persistence.Specifications;

public abstract class Specification<TEntity>
    where TEntity : Entity
{
    protected abstract Expression<Func<TEntity, bool>> ToExpression();

    internal bool IsSatisfiedBy(TEntity entity) => ToExpression().Compile()(entity);

    public static implicit operator Expression<Func<TEntity, bool>>(Specification<TEntity> specification) =>
        specification.ToExpression();
}