using System;
using System.Collections.Generic;
using System.Linq;
using Internet_magazin.AppData;
using Internet_magazin.Models;

namespace Internet_magazin.Services
{
    public interface IDivisionService
    {
        public IEnumerable<Division> Get();
        public Division Get(Guid id);
        public void Create(Division division);
        public void Update(Division division);
        public void Delete(Guid id);
    }

    public class DivisionService:IDivisionService
    {
        private readonly AppDataContext _db;
        public DivisionService(AppDataContext db)
        {
            _db = db;
        }

        public IEnumerable<Division> Get()
        {
            return _db.Divisions;
        }
        public Division Get(Guid id)
        {
            return _db.Divisions.FirstOrDefault(d=>d.Id==id);
        }
        public void Create(Division division)
        {
            _db.Divisions.Add(division);
            _db.SaveChanges();
        }
        public void Update(Division division)
        {
            _db.Divisions.Update(division);
            _db.SaveChanges();
        }
        public void Delete(Guid id)
        {
            _db.Divisions.Remove(_db.Divisions.FirstOrDefault(d=>d.Id==id));
            _db.SaveChanges();
        }
    }
}