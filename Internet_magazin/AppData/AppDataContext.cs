using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Internet_magazin.Models;
using Microsoft.EntityFrameworkCore;

namespace Internet_magazin.AppData
{
    public class AppDataContext:DbContext
    {
        public AppDataContext()
        {
        }
        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
        {
        }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemInCart> ItemInCarts  { get; set; }
        public DbSet<ItemInOrder> ItemInOrders { get; set; }
        public DbSet<SubDivision> SubDivisions { get; set; }
        public DbSet<Account>Accounts  { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
