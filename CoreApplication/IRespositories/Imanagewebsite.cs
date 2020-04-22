using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EzilineApp.Api.CoreApplication.DataTransferObjs.admindtos;
using EzilineApp.Api.CoreApplication.Models;

namespace EzilineApp.Api.CoreApplication.IRespositories
{
    public interface Imanagewebsite:IGeneric<website>
    {
        Task<website> setentity(websiteentity entity);

        Task<List<websiteentity>> Getallwebsites ();
        Task<websiteentity> UpdateEntity(websiteentity entity);

        Task<websiteentity> Getdetails(int id);
        Task Deletewebsite(int id);
        void GetMapper(IMapper mapper);
    }
}