using System;
using System.Collections.Generic;

#nullable disable

namespace Vuokraamo.Models
{
    public partial class Message
    {
        public int MessageId { get; set; }
        public int CustomerId { get; set; }
        public string Title { get; set; }
        public string Message1 { get; set; }
        public bool? Done { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
