using System;
using System.Collections.Generic;

namespace Internet_magazin.Models
{
    public class Division
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<SubDivision> SubDivisions { get; set; }
    }
}