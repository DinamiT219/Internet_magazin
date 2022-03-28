using System;
using System.Collections.Generic;

namespace Internet_magazin.Models
{
    public class Cart
    {
        public Guid Id { get; set; }
        public List<ItemInCart> ItemInCarts { get; set; }
    }
}