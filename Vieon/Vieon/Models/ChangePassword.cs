using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vieon.Models
{
    public class ChangePassword
    {
        public string CurentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}