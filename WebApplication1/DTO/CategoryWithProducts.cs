using System.Collections.Generic;

namespace WebApplication1.DTO
{
    public class CategoryWithProducts
    {
        public int CatId { get; set; }
        public string CatName { get; set; }
        
        public List<string> ProdNames { get; set; }= new List<string>();

    }
}
