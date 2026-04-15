using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentImporter.Domain
{
    public class DocumentItem
    {
        public int Id { get; set; }
        public int DocumentId { get; set; }
        public int Ordinal { get; set; }
        public string Product { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int TaxRate { get; set; }

        // Właściwości wyliczane
        public decimal NetValue => Price;
        public decimal GrossValue => NetValue * (1 + TaxRate / 100m);

        public Document Document { get; set; } = null!;
    }
}
