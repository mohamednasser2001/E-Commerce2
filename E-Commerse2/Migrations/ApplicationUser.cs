using Microsoft.AspNetCore.Identity;

namespace E_Commerse2.Migrations
{
    public class ApplicationUser:IdentityUser
    {
        public string Address { get; set; }
    }
}
