using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EzilineApp.Api.CoreApplication.Data;
using EzilineApp.Api.CoreApplication.DataTransferObjs.admindtos;
using EzilineApp.Api.CoreApplication.IRespositories;
using EzilineApp.Api.CoreApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace EzilineApp.Api.CoreApplication.Repositories
{
    public class Admin : Generic<UserRole>, IAdmin
    {
        private readonly DataContext _dataContext;
        private IMapper _mapper;
        public Admin(DataContext dataContext) : base(dataContext)
        {
             
             _dataContext=dataContext;

        }
        public void GetMapper(IMapper mapper)
        {
            _mapper=mapper;
        }
        public async Task EditUserRoles(editroles edit)
        {


                List<Role>     rollist   = new List<Role>();
               
                List<UserRole> usersrols = new List<UserRole>();

                    var roles = await _dataContext.UserRoles
                                                  .Where(x=>x.UserId==edit.id)
                                                  .ToListAsync();

                                      _dataContext.UserRoles
                                                  .RemoveRange(roles);
                            
                                await _dataContext.SaveChangesAsync();

                List<User> user =_dataContext.Users
                                         .Where(x=>x.Id==edit.id)
                                         .ToList();

                for(int i=0;i<edit.roles.Count();i++)
                {

                       var role =_dataContext.Roles
                                             .Where(x=>x.Name==edit.roles[i])
                                             .ToList();
                           rollist.Add(role[0]);                                                       
                }

                for(int i=0;i<rollist.Count();i++)
                {
                       var entity  =_mapper.Map<User,UserRole>(user[0]);
                                    _mapper.Map<Role,UserRole>(rollist[i],entity); 
                       
                           usersrols.Add(entity);
                }

                if(usersrols.Any())
                {
                        await _dataContext.UserRoles.AddRangeAsync(usersrols);
                        await _dataContext.SaveChangesAsync();
                }
        }


        public async Task<object> GetUsersAlongRoles()
        {
               
            var userroles = await (from user in _dataContext.Users
            select new
            {
                    id       = user.Id,
                    username = user.UserName,
                    imgurl   = user.profile.imgurl,
                    Roles = ( from role in user.UserRoles
                              join userrole in _dataContext.Roles
                              on role.RoleId equals userrole.Id
                              select userrole.Name).ToList ()
            }).ToListAsync ();

            return userroles;
        }
    }
}