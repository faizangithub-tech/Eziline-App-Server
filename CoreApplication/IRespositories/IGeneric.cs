using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EzilineApp.Api.CoreApplication.IRespositories
{
    public interface IGeneric<Tentity>where Tentity: class 
    {

        Task AddAsync(Tentity T);
        Task<IEnumerable<Tentity>> GetAllAsync();

        Task<Tentity> FindAsync(Expression<Func<Tentity, bool>> predicate);

        void Remove(Tentity T);
        
    }
}