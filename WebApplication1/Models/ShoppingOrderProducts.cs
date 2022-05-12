using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class ShoppingOrderProducts
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        
        public int quantity { get; set; }
        public string Img { get; set; }
     //   [JsonIgnore]
        public virtual Category Category { get; set; }


        public int UserOrderId { get; set; }
        [JsonIgnore]
        public virtual  UserOrder UserOrder { get; set; }

    }
}
