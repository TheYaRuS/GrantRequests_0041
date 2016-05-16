using Foolproof;
using GrantRequests.WEB.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using GrantRequests.Common;
namespace GrantRequests.WEB.Models
{
    public class PatientAdvocasyRequestInformationViewModel : InformationViewModelBase, IValidatableObject
    {
        public const string actionName = "RequestInformationPA";
        public const string controllerName = "Home";
        public const string editView = "EditView/EditPatientAdvocasyRequestInformation";
        public const string detailsView = "DetailsView/ViewPatientAdvocasyRequestInformation";

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

        [Display(Name = "Therapeutic Focus Area")]
        [Required]
        public int? TherapeuticAreaId { get; set; }

        [Display(Name = "Therapeutic Focus Area")]
        public string TherapeuticArea { get; set; }

        [Display(Name = "Describe program initiative.")]
        [Required]
        [DataType(DataType.MultilineText)]
        [UIHint("MultilineText")]
        public string DescribeProgramInitiative { get; set; }

        [Display(Name = "Attach the organization’s Letter of Intent.")]
        [RequiredIfEmpty("OrgLetterOfIntent")]
        public HttpPostedFileBase OrgLetterOfIntentFile { get; set; }

        [Display(Name = "Organization’s Letter of Intent.")]
        public string OrgLetterOfIntent { get; set; }

        [Display(Name = "Describe how the program will be communicated and how Regeneron's support will be acknowledged (if applicable)?")]
        [Required]
        [DataType(DataType.MultilineText)]
        [UIHint("MultilineText")]
        public string DescribeProgramCommunicate { get; set; }

        [Display(Name = "List impacts or outcomes to be achieved by your program. Describe the methodology to track and measure results.")]
        [Required]
        [DataType(DataType.MultilineText)]
        [UIHint("MultilineText")]
        public string DescribeMethodology { get; set; }

        [Display(Name = "Event Title")]
        [Required]
        public string Title { get; set; }

        [Display(Name = "Attach a detailed event Agenda, if applicable, including a detailed description of the program/initiative our funding will support.")]
        [RequiredIfEmpty("EventAgenda")]
        public HttpPostedFileBase EventAgendaFile { get; set; }

        [Display(Name = "Agenda of events")]
        public string EventAgenda { get; set; }

        [Display(Name = "Attach a sample invitation. (if applicable)")]
        public HttpPostedFileBase SampleInvitationFile { get; set; }

        [Display(Name = "Sample invitation")]
        public string SampleInvitation { get; set; }

        [Display(Name = "Event Start Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Required]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Event End Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [GreaterThanOrEqualTo("StartDate")]
        [Required]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Event Venue Name")]
        [MaxLength(512)]
        [Required]
        public string VenueName { get; set; }

        [Display(Name = "Event Country")]
        [Required]
        public int? CountryId { get; set; }

        [Display(Name = "Event Country")]
        public string Country { get; set; }

        [Display(Name = "Event State")]
        [RequiredIf("CountryId", Operator.EqualTo, 1)]
        public int? StateId { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        [Display(Name = "Event State/Province/Territory")]
        [RequiredIf("CountryId", Operator.GreaterThan, 1)]
        [MaxLength(512)]
        public string StateProvinceTerritory { get; set; }

        [Display(Name = "Event Address line 1")]
        [MaxLength(512)]
        [Required]
        public string AddressLine1 { get; set; }

        [Display(Name = "Event Address line 2")]
        [MaxLength(512)]
        public string AddressLine2 { get; set; }

        [Display(Name = "Event Address line 3")]
        [MaxLength(512)]
        public string AddressLine3 { get; set; }

        [Display(Name = "Event City")]
        [MaxLength(512)]
        [Required]
        public string City { get; set; }

        [Display(Name = "Event Postal Code")]
        [DataType(DataType.PostalCode)]
        [RegularExpression(@"^\d*$", ErrorMessage = "Incorrect Event Postal Code")]
        [Required]
        public string PostalCode { get; set; }

        [Display(Name = "Estimated Number of Attendees")]
        [Range(0, 1000000)]
        [Required]
        public int NumberOfAttendees { get; set; }

        [Display(Name = "Is there an educational portion of this event?")]
        [Required]
        public bool? IsEducationalPortion { get; set; }

        [Display(Name = "What type of healthcare professionals are being invited to the program?")]
        [Required]
        public int[] HealthcareProfessionalsId { get; set; }

        [Display(Name = "List of healthcare professionals are being invited to the program")]
        [UIHint("Collection")]
        public HealthcareProfessionViewModel[] HealthcareProfessionals { get; set; }

        [Display(Name = "Are patients invited to the event?")]
        [Required]
        public bool? ArePatientsInvited { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            if (CountryId == 1 && (PostalCode.Length > 5))
                errors.Add(new ValidationResult(string.Format("Incorrect Postal Code for United States")));
            if (StartDate <= DateTime.Now.AddDays(5))
                errors.Add(new ValidationResult(string.Format("Please note, we can only accept requests beginning at least five business days from today.")));
            return errors;
        }
    }
}