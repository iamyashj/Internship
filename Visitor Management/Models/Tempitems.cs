using System.ComponentModel.DataAnnotations;

namespace Visitor_Management.Models
{
    public class Tempitems
    {
        public string Item_Name { get; set; }
        public int Qty { get; set; } = 1;




     [Key]   public int SrNO { get; set; }
    }
}
