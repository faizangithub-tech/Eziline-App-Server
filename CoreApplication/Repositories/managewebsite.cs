using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EzilineApp.Api.CoreApplication.Data;
using EzilineApp.Api.CoreApplication.DataTransferObjs.admindtos;
using EzilineApp.Api.CoreApplication.IRespositories;
using EzilineApp.Api.CoreApplication.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EzilineApp.Api.CoreApplication.Repositories
{
    public class managewebsite:Generic<website>, Imanagewebsite
    {
        private readonly DataContext _dataContext;
        private IMapper _mapper;
        public managewebsite (DataContext dataContext) : base(dataContext)
        {
             _dataContext=dataContext;
        }
        public void GetMapper(IMapper mapper)
        {
            _mapper=mapper;
        }

        public async Task<website> setentity(websiteentity entity)
        {
                  var obj=_mapper.Map<website>(entity);
                  
                  await base.AddAsync(obj);
                  
                  await _dataContext.SaveChangesAsync();

                  return obj;
        }

        public async Task<List<websiteentity>> Getallwebsites()
        {
                var records  = await _dataContext.websites
                                                 .OrderByDescending(x=>x.id)
                                                 .ToListAsync();

                var mappedentity=_mapper.Map<List<websiteentity>>(records);                             
                                                   
                return mappedentity;
        }
        
        public async Task Deletewebsite(int id)
        {
                    
                  var entity=await _dataContext.websites
                                               .FirstOrDefaultAsync(x=>x.id==id);  

                 _dataContext.websites.Remove(entity);
                     
        }
        public async Task<websiteentity> UpdateEntity(websiteentity entity)
        {
                var obj= await _dataContext.websites
                                           .FirstOrDefaultAsync(x=>x.id==entity.id);

                var update=_mapper.Map<websiteentity,website>(entity,obj);
                          
                    _dataContext.websites.Update(obj); 

                                await _dataContext.SaveChangesAsync();

                var obj1=_mapper.Map<websiteentity>(update);

                return obj1;                                                    
        }

        public async  Task<websiteentity> Getdetails(int id)
        {
            var entity =await  _dataContext.websites
                                           .FirstOrDefaultAsync(x=>x.id==id);

            var objreturn= _mapper.Map<websiteentity>(entity);

            return objreturn;
        }
    }
}