using System;
using System.Collections.Generic;
using System.Linq;
using Internet_magazin.AppData;
using Internet_magazin.Enums;
using Internet_magazin.Models;

namespace Internet_magazin.Services
{
    public interface IAccountService
    {
        public void Create(Account acc);
        public void Update(Account account);
        public Account Get(Guid id);
        public bool CheckForLoginAndEmail(string login, string email);
        public IEnumerable<Account> GetList();
        public void Delete(Guid id);
        public Account LogIn(string login, string password);
        public bool IsAdmin(Guid id);
    }

    public class AccountService : IAccountService
    {
        private readonly AppDataContext _db;
        public AccountService(AppDataContext db)
        {
            _db = db;
        }
        public void Create(Account acc)
        {
            _db.Accounts.Add(acc);
            _db.SaveChanges();
        }
        public void Update(Account account)
        {
            _db.Accounts.Update(account);
            _db.SaveChanges();
        }
        public Account Get(Guid id)
        {
            return _db.Accounts.FirstOrDefault(u=>u.Id==id);
        }
        public IEnumerable<Account> GetList()
        {
            return _db.Accounts;
        }
        public void Delete(Guid id)
        {
            _db.Accounts.Remove(_db.Accounts.FirstOrDefault(acc=>acc.Id==id));
            _db.SaveChanges();
        }
        public bool CheckForLoginAndEmail(string login, string email)
        {
            return _db.Accounts.Any(u => u.Login == login && u.Email == email);
        }
        public Account LogIn (string login,string password)
        {
            return _db.Accounts.FirstOrDefault(acc=>acc.Login==login&&acc.Password==password);
        }
        public bool IsAdmin(Guid id)
        {
            if (_db.Accounts.Any(a => a.Id == id))
                return _db.Accounts.FirstOrDefault(a => a.Id == id).Role == Role.Admin;
            return false;
        }
    }
}