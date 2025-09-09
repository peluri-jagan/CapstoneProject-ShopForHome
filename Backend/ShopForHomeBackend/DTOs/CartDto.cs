// Backend/DTOs/CartDto.cs
namespace ShopForHomeBackend.DTOs
{

    public class CartDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }         

        public decimal Price { get; set; }
        public string ImageUrl { get; set; }                                    


    }
    
}
