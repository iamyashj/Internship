using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Visitor_Management.Models
{
    public class Visitor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required][EmailAddress] public string Email { get; set; }
        [Required] public string Name { get; set; }

        [Required] public DateTime Arrivaltime { get; set; }

        public DateTime? departuretime { get; set; }



        public ICollection<Item>? Items { get; set; }
      

    }
}
