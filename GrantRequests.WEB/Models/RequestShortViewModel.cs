using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GrantRequests.Common;
using GrantRequests.WEB.Services;

namespace GrantRequests.WEB.Models
{
    public class RequestShortViewModel
    {  

        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "ID")]
        [StringLength(8)]        
        public string DescriptorId
        {
            get
            {
                return string.Format("{0}-{1:D5}", RequestType.GetDescription(), Id);
            }
         }
        [Display(Name = "Therapeutic Area")]
        public int? TherapeuticAreaId { get; set; }
        [Display(Name = "Therapeutic Area")]
        public string TherapeuticArea { get; set; }
        public int? ContactInformationId { get; set; }
        public int? ScientificFundingRequestInformationId { get; set; }
        public int? PatientAdvocasyRequestInformationId { get; set; }
        public int? DisplayAndExhibitRequestInformationId { get; set; }
        public int? PaymentInformationId { get; set; }

        [Display(Name = "Event Name")]
        public string EventName { get; set; }
        [Display(Name = "Date of Event")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? DateOfEvent { get; set; }
        [Display(Name = "Status")]
        public StatusRequest StatusRequest { get; set; }
        [Display(Name = "Request type")]
        public RequestType RequestType { get; set; }
    }
}