using System.Collections.Generic;

namespace EzilineApp.Api.CoreApplication.Models
{
    public class website
    {
        public int    id           {get;set;}
        public string refrallink   {get;set;}
        public string layoutimgurl {get;set;}
        public string name         {get;set;}
        public string intro        {get;set;}
        public string description  {get;set;}
        public int    userid       {get;set;}
        public User   user         {get;set;}
        public summary  summary    {get;set;}
        public ICollection<comments> comments {get;set;}
        public ICollection<ratings>  ratings  {get;set;}
    }

}