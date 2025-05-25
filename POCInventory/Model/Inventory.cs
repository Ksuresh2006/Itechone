using System.ComponentModel.DataAnnotations;

namespace POCInventory.Model
{
    public class Inventory
    {
        [Key]
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public string? Productdescription { get; set; }
        public string ProductUOM { get; set; }
        public double? UnitPrice { get; set; } = 0.0;
        public double? OpeningBalance { get; set; }
        public double? Quantity { get; set; }
        public double? TaxAmt { get; set; }
        public double TaxPer { get; set; }=0.0;
        public string? ProductCatagory { get; set; }
        public string? ProductCode { get; set; }
        public string? HSNNo { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; } = "Admin";
        public string? ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }=DateTime.Now; 
        public DateTime? ModifiedDate { get; set; }
      
    }
}
