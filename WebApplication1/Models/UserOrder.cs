using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class UserOrder
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
       [JsonIgnore]
        public virtual ApplicationUser ApplicationUser { get; set; }
//[JsonIgnore]
        public virtual List<ShoppingOrderProducts> ShoppingOrderProducts { get; set; }

    }
}
