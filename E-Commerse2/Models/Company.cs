namespace E_Commerse2.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public ICollection<Category>categories { get;  }=new List<Category>();
    }
}
