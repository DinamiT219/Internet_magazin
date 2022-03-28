using System;
using System.Linq;
using Internet_magazin.AppData;
using Internet_magazin.Models;
using Microsoft.EntityFrameworkCore;

namespace Internet_magazin.Services
{
    public interface IItemInOrderService
    {
        public void MoveFromCart(Guid cartId,Guid orderId);
    }

    public class ItemInOrderService : IItemInOrderService
    {
        private readonly AppDataContext _db;
        public ItemInOrderService(AppDataContext db)
        {
            _db = db;
        }
        public void MoveFromCart(Guid cartId, Guid orderId)
        {
            var items = _db.ItemInCarts.Where(i => i.CartId == cartId).Include(i=>i.Item).Select(i => new ItemInOrder {Id = i.Id, Count = i.Count, ItemId = i.ItemId, OrderId = orderId,Item = i.Item});
            _db.ItemInOrders.AddRange(items);
            _db.ItemInCarts.RemoveRange(items.Select(i => new ItemInCart { Id = i.Id, Count = i.Count, ItemId = i.ItemId, CartId  = cartId }));
            var order = _db.Orders.FirstOrDefault(o=>o.Id==orderId);
            order.Count = Convert.ToUInt64(items.Count());
            order.Cost = items.Sum(i=>i.Count*i.Item.Price);
            _db.Orders.Update(order);
            _db.SaveChanges();
        }
    }
}