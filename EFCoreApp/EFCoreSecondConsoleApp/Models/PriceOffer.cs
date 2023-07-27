using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreSecondConsoleApp.Models
{
    public class PriceOffer
    {

        /**
         * 
         * PriceOffer, if present, is designed to override the normal price.
         */
        public int PriceOfferId { get; set; }
        public decimal NewPrice { get; set; }
        public string? PromotionalText { get; set; }
        //-----------------------------------------------
        //Relationships
        //Foreign key back to the book it should be applied to
        public int BookId { get; set; }
    }
}
