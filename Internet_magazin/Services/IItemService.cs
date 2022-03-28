using System;
using System.Collections.Generic;
using System.Linq;
using Internet_magazin.AppData;
using Internet_magazin.Enums;
using Internet_magazin.Models;
using Microsoft.EntityFrameworkCore;

namespace Internet_magazin.Services
{
    public interface IItemService
    {
        public IEnumerable<Item> GetList(Guid subDivisionId, string search="", Sort sort= Sort.Null);
        public Item GetOne(Guid id);
        public void Create(Item item);
        public void Update(Item item);
        public void Delete(Guid id);
    }
    public class ItemService:IItemService
    {
        private readonly AppDataContext _db;
        public ItemService(AppDataContext db)
        {
            _db = db;
        }
        public IEnumerable<Item> GetList(Guid subDivisionId,string search="",Sort sort=Sort.Null)
        {
            IEnumerable<Item> select;
            if (subDivisionId != Guid.Empty)
            {
               select = _db.Items.Where(i=>i.SubDivisionId==subDivisionId);
            }
            else
            {
                select = _db.Items;
            }
            select = select.Where(i => i.Name.Contains(search));
            switch (sort)
            {
                case Sort.ByDate:
                    select.OrderBy(i=>i.DateOfAdding);
                    break;
                case Sort.ByDateRev:
                    select.OrderByDescending(i => i.DateOfAdding);
                    break;
                case Sort.ByName:
                    select.OrderBy(i=>i.Name);
                    break;
                case Sort.ByNameRev:
                    select.OrderByDescending(i => i.Name);
                    break;
                case Sort.ByPrice:
                    select.OrderBy(i=>i.Price);
                    break;
                case Sort.ByPriceRev:
                    select.OrderByDescending(i => i.Price);
                    break;
            }
            return select;
        }
        public Item GetOne(Guid id)
        {
            return _db.Items.FirstOrDefault(i=>i.Id==id);
        }

        public void Create(Item item)
        {
            _db.Items.Add(item);
            _db.SaveChanges();
        }

        public void Update(Item item)
        {
            _db.Items.Update(item);
            _db.SaveChanges();
        }

        public void Delete(Guid id)
        {
            if(_db.Items.FirstOrDefault(i => i.Id == id)!=null)
                _db.Items.Remove(_db.Items.FirstOrDefault(i=>i.Id==id));
            _db.SaveChanges();
        }
    }
}