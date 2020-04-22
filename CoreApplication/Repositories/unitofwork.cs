using EzilineApp.Api.CoreApplication.Data;
using EzilineApp.Api.CoreApplication.IRespositories;

namespace EzilineApp.Api.CoreApplication.Repositories
{
    public class unitofwork:Iunitofwork
    {

        private readonly DataContext _dataContext;
        public unitofwork (DataContext dataContext)
        {

            _dataContext = dataContext;

            admin        = new  Admin(_dataContext);

            website      = new  managewebsite(_dataContext);

            reviews      = new  managereviews(_dataContext);
               
        }

        public IAdmin admin            {get;set;}
        public Imanagewebsite website  {get;set;}
        public Imanagereviews reviews  {get;set;}

        public IAdmin         AdminRepository => throw new System.NotImplementedException();
        public Imanagewebsite managewebsite => throw new System.NotImplementedException();
        public Imanagereviews managereviews => throw new System.NotImplementedException();
        public void Complete () 
        {
            _dataContext.SaveChanges();
        }
    }
}