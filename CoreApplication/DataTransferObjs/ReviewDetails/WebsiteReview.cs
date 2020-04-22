using EzilineApp.Api.CoreApplication.DataTransferObjs.users_dtos;

namespace EzilineApp.Api.CoreApplication.DataTransferObjs.ReviewDetails
{
    public class WebsiteReview
    {
        public usertoreturn user =new usertoreturn();
        public int       id       {get;set;}
        public string    text     {get;set;}
        public int       polarity {get;set;}
        public int       webid    {get;set;}
        public int       userid   {get;set;}
        
    }
}