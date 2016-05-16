using GrantRequests.Common;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GrantRequests.WEB.Models.Account
{
    public class RegisterViewModel
    {
        [Display(Name = "Login Name")]
        [Required]
        [Remote("ValidateUserName","Account")]
        public string Name { get; set; }

        [Display(Name = "Password")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "User role")]
        [Required]
        [Range(1,4)]
        public Role Role { get; set; }
    }
}