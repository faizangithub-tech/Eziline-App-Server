using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace EzilineApp.Api.CoreApplication.Models
{
    public class Role:IdentityRole<int>
    {
        public ICollection<UserRole> UserRoles {get;set;}
        
    }
}