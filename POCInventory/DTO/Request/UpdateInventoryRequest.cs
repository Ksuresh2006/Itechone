namespace POCInventory.DTO.Request
{
    public class UpdateInventoryRequest
    {
        public long ProductId { get; set; }
        public string? HSNNo { get; set; }
        public string? ProductCode { get; set; }
        public string? ProductName { get; set; }
        public string? Productdescription { get; set; }
        public double? UnitPrice { get; set; }
        public string? ProductUOM { get; set; }
        public double? OpeningBalance { get; set; }
        public double? Quantity { get; set; }
        public double? TaxPer { get; set; }
        public string? ProductCatagory { get; set; }
    }
}
