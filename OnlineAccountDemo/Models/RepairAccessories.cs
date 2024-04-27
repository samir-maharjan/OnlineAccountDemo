using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineAccountDemo.Models

{
    public class RepairAccessories
    {
/*        public ICollection<BrandModel> BrandModel { get; set; }
        public ICollection<BrandCategory> BrandCategory { get; set; }
        public ICollection<ModelColor> ModelColor { get; set; }
        public ICollection<Employees> Employees { get; set; }
        public ICollection<ModelIssues> ModelIssues { get; set; }
        public ICollection<JobStatus> JobStatus { get; set; } */

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required]
        public int BrandId { get; set; }
        [Required]
        public int ModelId { get; set; }
        [Required]
        public int Colorid { get; set; }
        [Required]
        public int EmpId { get; set; }
        [Required]
        public int StatusId { get; set; }
        [Required]
        public int IssueId { get; set; }

        [Required]
        public string IMEINumber { get; set; }
        [Required]
        public string Remarks { get; set; }
/*        [Required]
        public double Price { get; set; }*/
        public bool Deleted { get; set; } = false;
        public bool Status { get; set; } = true;
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        public BrandCategory BrandCategory { get; set; }
        public ModelColor ModelColor { get; set; }
        public Employees Employees { get; set; }
        public JobStatus JobStatus { get; set; }
        public ModelIssues ModelIssues { get; set; }


    }


}
