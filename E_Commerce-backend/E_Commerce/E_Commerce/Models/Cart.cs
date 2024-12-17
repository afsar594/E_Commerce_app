namespace E_Commerce.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public string? Brand { get; set; }
        public string? FilePath { get; set; }    // New column
        public string? Category { get; set; }    // New column
        public decimal? TotalPrice { get; set; }
        public int? UserId { get; set; }
        public int? itemId { get; set; }
        public int? Qty { get; set; }






    }
}
