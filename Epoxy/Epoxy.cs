using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Epoxy
{
  public class Epoxy<TEntity, TId>
  {
    private Func<TEntity, TId> KeySelector { get; set; }
    private Type LocatorType { get; set; }
    private Dictionary<RelationshipMap, LambdaExpression> KnownReferences { get; set; } = new Dictionary<RelationshipMap, LambdaExpression>();

    public Epoxy<TEntity, TId> AddLocator<T>() where T : ILocator<TId, TEntity>
    {
      LocatorType = typeof(T);
      return this;
    }
    public Epoxy<TEntity, TId> WithKey(Func<TEntity, TId> keySelector)
    {
      if (keySelector is null)
      {
        throw new ArgumentNullException(nameof(keySelector));
      }
      KeySelector = keySelector;
      return this;
    }

    public Epoxy<TEntity, TId> WithRelationship<TReference, TfkId>(Expression<Func<TReference, TfkId>> p)
    {
      KnownReferences.Add(new RelationshipMap
      {
        ReferenceType = typeof(TReference),
        ReferenceIdType = typeof(TfkId)
      }, p);

      return this;
    }
    public EpoxyEntityDefinition GetDefinition()
    {
      return new EpoxyEntityDefinition
      {
        EntityIdType = typeof(TId),
        EntityType = typeof(TEntity),
        LocatorType = LocatorType
      };
    }
  }
}
