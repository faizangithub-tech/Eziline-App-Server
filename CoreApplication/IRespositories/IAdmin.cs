using System.Threading.Tasks;
using AutoMapper;
using EzilineApp.Api.CoreApplication.DataTransferObjs.admindtos;
using EzilineApp.Api.CoreApplication.Models;

namespace EzilineApp.Api.CoreApplication.IRespositories
{
    public interface IAdmin:IGeneric<UserRole>
    {
        Task EditUserRoles(editroles edit);
        Task<object> GetUsersAlongRoles();
        void GetMapper(IMapper mapper);
    } 
    
}