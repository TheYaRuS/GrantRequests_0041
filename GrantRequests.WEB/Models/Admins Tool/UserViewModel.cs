using GrantRequests.Common;
using GrantRequests.WEB.Services;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GrantRequests.WEB.Models
{
    public class UserViewModel
    {
        private ViewDataService viewDataService;
        public ViewDataService ViewDataService
        {
            get
            {
                if (viewDataService == null)
                    viewDataService = DependencyResolver.Current.GetService<ViewDataService>();
                return viewDataService;
            }
        }

        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "User name")]
        [Required]
        [StringLength(512)]
        public string Name { get; set; }

        [Display(Name = "User role")]
        [Required]
        public Role Role { get; set; }

        [Display(Name = "My approvers")]
        public int[] ApproversId { get; set; }

        [Display(Name = "My approvers")]
        public UserViewModel[] Approvers { get; set; }

        [Display(Name = "My point personals")]
        public UserViewModel[] PointPersonals { get; set; }
    }
}