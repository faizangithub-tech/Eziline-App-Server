
using Microsoft.AspNetCore.Identity;
namespace EzilineApp.Api.CoreApplication.Models
{
    public class UserRole:IdentityUserRole<int>
    {

        public User User {get;set;}       
        public Role Role {get;set;}
        
    }
}