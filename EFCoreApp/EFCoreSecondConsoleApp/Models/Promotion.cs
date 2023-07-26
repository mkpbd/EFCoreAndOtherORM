using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreSecondConsoleApp.Models
{
    public class Promotion
    {
        public int PromotionId { get; set; }
        public int NewPrice { get; set; }
        public int PromotionType { get; set; }
        public string? PromotionalText { get; set; }

        public int BookId { get; set; }
        public ICollection<Book> Books { get; set;}
    }
}
