using System;
using System.Collections.Generic;
using System.Linq;
using Internet_magazin.AppData;
using Internet_magazin.Models;
using Microsoft.EntityFrameworkCore;

namespace Internet_magazin.Services
{
    public interface ISubDivisionService
    {
        public IEnumerable<SubDivision> Get(Guid divId);
        public IEnumerable<SubDivision> Get();
        public SubDivision GetOne(Guid subDivId);
        public void Create(SubDivision subdivision);
        public void Update(SubDivision subdivision);
        public void Delete(Guid id);
    }

    public class SubDivisionService:ISubDivisionService
    {
        private readonly AppDataContext _db;
        public SubDivisionService(AppDataContext db)
        {
            _db = db;
        }
        public IEnumerable<SubDivision> Get(Guid divId)
        {
            return _db.SubDivisions.Where(s => s.DivisionId == divId).Include(s=>s.Division);
        }
        public SubDivision GetOne(Guid subDivId)
        {
            return _db.SubDivisions.FirstOrDefault(sd=>sd.Id==subDivId);
        }
        public IEnumerable<SubDivision> Get()
        {
            return _db.SubDivisions;
        }
        public void Create(SubDivision subdivision)
        {
            _db.SubDivisions.Add(subdivision);
            _db.SaveChanges();
        }
        public void Update(SubDivision subdivision)
        {
            _db.SubDivisions.Update(subdivision);
            _db.SaveChanges();
        }
        public void Delete(Guid id)
        {
            _db.SubDivisions.Remove(_db.SubDivisions.FirstOrDefault(d => d.Id == id));
            _db.SaveChanges();
        }
    }
}