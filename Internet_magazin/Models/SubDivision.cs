using System;
using System.Collections.Generic;

namespace Internet_magazin.Models
{
    public class SubDivision
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid DivisionId { get; set; }
        public Division Division { get; set; }
        public List<Item> Items { get; set; }
    }
}