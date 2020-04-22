namespace EzilineApp.Api.CoreApplication.Models
{
    public class comments
    {
        public int       id       {get;set;}
        public string    text     {get;set;}
        public int       polarity {get;set;}
        public int       webid    {get;set;}
        public website   website  {get;set;}
        public int       userid   {get;set;}
        public User      user     {get;set;}

    }
}