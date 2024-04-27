
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineAccountDemo.Models

{
    public class Sales
    {
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
        public int StorageId { get; set; }
        [Required]
        public double Rate { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double Total { get; set; }
        [Required]
        public string IMEINumber { get; set; }
        [Required]
        public string InvoiceNum { get; set; }
        [Required]
        public string Remarks { get; set; }
        [Required]
        public string PaymentMode { get; set; }
        public bool Deleted { get; set; } = false;
        public bool Status { get; set; } = true;
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        public BrandCategory BrandCategory { get; set; }
        public ModelColor ModelColor { get; set; }
        public Employees Employees { get; set; }
        public StorageCapacity StorageCapacity { get; set; }

    }


}
