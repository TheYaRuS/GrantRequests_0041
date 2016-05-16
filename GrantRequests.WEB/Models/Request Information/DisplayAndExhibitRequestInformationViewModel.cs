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
    public class DisplayAndExhibitRequestInformationViewModel :InformationViewModelBase, IValidatableObject
    {
        public const string actionName = "RequestInformationDE";
        public const string controllerName = "Home";
        public const string editView = "EditView/EditDisplayAndExhibitRequestInformation";
        public const string detailsView = "DetailsView/ViewDisplayAndExhibitRequestInformation";

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

        [Display(Name = "Is there an event associated with this program?")]
        [Required]
        public bool IsThereEvent { get; set; }

        [Display(Name = "Event Title")]
        [RequiredIfTrue("IsThereEvent")]
        public string Title { get; set; }

        [Display(Name = "Attach a detailed event Agenda, if applicable, including a detailed description of the program/initiative our funding will support.")]
        public HttpPostedFileBase EventAgendaFile { get; set; }

        [Display(Name = "Agenda of events")]
        public string EventAgenda { get; set; }

        [Display(Name = "Event Start Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [RequiredIfTrue("IsThereEvent")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Event End Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [GreaterThanOrEqualTo("StartDate")]
        [RequiredIfTrue("IsThereEvent")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Event Venue Name")]
        [MaxLength(512)]
        [RequiredIfTrue("IsThereEvent")]
        public string VenueName { get; set; }

        [Display(Name = "Event Country")]
        [RequiredIfTrue("IsThereEvent")]
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
        [RequiredIfTrue("IsThereEvent")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Event Address line 2")]
        [MaxLength(512)]
        public string AddressLine2 { get; set; }

        [Display(Name = "Event Address line 3")]
        [MaxLength(512)]
        public string AddressLine3 { get; set; }

        [Display(Name = "Event City")]
        [MaxLength(512)]
        [RequiredIfTrue("IsThereEvent")]
        public string City { get; set; }

        [Display(Name = "Event Postal Code")]
        [DataType(DataType.PostalCode)]
        [RegularExpression(@"^\d*$", ErrorMessage = "Incorrect Event Postal Code")]
        [RequiredIfTrue("IsThereEvent")]
        public string PostalCode { get; set; }

        [Display(Name = "Estimated Number of Attendees")]
        [Range(0, 1000000)]
        [RequiredIfTrue("IsThereEvent")]
        public int NumberOfAttendees { get; set; }

        [Display(Name = "Is there an educational portion of this event?")]
        [RequiredIfTrue("IsThereEvent")]
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

        [Display(Name = "Type of Display/Exhibit ")]
        [Required]
        public int? TypeOfDisplayExhibitId { get; set; }

        [Display(Name = "Type of Display/Exhibit ")]
        public string TypeOfDisplayExhibit { get; set; }

        [Display(Name = "Total number of exhibitors expected")]
        [Required]
        [Range(1, 1000000)]
        public int NumberOfExhibitors { get; set; }

        [Display(Name = "How many hours per day are set aside for attendees to view the exhibits?")]
        [Required]
        public WatchTime WatchTime { get; set; }

        [Display(Name = "Location of exhibit")]
        [Required]
        public string LocationOfExhibit { get; set; }

        [Display(Name = "Is the exhibit space accessible while the event is in session?")]
        [Required]
        public bool? IsAccessibleInSession { get; set; }

        [Display(Name = "Please attach a request letter on the organization’s letterhead including a detailed description of the initiative our funding will support.")]
        [RequiredIfEmpty("RequestLetter")]
        public HttpPostedFileBase RequestLetterFile { get; set; }

        [Display(Name = "Request letter")]
        public string RequestLetter { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            if (CountryId == 1 && (PostalCode.Length > 5))
                errors.Add(new ValidationResult(string.Format("Incorrect Postal Code for United States")));
            if (StartDate <= DateTime.Now.AddDays(5))
                errors.Add(new ValidationResult(string.Format("Please note, we can only accept requests beginning at least five business days from today.")));
            if (string.IsNullOrWhiteSpace(EventAgenda) && IsThereEvent && EventAgendaFile == null)
                errors.Add(new ValidationResult(string.Format("Incorrect Event Agenda File")));
            return errors;
        }
    }
}