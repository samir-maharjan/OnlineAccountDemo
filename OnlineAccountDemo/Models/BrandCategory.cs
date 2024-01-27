﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineAccountDemo.Models

{
    public class BrandCategory
    {
        public ICollection<BrandModel> BrandModel { get; set; }
        public ICollection<RepairAccessories> RepairAccessories { get; set; }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required]
        public string BrandTitle { get; set; }
        [Required]
        public string BrandCode { get; set; }
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



    }


}