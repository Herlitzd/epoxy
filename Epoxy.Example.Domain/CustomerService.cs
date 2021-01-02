using System.Collections.Generic;
using System.Threading.Tasks;
namespace Epoxy.Example.Domain
{
  public class CustomerService : Epoxy.ILocator<int, Customer>
  {
    public Task<IEnumerable<Customer>> LocateAsync(int id)
    {
      throw new System.NotImplementedException();
    }
  }
}