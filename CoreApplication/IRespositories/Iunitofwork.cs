namespace EzilineApp.Api.CoreApplication.IRespositories
{
    public interface Iunitofwork
    {
         
        IAdmin  AdminRepository {get;}
        Imanagewebsite managewebsite {get;}

        Imanagereviews managereviews {get;}

    }
}