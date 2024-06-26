﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineAccountDemo.Models

{
    public class ModelIssues
    {
        public ICollection<RepairAccessories> RepairAccessories { get; set; }
        public ICollection<Inventory> Inventory { get; set; }
        public ICollection<Sales> Sales { get; set; }
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

/*        [Required]
        public int ModelId { get; set; }*/
        [Required]
        public string IssueTitle { get; set; }
        [Required]
        public string IssueCode { get; set; }
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

/*        public BrandModel BrandModel { get; set; }*/


    }


}
