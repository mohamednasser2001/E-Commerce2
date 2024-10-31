using System.ComponentModel.DataAnnotations;

namespace E_Commerse2.ViewModel
{
    public class LoginVM
    {
        public int Id { get; set; }
        [Required]
        public String User { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public String Password { get; set; }
        public bool  Remembere { get; set; }
    }
}
