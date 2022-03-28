using System;
using System.Collections.Generic;
using System.Linq;
using Internet_magazin.AppData;
using Internet_magazin.Models;
using Microsoft.EntityFrameworkCore;

namespace Internet_magazin.Services
{
    public interface IOrderService
    {
        public void Create(Order order);
        public IEnumerable<Order> GetAll(Guid userId);
        public IEnumerable<Order> GetAll();
        public Order Get(Guid id);
        public void Update(Order order);
        public void Delete(Guid id);
    }

    public class OrderService : IOrderService
    {
        private readonly AppDataContext _db;
        public OrderService(AppDataContext db)
        {
            _db = db;
        }
        public void Create(Order order)
        {
            _db.Orders.Add(order);
            _db.SaveChanges();
        }
        public IEnumerable<Order> GetAll(Guid accountId)
        {
            return _db.Orders.Where(o=>o.AccountId== accountId);
        }
        public IEnumerable<Order> GetAll()
        {
            return _db.Orders;
        }
        public Order Get(Guid id)
        {
            return _db.Orders.FirstOrDefault(o=>o.Id==id);
        }
        public void Update(Order order)
        {
            _db.Orders.Add(order);
            _db.SaveChanges();
        }
        public void Delete(Guid id)
        {
            _db.Orders.Remove(_db.Orders.FirstOrDefault(o=>o.Id==id));
            _db.SaveChanges();
        }
    }
}