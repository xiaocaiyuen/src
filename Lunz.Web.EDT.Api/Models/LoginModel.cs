using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lunz.Web.EDT.Api.Models
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Remember { get; set; }
    }
}