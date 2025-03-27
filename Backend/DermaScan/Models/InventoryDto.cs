namespace DermaScan.Models
{
    public class InventoryDto
    {
        #region Properties
        public int ProductId { get; set; }
        public string ProductType { get; set; }
        public string ProductName { get; set; }
        public DateOnly ExpireDate { get; set; }
        #endregion
    }
}
