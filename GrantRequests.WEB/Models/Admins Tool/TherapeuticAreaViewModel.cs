using GrantRequests.WEB.Services;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using GrantRequests.Common;

namespace GrantRequests.WEB.Models
{
    public class TherapeuticAreaViewModel
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

        [Display(Name = "Title name")]
        [Required]
        [StringLength(512)]
        public string Title { get; set; }

        [Display(Name = "Type of request")]
        [Required]
        public RequestType TypeRequest { get; set; }

        [Display(Name = "Point Personal")]
        [Required]
        public int? PointPersonalId { get; set; }

        [Display(Name = "Point Personal")]
        public UserViewModel PointPersonal { get; set; }

    }
}