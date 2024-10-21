using System.ComponentModel.DataAnnotations;

namespace E_Commerse2.Models
{
    public class Product
    {
       
            public int Id { get; set; }

            [Required]
            [MinLength(3)]
            [MaxLength(255)]
            public string Name { get; set; }
            [Required]
            public string Description { get; set; }
            [Range(0,100000)]
            public decimal Price { get; set; }
            public string ImgUrl { get; set; }
            [Range(0,5)]
            public double Rate { get; set; }

            public int CategoryId { get; set; }
            public Category Category { get; set; }
        
    }
}
