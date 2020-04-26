using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EzilineApp.Api.CoreApplication.DataTransferObjs.users_dtos;
using EzilineApp.Api.CoreApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using EzilineApp.Api.CoreApplication.Data;
using System.Linq;

namespace EzilineApp.Api.Controllers
{
    
    [Route ("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase 
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _usermanager;
        private readonly SignInManager<User> _signInmanager;
        private readonly DataContext _datacontext;
        public AuthController (IConfiguration config,
         IMapper mapper,
         UserManager<User> userManager,
         SignInManager<User> signInManager,DataContext datacontext) 
        {
            _mapper = mapper;
            _config = config;
            _usermanager=userManager;
            _signInmanager=signInManager;
            _datacontext=datacontext;
        }

        
        [HttpPost("login")]
        public async Task<IActionResult> login (logindto loginobj) 
        {
           
            var user =await _usermanager.FindByEmailAsync(loginobj.email);
            
            if(user!=null)
            {

            var result =await _signInmanager.CheckPasswordSignInAsync(user,loginobj.password,false);

                if(result.Succeeded)
                {
                     var userprofile  =_datacontext.Users
                                                   .Include(x=>x.profile)
                                                   .Where(x=>x.Id==user.Id)
                                                   .ToList();
                                                           
                     var usertoreturn = _mapper.Map<User,usertoreturn>(userprofile[0]);
                     
                     return Ok (new
                     {
                            token=GenerateJwtToken(user).Result,
                            user=usertoreturn   
                     });
                }
            }
                return StatusCode(400,"email or password is incorrect");
        }

        
        [HttpPost("register")]
        public async Task<IActionResult> register(registerdto newuser)
        {
            
            var usertocreate =_mapper.Map<User>(newuser);

            var result=await _usermanager.CreateAsync(usertocreate,newuser.password);

            if(result.Succeeded)
            {
                    var user =await _usermanager.FindByEmailAsync(newuser.Email);
             
                    var usertoreturn = _mapper.Map<usertoreturn>(user);

                    return Ok(usertoreturn);
            }

            return StatusCode(400,"failed to add new user");

        }

        
        [HttpGet("uniquename")]
        public async Task<IActionResult> uniqueusername(string username)
        {
            User user=await _usermanager.FindByNameAsync(username);
             
            usertoreturn mapped=_mapper.Map<usertoreturn>(user); 
     
            return Ok(mapped);
        }

        
        [HttpGet("uniqueemail")]
        public async Task<IActionResult> uniqueemail(string email)
        {
            User user=await _usermanager.FindByEmailAsync(email);
             
            usertoreturn mapped=_mapper.Map<usertoreturn>(user); 
     
            return Ok(mapped);
        }


        private async Task<string> GenerateJwtToken(User user)
        {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimsIdentity.DefaultNameClaimType,user.UserName)
                    
                };

                var roles=await _usermanager.GetRolesAsync(user);

                foreach(var role in roles)
                { 
                    claims.Add(new Claim(ClaimTypes.Role,role));
                }
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

                var SignInCredential = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var TokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = SignInCredential
                };
                var TokenHandler = new JwtSecurityTokenHandler();

                var token = TokenHandler.CreateToken(TokenDescriptor);

                string Token=TokenHandler.WriteToken(token);
                
                return Token;
        }

    }
}