using Microsoft.AspNetCore.Identity;

namespace CowRationWebApplication
{
    public class User:IdentityUser
    {
        public string OrganizationName { get; set; }
    }
}