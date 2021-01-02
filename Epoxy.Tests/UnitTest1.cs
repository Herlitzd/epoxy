using System;
using Epoxy.Example.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Epoxy.Tests
{
  [TestClass]
  public class UnitTest1
  {
    public UnitTest1(IResolver<CustomerCartViewModel> customerCartResolver)
    {
      var c = customerCartResolver.ManyFrom<Customer, int>(new int[] { 1, 2 });
      var cs = customerCartResolver.OneFrom<Customer, int>(1);
    }
    [TestMethod]
    public void TestMethod1()
    {

      new Epoxy<Customer, int>()
          .WithKey(x => x.CustomerId)
          .AddLocator<CustomerService>()
          .WithRelationship<ShoppingCart, int>(x => x.CustomerId);

      new Epoxy<ShoppingCart, Guid>()
          .WithKey(x => x.CartId)
          .AddLocator<ShoppingCartService>()
          .WithRelationship<Customer, int>(x => x.CustomerId);


    }
  }
}
