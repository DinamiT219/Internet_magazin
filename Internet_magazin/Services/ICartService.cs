using System;
using System.Collections.Generic;
using System.Linq;
using Internet_magazin.AppData;
using Internet_magazin.Models;

namespace Internet_magazin.Services
{
    public interface ICartService
    {
        public Guid Create();
        public bool Check(Guid id);
    }

    public class CartService:ICartService
    {
        private readonly AppDataContext _db;
        public CartService(AppDataContext db)
        {
            _db = db;
        }

        public Guid Create()
        {
            Cart c=new Cart{Id = Guid.NewGuid()};
            _db.Carts.Add(c);
            _db.SaveChanges();
            return c.Id;
        }
        public bool Check(Guid id)
        {
            return _db.Carts.Contains(new Cart {Id=id});
        }
    }
}