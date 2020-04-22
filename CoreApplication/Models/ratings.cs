namespace EzilineApp.Api.CoreApplication.Models
{
    public class ratings
    {
        public int id            {get;set;}
        public int polarity      {get;set;}
        public int percentage    {get;set;}
        public int webid         {get;set;}
        public website website   {get;set;}

    }
}