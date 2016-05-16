using Foolproof;
using GrantRequests.WEB.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using GrantRequests.Common;

namespace GrantRequests.WEB.Models
{
    public class PaymentInformationViewModel :InformationViewModelBase, IValidatableObject
    {
        public const string actionName = "PaymentInformation";
        public const string controllerName = "Home";
        public const string editView = "EditView/EditFundingAndPaymentInformation";
        public const string detailsView = "DetailsView/ViewFundingAndPaymentInformation";        

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

        public static BudgetBreakdownLineViewModel[] GetDefaultBudgetBreakdown(int size = 5)
        {
            return new[]
            {
                new BudgetBreakdownLineViewModel(),
                new BudgetBreakdownLineViewModel(),
                new BudgetBreakdownLineViewModel(),
                new BudgetBreakdownLineViewModel(),
                new BudgetBreakdownLineViewModel()
            };
        }

        [Display(Name = "Requested Funding")]
        [Required]
        [Range(0.01, 1000000000)]
        public decimal RequestedFunding { get; set; }

        [Display(Name = "Total Program Cost")]
        [Required]
        [Range(0.01, 1000000000)]
        public decimal TotalProgramCost { get; set; }
        
        public int[] BudgetBreakdownLinesId { get; set; }

        [Display(Name = "Program Budget Breakdown")]
        public BudgetBreakdownLineViewModel[] BudgetBreakdown { get; set; }

        [Display(Name = "Attach the Funding Request Letter and/or Proposal on the organization’s letterhead")]
        [RequiredIfEmpty("FundingRequestLetter")]
        public HttpPostedFileBase FundingRequestLetterFile { get; set; }

        [Display(Name = "Funding Request Letter and/or Proposal on the organization’s letterhead")]
        public string FundingRequestLetter { get; set; }

        [Display(Name = "Are there different levels of funding available?")]
        [Required]
        public bool? AreDifferentLevelsOfFunding { get; set; }

        [Display(Name = "Please describe the different levels of funding in detail or attach the prospectus.")]
        [DataType(DataType.MultilineText)]
        [UIHint("MultilineText")]
        public string DifferentLevelsOfFundingDescription { get; set; }
        public string DifferentLevelsOfFunding { get; set; }
        public HttpPostedFileBase DifferentLevelsOfFundingFile { get; set; }

        [Display(Name = "Are any additional opportunities or benefits given to sponsors? ")]
        [Required]
        public bool? AreOpportunitiesOrBenefits { get; set; }

        [Display(Name = "Please specify opportunities and benefits.")]
        [RequiredIfTrue("AreOpportunitiesOrBenefits")]
        public int[] OpportunitiesOrBenefitsId { get; set; }

        [Display(Name = "List of specify opportunities and benefits.")]
        [UIHint("Collection")]
        public BenefitViewModel[] OpportunitiesOrBenefits { get; set; }

        [Display(Name = "Is the organization listed on this request or the payee a physician practice?")]
        public bool? IsOrganizationOrPhysicianPractice { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (AreDifferentLevelsOfFunding == true)
            {
                if(string.IsNullOrWhiteSpace(DifferentLevelsOfFunding))
                if(string.IsNullOrWhiteSpace(DifferentLevelsOfFundingDescription)&&(DifferentLevelsOfFundingFile == null))
                    errors.Add(new ValidationResult(string.Format("Incorrect describe the different levels of funding in detail or attach the prospectus.")));
            }        

            return errors;
        }
    }
}