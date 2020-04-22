using System.Threading.Tasks;
using AutoMapper;
using EzilineApp.Api.CoreApplication.Data;
using EzilineApp.Api.CoreApplication.DataTransferObjs.admindtos;
using EzilineApp.Api.CoreApplication.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EzilineApp.Api.Controllers
{
    
    [Route ("api/[controller]")]
    [ApiController]
    public class ManageSitesController:ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private unitofwork _unitofwork;

        public ManageSitesController(DataContext dataContext, IMapper mapper)
        {
                _dataContext = dataContext;
                _mapper=mapper;
                _unitofwork=new unitofwork(_dataContext);
                _unitofwork.website.GetMapper(_mapper);
        }

        [Authorize (Policy = "RequireAdmin")]
        [HttpPost ("addwebsite")]
        public async Task<IActionResult> Addwebsite(websiteentity entity)
        {
              
              var coreobj= await _unitofwork.website.setentity(entity);

              var mappedobj=_mapper.Map<websiteentity>(coreobj);
            
               
              return Ok(mappedobj);
        }


        [Authorize (Policy = "RequireMember")]
        [HttpGet ("getwebsite")]
        public async Task<IActionResult> Getallwebsites()
        {
                 
             var objlist=await _unitofwork.website.Getallwebsites();

             return Ok(objlist);
        }
   
        [Authorize (Policy ="RequireMember")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Getdetails(int id)
        {

             var obj=await _unitofwork.website.Getdetails(id);

            
             return Ok(obj);
        }

        [Authorize (Policy = "RequireAdmin")]
        [HttpPut ("editwebsite")]
        public async Task<IActionResult> Updatewebsite(websiteentity entity)
        {
             var result=await _unitofwork.website
                                         .UpdateEntity(entity);
               
            return Ok(result);
        }

        [Authorize  (Policy="RequireAdmin")]
        [HttpDelete ("{id}")]
        public async Task<IActionResult> Deletewebsite(int id)
        {
             
              await _unitofwork.website.Deletewebsite(id);

                    _unitofwork.Complete();

              return Ok(id);
        }

        



    }
}