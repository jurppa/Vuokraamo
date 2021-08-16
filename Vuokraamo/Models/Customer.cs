using System;
using System.Collections.Generic;

#nullable disable

namespace Vuokraamo.Models
{
    public partial class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public string Address { get; set; }
        public int? ZipCode { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public string Password { get; set; }
    }
}
