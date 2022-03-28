using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Internet_magazin.AppData;
using Internet_magazin.Models;

namespace Internet_magazin.Services
{
    public interface IItemInCartService
    {
        public void Add(Guid cartId,Guid itemId,ulong count=1);
        public void Update(ItemInCart itemInCart);
        public void Delete(Guid id, ulong count);
        public IEnumerable<ItemInCart> GetFromCart(Guid cartId);
        public ItemInCart Get(Guid id);
    }
    public class ItemInCartService:IItemInCartService
    {
        private readonly AppDataContext _db;
        public ItemInCartService(AppDataContext db)
        {
            _db = db;
        }

        public void Add(Guid cartId, Guid itemId, ulong count=1)
        {
            ItemInCart iic=new ItemInCart{Id=Guid.NewGuid(),CartId = cartId,ItemId = itemId,Count = count};
            Item i = _db.Items.FirstOrDefault(i=>i.Id==itemId);
            if (i.Count >= count)
            {
                i.Count -= count;
                if(_db.ItemInCarts.FirstOrDefault(i=>i.ItemId==itemId&&i.CartId==cartId)!=null)
                {
                    iic = _db.ItemInCarts.FirstOrDefault(i => i.ItemId == itemId && i.CartId == cartId);
                    iic.Count += count;
                    Update(iic);
                    _db.Items.Update(i);
                    _db.SaveChanges();
                    return;
                }
                _db.ItemInCarts.Add(iic);
                _db.Items.Update(i);
                _db.SaveChanges();
            }
        }
        public void Update(ItemInCart itemInCart)
        {
            _db.ItemInCarts.Update(itemInCart);
            _db.SaveChanges();
        }
        public void Delete(Guid id,ulong count)
        {
            var i = _db.ItemInCarts.FirstOrDefault(i => i.Id==id);
            if (i != null)
            {
                Item it = _db.Items.FirstOrDefault(it=>it.Id==i.ItemId);
                if (i.Count > count)
                {
                    i.Count -= count;
                    _db.ItemInCarts.Update(i);
                    
                }
                else
                {
                    _db.ItemInCarts.Remove(i);
                }
                it.Count += count;
                _db.Items.Update(it);
                _db.SaveChanges();
            }
        }
        public IEnumerable<ItemInCart> GetFromCart(Guid cartId)
        {
            return _db.ItemInCarts.Where(i=>i.CartId==cartId).Include(p => p.Item).ToList(); 
        }
        public ItemInCart Get(Guid id)
        {
            return _db.ItemInCarts.FirstOrDefault(i=>i.Id==id);
        }
    }
}