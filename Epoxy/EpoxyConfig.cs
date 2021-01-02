using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace Epoxy
{
  public class EpoxyConfig
  {
    // Set of Locators for Types
    internal HashSet<EpoxyEntityDefinition> EntityDefinitions
                = new HashSet<EpoxyEntityDefinition>();
    // Lookup from Locator Output type to Final Output type
    internal Dictionary<Type, Dictionary<Type, LambdaExpression>> Mappers
                  = new Dictionary<Type, Dictionary<Type, LambdaExpression>>();

    public EpoxyConfig Add<TEntity, TId>(Epoxy<TEntity, TId> epoxyEntityDeclaration)
    {
      EntityDefinitions.Add(epoxyEntityDeclaration.GetDefinition());
      return this;
    }

    public void Register<TOutEntity>(Action<RegistrationBuilder<TOutEntity>> builderSetup)
    {
      var builderRef = new RegistrationBuilder<TOutEntity>();
      builderSetup(builderRef);
      Mappers.Add(typeof(TOutEntity), builderRef.Build());
    }

  }
}