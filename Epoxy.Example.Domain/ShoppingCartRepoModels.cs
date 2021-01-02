using System;
using System.Collections.Generic;

namespace Epoxy.Example.Domain
{
  public class ShoppingCart
  {
    public Guid CartId { get; set; }
    public int CustomerId { get; set; }
    public IEnumerable<CartItem> CartItems { get; set; }
    public class CartItem
    {
      public string SKU { get; set; }
      public int Quantity { get; set; }
    }
  }
}