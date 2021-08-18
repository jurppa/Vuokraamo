using System;
using System.Collections.Generic;

#nullable disable

namespace Vuokraamo.Models
{
    public partial class Product
    {
        public Product()
        {
            Carts = new HashSet<Cart>();
            Rentals = new HashSet<Rental>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string Category { get; set; }
        public string Info { get; set; }
        public string Condition { get; set; }
        public int? Amount { get; set; }
        public int? Deductible { get; set; }
        public string ImageUrl { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Rental> Rentals { get; set; }
    }
}
