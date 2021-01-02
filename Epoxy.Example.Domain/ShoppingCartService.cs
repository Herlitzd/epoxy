using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Epoxy.Example.Domain
{
  public class ShoppingCartService : Epoxy.ILocator<Guid, ShoppingCart>
  {
    public Task<IEnumerable<ShoppingCart>> LocateAsync(Guid id)
    {
      throw new System.NotImplementedException();
    }
  }
}