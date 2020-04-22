using System.Collections.Generic;
using System.Linq;
using EzilineApp.Api.CoreApplication.Models;
using Microsoft.AspNetCore.Identity;

namespace EzilineApp.Api.CoreApplication.Data {
    public class SeedData {
        private readonly UserManager<User> _usermanager;
        private readonly RoleManager<Role> _roleManager;
        public SeedData (UserManager<User> usermanager, RoleManager<Role> roleManager) 
        {    
            _roleManager = roleManager;
            _usermanager = usermanager;
        }
        public void SeedingData () 
        {

            if (!_usermanager.Users.Any ()) 
            {
                List<User> users = new List<User> ();
                users.Add (new User () {
                    UserName = "Fasih",
                        Email = "faizanalam335@outlook.com",
                        PhoneNumber = "0306-5053875"
                });
                users.Add (new User () {
                    UserName = "Mohammad",
                        Email = "faizanraza113@gmail.com",
                        PhoneNumber = "0335-1993431"
                });

                users.Add (new User () {
                    UserName = "Shoaib",
                        Email = "naumanalam335@hotmail.com",
                        PhoneNumber = "0305-8975458"
                });
                users.Add (new User () {
                    UserName = "Shakeel",
                        Email = "shakeelalam335@outlook.com",
                        PhoneNumber = "0300-5053825"
                });
                users.Add (new User () {
                    UserName = "Baig",
                        Email = "faizanbaig113@gmail.com",
                        PhoneNumber = "0335-1992201"
                });

                users.Add (new User () {
                    UserName = "Owaisi",
                        Email = "naumanowaisi335@hotmail.com",
                        PhoneNumber = "0305-8978458"
                });

                var roles = new List<Role> {
                    new Role { Name = "Member" },
                    new Role { Name = "Admin" },
                    new Role { Name = "Moderator" },
                    new Role { Name = "Vip" }
                };

                foreach (var role in roles) 
                {
                     _roleManager.CreateAsync(role)
                                 .Wait();
                }

                for (int i = 0; i < users.Count (); i++) 
                {
                    _usermanager.CreateAsync (users[i], "password")
                                .Wait ();

                    _usermanager.AddToRoleAsync(users[i],"Member")
                                .Wait();
                }

                var adminuser=new User
                {
                    UserName="Admin",
                    Email="adminomanbaig@hotmail.com"
                };

                IdentityResult result=_usermanager.CreateAsync(adminuser,"password")
                                                  .Result;

                if(result.Succeeded)
                {
                    var admin=_usermanager.FindByNameAsync("Admin").Result;
                        
                        _usermanager.AddToRolesAsync(admin, new[] {"Admin","Moderator"}).Wait();
                }

            }

        }
    }
}