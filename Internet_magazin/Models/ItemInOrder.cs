using System;

namespace Internet_magazin.Models
{
    public class ItemInOrder
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Guid ItemId { get; set; }
        public Item Item { get; set; }
        public ulong Count { get; set; }
    }
}