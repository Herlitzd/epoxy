using System.Collections.Generic;
using System.Threading.Tasks;

namespace Epoxy {
  public interface ILocator<TId,TEntity>
  {
      Task<IEnumerable<TEntity>> LocateAsync(TId id);
  }
  internal interface LocatorBase : ILocator<object, object>{
    
  }
}