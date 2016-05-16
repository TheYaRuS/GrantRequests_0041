using Foolproof;
using GrantRequests.Common;
using GrantRequests.WEB.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GrantRequests.WEB.Models
{
    public class CommitteeViewModel:InformationViewModelBase
    {
        public const string actionName = "";
        public const string controllerName = "Home";
        public const string editView = "";
        public const string detailsView = "";

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

        [Display(Name = "Recommended Request for Funding Amount:")]
        [Range(minimum: 0.01, maximum: 1000000000)]
        public decimal RecommendedAmount { get; set; }

        [Display(Name = "Committee Meeting Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Required]        
        public DateTime? CommitteeMeetingDate { get; set; }

        [Display(Name = "Committee for")]
        [Required]
        public int[] ApproversId { get; set; }

    }
}