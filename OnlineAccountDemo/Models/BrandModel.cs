using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineAccountDemo.Models

{
    public class BrandModel
    {

/*        public ICollection<ModelColor> ModelColor { get; set; }
        public ICollection<ModelIssues> ModelIssues { get; set; }*/
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int BrandId { get; set; }
        [Required]
        public string ModelTitle { get; set; }
        [Required]
        public string ModelCode { get; set; }
/*        [Required]
        public string Address { get; set; }
        [Required]
        [MinLength(5)]
        public string Password { get; set; }*/
        public bool Deleted { get; set; } = false;
        public bool Status { get; set; } = true;
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        public BrandCategory BrandCategory { get; set; }


    }


}
