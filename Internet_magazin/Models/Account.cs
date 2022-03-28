using System;
using Internet_magazin.Enums;

namespace Internet_magazin.Models
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public DateTime DateOfRegistration { get; set; }
        public Guid CartId { get; set; }
        public Cart Cart { get; set; }
    }
}