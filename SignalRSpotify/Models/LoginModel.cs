using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRSpotify.Models
{
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [Remote("CheckIfUserExists", "Home", ErrorMessage = "UserName or Password is Incorrect", AdditionalFields = "UserName")]
        public string Password { get; set; }
    }
}
