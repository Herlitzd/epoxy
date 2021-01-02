using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Epoxy
{
  public class RegistrationBuilder<TOutEntity>
  {
    private Dictionary<Type, LambdaExpression> Mappers = new Dictionary<Type, LambdaExpression>();
    public RegistrationBuilder<TOutEntity> With<TCompositeEntity>(Action<TCompositeEntity, TOutEntity> mapping)
    {
      Expression<Action<TCompositeEntity, TOutEntity>> wrapper =
       (TCompositeEntity composite, TOutEntity tOut) => mapping(composite, tOut);

      Mappers.Add(typeof(TCompositeEntity), wrapper);

      return this;
    }
    internal Dictionary<Type, LambdaExpression> Build()
    {
      return Mappers;
    }
  }
}
