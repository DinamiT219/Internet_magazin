using System;

namespace Internet_magazin.Models
{
    public class ItemInCart
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public Cart Cart { get; set; }
        public Guid ItemId { get; set; }
        public Item Item { get; set; }
        public ulong Count { get; set; }
    }
}