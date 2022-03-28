using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Internet_magazin.ViewModels
{
    public class CreateItemVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Manufacturer { get; set; }
        public ulong Count { get; set; }
        public string ImgPath { get; set; }
        public Guid SubDivisionId { get; set; }
    }
}
