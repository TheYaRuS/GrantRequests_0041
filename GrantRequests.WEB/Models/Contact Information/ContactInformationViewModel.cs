using GrantRequests.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Foolproof;
using GrantRequests.WEB.Services;

namespace GrantRequests.WEB.Models
{
    public class ContactInformationViewModel : InformationViewModelBase, IValidatableObject
    {
        public const string actionName = "ContactInformation";
        public const string controllerName = "Home";
        public const string editView = "EditView/EditContactInformation";
        public const string detailsView = "DetailsView/ViewContactInformation";

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


        [Display(Name = "First Name")]
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Display(Name = "E-mail")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Salutation type")]
        public SalutationType SalutationType { get; set; }

        [Display(Name = "Job Title")]
        [Required]
        [MaxLength(512)]
        public string JobTitle { get; set; }

        [Display(Name = "Phone number")]
        [DisplayFormat(DataFormatString = "{0:(###)###-####}", ApplyFormatInEditMode = true)]
        [Required]
        [RegularExpression(@"^(?:\(\d{3}\))\d{3}-\d{4}$", ErrorMessage = "Incorrect Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Mobile number")]
        [DisplayFormat(DataFormatString = "{0:(###)###-####}", ApplyFormatInEditMode = true)]
        [RegularExpression(@"^(?:\(\d{3}\))\d{3}-\d{4}$", ErrorMessage = "Incorrect Mobile Number")]
        public string MobileNumber { get; set; }

        [Display(Name = "Fax number")]
        [DisplayFormat(DataFormatString = "{0:(###)###-####}", ApplyFormatInEditMode = true)]
        [RegularExpression(@"^(?:\(\d{3}\))\d{3}-\d{4}$", ErrorMessage = "Incorrect Fax Number")]
        public string FaxNumber { get; set; }

        [Display(Name = "Country")]
        [Required]
        public int? CountryId { get; set;}
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "State")]
        [RequiredIf("CountryId", Operator.EqualTo, 1)]
        public int? StateId { get; set; }
        [Display(Name = "State")]
        public string State { get; set; }

        [Display(Name = "State/Province/Territory")]
        [MaxLength(512)]
        [RequiredIf("CountryId", Operator.GreaterThan, 1)]
        public string StateProvinceTerritory { get; set; }

        [Display(Name = "Address line 1")]
        [Required]
        [MaxLength(512)]
        public string AddressLine1 { get; set; }

        [Display(Name = "Address line 2")]
        [MaxLength(512)]
        public string AddressLine2 { get; set; }

        [Display(Name = "Address line 3")]
        [MaxLength(512)]
        public string AddressLine3 { get; set; }

        [Display(Name = "City")]
        [Required]
        [MaxLength(512)]
        public string City { get; set; }

        [Display(Name = "Postal Code")]
        [Required]
        [DataType(DataType.PostalCode)]
        [RegularExpression(@"^\d*$", ErrorMessage = "Incorrect Postal Code")]
        public string PostalCode { get; set; }

        [Display(Name = "Doctor of Business Administration")]
        [MaxLength(512)]
        public string DBA { get; set; }

        [Display(Name = "Web Site")]
        [MaxLength(512)]
        public string WebSite { get; set; }        

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            if (CountryId == 1 && (PostalCode.Length > 5))
                errors.Add(new ValidationResult(string.Format("Incorrect Postal Code for United States")));
            return errors;
        }
    }
}