using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreConsoleApp.Models
{
    public class PriceOffer
    {
        public int PriceOfferId { get; set; }
        public int NewPrice { get; set; }

        public string PromotionalText { get; set; }
        // Relationships  Book entity to PriceOffer
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
