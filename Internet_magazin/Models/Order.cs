using System;
using System.Collections.Generic;

namespace Internet_magazin.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid? AccountId { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string BuyerName { get; set; }
        public string BuyerLastName { get; set; }
        public string Email { get; set; }
        public string Сondition { get; set; }
        public ulong Count { get; set; }
        public decimal Cost { get; set; }
        public DateTime Date { get; set; } 
        public List<ItemInOrder> ItemInOrders { get; set; }
    }
}