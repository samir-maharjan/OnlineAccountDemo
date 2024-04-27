using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineAccountDemo.Models

{
    public class StorageCapacity
    {
        public ICollection<Inventory> Inventory { get; set; }
        public ICollection<Sales> Sales { get; set; }
        

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string StorageTitle { get; set; }

/*        [Required]
        public string StorageCode { get; set; }
We can place it later on to select brand as a Storage code
 */
        public bool Deleted { get; set; } = false;
        public bool Status { get; set; } = true;
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}
