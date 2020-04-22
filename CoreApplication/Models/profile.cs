namespace EzilineApp.Api.CoreApplication.Models
{
    public class profile
    {
        public int    id        {get;set;}
        public string name      {get;set;}
        public string imgurl    {get;set;} 
        public string aboutme   {get;set;}
        public int    usrid     {get;set;}
        public User   user      {get;set;}
        
    }
}