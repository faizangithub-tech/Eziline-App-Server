
using System.Threading.Tasks;
using AutoMapper;
using EzilineApp.Api.CoreApplication.Data;
using EzilineApp.Api.CoreApplication.DataTransferObjs.admindtos;
using EzilineApp.Api.CoreApplication.Models;
using EzilineApp.Api.CoreApplication.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EzilineTask.Controllers 
{
    [Route ("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase 
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _usermanager;
        private unitofwork _unitofwork;
        
        public AdminController (DataContext dataContext, UserManager<User> userManager, IMapper mapper)
        {
            _dataContext = dataContext;
            _usermanager = userManager;
            _mapper=mapper;
            _unitofwork=new unitofwork(_dataContext);
            _unitofwork.admin.GetMapper(_mapper);
            
        }

        [Authorize (Policy = "RequireAdmin")]
        [HttpGet ("usersalongrole")]
        public async Task<IActionResult> Get ()
        {
            
            var userroles=await _unitofwork.admin
                                           .GetUsersAlongRoles();

            return Ok (userroles);
        }

        [Authorize (Policy = "RequireAdmin")]
        [HttpPost ("editroles")]
        public async Task<IActionResult> EditRoles (editroles edit)
        {

                await _unitofwork.admin.EditUserRoles(edit);
                           
                return Ok();
        }

        
        // POST api/values
        [HttpPost]
        public void Post ([FromBody] string value) { }

        // PUT api/values/5
        [HttpPut ("{id}")]
        public void Put (int id, [FromBody] string value) { }

        // DELETE api/values/5
        [HttpDelete ("{id}")]
        public void Delete (int id) { }
    }
}