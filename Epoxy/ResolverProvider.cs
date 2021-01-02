using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Epoxy
{
  public class ResolverProvider
  {
    private EpoxyConfig EpoxyConfig { get; }

    public ResolverProvider(EpoxyConfig epoxyConfig)
    {
      this.EpoxyConfig = epoxyConfig;
    }

    public IResolver<TViewModel> ResolverFactory<TViewModel>(IServiceProvider services)
    {
      var defsWithServices = new Dictionary<EpoxyEntityDefinition, Func<LocatorBase>>();
      if (EpoxyConfig.Mappers.ContainsKey(typeof(TViewModel)))
      {
        var requiredTypesToResolve = EpoxyConfig.Mappers[typeof(TViewModel)].Keys;
        var typesToResolveLocatorsFor = new HashSet<EpoxyEntityDefinition>();
        foreach (var type in requiredTypesToResolve)
        {
          Type typeToResolveLocatorFor;
          if (typeof(IEnumerable).IsAssignableFrom(type))
          {
            typeToResolveLocatorFor = type.GenericTypeArguments.FirstOrDefault();
          }
          else
          {
            typeToResolveLocatorFor = type;
          }
          typesToResolveLocatorsFor.Add(EpoxyConfig.EntityDefinitions.FirstOrDefault(x => x.EntityType == typeToResolveLocatorFor));
        }
        foreach (var definition in typesToResolveLocatorsFor)
        {
          // This should reference the an ILocator<T> not an Impl of ILocator<T>
          Func<LocatorBase> svc = () => (LocatorBase) services.GetService(definition.LocatorType);
          defsWithServices.Add(definition, svc);
        }
        return new Resolver<TViewModel>(EpoxyConfig.Mappers, defsWithServices);
      }
      throw new NotImplementedException($"A Resolver is not registered for type {typeof(TViewModel).FullName}");
    }
  }
}