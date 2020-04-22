using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EzilineApp.Api.CoreApplication.DataTransferObjs.ReviewDetails;
using EzilineApp.Api.CoreApplication.Models;
using Microsoft.AspNetCore.SignalR;

namespace EzilineApp.Api.CoreApplication.IRespositories
{
    public interface Imanagereviews:IGeneric<ratings>
    {

          Task<object> Getsummary(int id);
          Task AddReview(reviewentity entity);
          Task<List<WebsiteReview>> Getallreviews(int id);
          void GetMapper(IMapper mapper);
          void Inithub(IHubContext<BroadCast> hubContext);
          
    }



}