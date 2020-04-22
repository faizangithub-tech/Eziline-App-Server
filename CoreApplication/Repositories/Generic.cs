using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EzilineApp.Api.CoreApplication.Data;
using EzilineApp.Api.CoreApplication.IRespositories;
using Microsoft.EntityFrameworkCore;

namespace EzilineApp.Api.CoreApplication.Repositories
{
    public class Generic<Tentity> : IGeneric<Tentity> where Tentity : class 
    {
        private readonly DataContext _dataContext;

        public Generic (DataContext dataContext) 
        {
            
            _dataContext = dataContext;
            
        }

        public async Task AddAsync(Tentity T)
        {

             await _dataContext.Set<Tentity>().AddAsync(T);

        }

        public async Task<Tentity> FindAsync(Expression<Func<Tentity, bool>> predicate)
        {
            
            return await _dataContext.Set<Tentity>()
                                     .Where(predicate)
                                     .FirstOrDefaultAsync();

        }

        public  Task<IEnumerable<Tentity>> GetAllAsync()
        {
                throw new NotImplementedException();
        }

        public void Remove(Tentity T)
        {
            throw new NotImplementedException();
        }
    }
}