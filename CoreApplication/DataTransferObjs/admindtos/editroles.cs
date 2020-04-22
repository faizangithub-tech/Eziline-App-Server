using System.Collections.Generic;
using EzilineApp.Api.CoreApplication.Models;

namespace EzilineApp.Api.CoreApplication.DataTransferObjs.admindtos
{
    public class editroles
    {
        public int id{get;set;}
        public string username {get;set;}
        public string[] roles;

    }
}