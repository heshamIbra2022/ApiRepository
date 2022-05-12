
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
       [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public int quantity { get; set; }
        public string Img { get; set; }
        [JsonIgnore]
     public virtual Category Category { get; set; }
    }
}
