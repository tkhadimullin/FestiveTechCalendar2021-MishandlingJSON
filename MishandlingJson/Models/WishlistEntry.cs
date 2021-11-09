using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MishandlingJson.Models
{
    internal class WishlistEntry
    {

        public int Id { get; set; }
        public int CustomerId { get; set; }
        [MaxLength(300)]
        public string ItemSource { get; set; }
        public string ItemData { get; set; } // no value conversion        
                                             //public ItemData ItemData { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string ItemCategory { get; set; }
    }
}
