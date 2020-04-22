using System.Threading.Tasks;
using AutoMapper;
using EzilineApp.Api.CoreApplication.Data;
using EzilineApp.Api.CoreApplication.DataTransferObjs.ReviewDetails;
using EzilineApp.Api.CoreApplication.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace EzilineApp.Api.Controllers
{

    [Route ("api/[controller]")]
    [ApiController]
    public class MangeReviews:ControllerBase
    {
          private readonly DataContext _dataContext;
          private readonly IMapper _mapper;
          private unitofwork _unitofwork;
          private readonly IHubContext<BroadCast> _hubcontext;


          public MangeReviews(DataContext dataContext,IMapper mapper,IHubContext<BroadCast> hubcontext)
          {
                 
                _dataContext = dataContext;
                
                _mapper=mapper;

                _hubcontext=hubcontext;
                
                _unitofwork=new unitofwork(_dataContext);
                
                _unitofwork.reviews.GetMapper(_mapper);

                _unitofwork.reviews.Inithub(hubcontext);
            
          }

       
       [Authorize (Policy ="RequireMember")]
       [HttpGet("{id}")]
       public async Task<IActionResult> GetRatings(int id)
       {
            
             dynamic result= await _unitofwork.reviews.Getsummary(id);

             return Ok(result);
       }

       [Authorize (Policy="RequireMember")]
       [HttpGet("getreviews")]
       public async Task<IActionResult> GetReviews(int webid)
       {
             var records = await _unitofwork.reviews.Getallreviews(webid);

             return Ok(records);
       }
       
       [Authorize (Policy="RequireMember")]
       [HttpPost("addreview")]
       public async Task<IActionResult> AddReview(reviewentity entity)
       {
              
                await _unitofwork.reviews.AddReview(entity); 
                                  
                return Ok(entity);
       }





    }
}