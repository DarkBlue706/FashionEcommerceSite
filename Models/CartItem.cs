using System.ComponentModel.DataAnnotations;


namespace ShirtCompany.Models
{
    public class CartItem
    {
        [Key]
        public int ProductId { get; set; }
        
        public string? Name {get; set;}

        public int CartId { get; set; } // specifies user that is assoicated with the item to purchase

        public int Quantity { get; set; }

        public System.DateTime DateCreated { get; set; }

        public decimal Price { get; set; }

        public decimal Total => Quantity * Price;

        //public virtual Product Product { get; set; }

    }
    public class Cart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public decimal GrandTotal => Items.Sum(item => item.Total);
    }
}