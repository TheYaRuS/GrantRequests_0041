using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GrantRequests.WEB.Models.Account
{
    public class LoginViewModel
    {
        [Display(Name = "Login Name")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Password")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}