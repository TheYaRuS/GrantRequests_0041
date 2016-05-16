using System;
using System.Collections.Generic;
using System.Web.Mvc;
using GrantRequests.Common;
using Foolproof;
using System.ComponentModel.DataAnnotations;
using GrantRequests.WEB.Services;
using GrantRequests.DAL.Entities;

namespace GrantRequests.WEB.Models
{
    public class RequestLongViewModel
    {
        public const string actionName = "";
        public const string controllerName = "";
        public const string editView = "";
        public const string detailsView = "DetailsView/ViewRequestDetails";
        
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        public RequestType RequestType { get; set; }

        [HiddenInput(DisplayValue = false)]
        public StatusRequest StatusRequest { get; set; }

        public CommitteeViewModel Committee { get; set; }

        public IEnumerable<VoteResultViewModel> VoteResult { get; set; }

        public int? TherapeuticAreaId { get; set; }
        public virtual TherapeuticAreaViewModel TherapeuticArea { get; set; }

        public int? ContactInformationId { get; set; }
        public ContactInformationViewModel ContactInformation { get; set; }

        public int? PatientAdvocasyRequestInformationId { get; set; }
        public PatientAdvocasyRequestInformationViewModel RequestInformationPA { get; set; }

        public int? ScientificFundingRequestInformationId { get; set; }
        public ScientificFundingRequestInformationViewModel RequestInformationSF { get; set; }

        public int? DisplayAndExhibitRequestInformationId { get; set; }
        public DisplayAndExhibitRequestInformationViewModel RequestInformationDE { get; set; }

        public int? PaymentInformationId { get; set; }
        public PaymentInformationViewModel PaymentInformation { get; set; }

        public bool[] VerificationPart()
        {
            var verificationPart = new bool[3];

            verificationPart[0] = (ContactInformation != null && ContactInformation.IsValid);
            verificationPart[1] =    ((RequestInformationSF != null && RequestInformationSF.IsValid) && (RequestType == RequestType.ScientificFunding))
                                  || ((RequestInformationPA != null && RequestInformationPA.IsValid) && (RequestType == RequestType.PatientAdvocasy))
                                  || ((RequestInformationDE != null && RequestInformationDE.IsValid) && (RequestType == RequestType.DisplayAndExhibit));
            verificationPart[2] = (PaymentInformation != null && PaymentInformation.IsValid);
           
            return verificationPart;
        }
    }
}