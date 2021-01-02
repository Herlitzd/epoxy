using System.Collections.Generic;

namespace Epoxy.Example.Domain
{
  public class Customer
  {
    public int CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public IEnumerable<ShippingAddress> ShippingAddresses { get; set; }
    public IEnumerable<PaymentMethod> PaymentMethods { get; set; }
  }

  public class PaymentMethod
  {
    public int PaymentMethodId { get; set; }
    public int CustomerId { get; set; }
    public string OpaqueToken { get; set; }
  }


  public class ShippingAddress
  {
    public string StreetAddress { get; set; }
    public string PostalCode { get; set; }
  }
}