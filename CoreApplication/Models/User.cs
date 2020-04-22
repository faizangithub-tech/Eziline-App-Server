using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
namespace EzilineApp.Api.CoreApplication.Models
{
    public class User:IdentityUser<int>
    {


       public  profile profile                 {get;set;}
       public  ICollection<UserRole> UserRoles {get;set;}
       public  ICollection<website>  websites  {get;set;}
       public  ICollection<comments> comments  {get;set;}
       
    }
}