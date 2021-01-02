using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Epoxy
{
  public interface IResolver<TModelToResolve>
  {
    TModelToResolve OneFrom<TSelector, TId>(TId id);
    IEnumerable<TModelToResolve> ManyFrom<TSelector, TId>(IEnumerable<TId> ids);
  }
  public class Resolver<TModelToResolve> : IResolver<TModelToResolve>
  {
    internal Dictionary<Type, Dictionary<Type, LambdaExpression>> Mappers
                  = new Dictionary<Type, Dictionary<Type, LambdaExpression>>();

    internal Dictionary<EpoxyEntityDefinition, LocatorBase> EntityDefinitionsServiceLookup
                  = new Dictionary<EpoxyEntityDefinition, LocatorBase>();
    internal Resolver(Dictionary<Type, Dictionary<Type, LambdaExpression>> mappers, Dictionary<EpoxyEntityDefinition, Func<LocatorBase>> defsWithServices)
    {
      Mappers = mappers;
      foreach (var (key, value) in defsWithServices.Select(x => (x.Key, x.Value)))
      {
        // Get the service once in the constructor to ensure lifetimes are respected
        EntityDefinitionsServiceLookup.Add(key,value());
      }
    }
    public IEnumerable<TModelToResolve> ManyFrom<TSelector, TId>(IEnumerable<TId> ids)
    {
      throw new System.NotImplementedException();
    }

    public TModelToResolve OneFrom<TSelector, TId>(TId id)
    {
      throw new System.NotImplementedException();
    }
  }
}