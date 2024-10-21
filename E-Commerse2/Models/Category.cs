using System.ComponentModel.DataAnnotations;

namespace E_Commerse2.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3,ErrorMessage ="Falid Must be have 3 character")]
        [MaxLength(100,ErrorMessage ="Falid Must Be hAVE 100 Character")]
        public string Name { get; set; }

        public ICollection<Product> Products { get; } = new List<Product>();
    }
}
