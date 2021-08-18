using System;
using System.Collections.Generic;

#nullable disable

namespace Vuokraamo.Models
{
    public partial class Rental
    {
        public int RentalId { get; set; }
        public int? CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Price { get; set; }
        public DateTime Rentaldate { get; set; }
        public DateTime Returndate { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
    }
}
