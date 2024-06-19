using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Visitor_Management.Models
{
    public class Item
    {
        private static int counter = 0;
        public string Item_Name { get; set; }
        public int Qty { get; set; } = 1;




        public int SrNO { get; set; }

     
       [Key] public int Id { get; set; }
        
        public virtual Visitor Visitor { get; set; }
        public Item()
        {
            SrNO = ++counter;
        }


    }
}
