using System;

namespace Internet_magazin.Models
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Manufacturer { get; set; }
        public ulong Count { get; set; }
        public string ImgPath { get; set; }
        public DateTime DateOfAdding { get; set; }
        public Guid SubDivisionId { get; set; }
        public SubDivision SubDivision { get; set; }
    }
}