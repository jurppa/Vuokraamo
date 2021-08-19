using System;
using System.Collections.Generic;

#nullable disable

namespace Vuokraamo.Models
{
    public partial class Invoice
    {
        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }
        public int RentalId { get; set; }
        public DateTime DueDate { get; set; }
        public int TotalDue { get; set; }
        public bool? Paid { get; set; }
    }
}
