using GrantRequests.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GrantRequests.DAL.Entities
{

    public class Request
    {
        public Request()
        {
            Votes = new List<VoteResult>();
        }
        public int Id { get; set; }
        public int? TherapeuticAreaId { get; set; }
        public virtual TherapeuticArea TherapeuticArea { get; set; }
        public RequestType RequestType { get; set; }
        public int? ContactInformationId { get; set; }
        public virtual ContactInformation ContactInformation { get; set; }
        public int? PatientAdvocasyRequestInformationId { get; set; }
        public virtual PatientAdvocasyRequestInformation RequestInformationPA { get; set; }
        public int? ScientificFundingRequestInformationId { get; set; }
        public virtual ScientificFundingRequestInformation RequestInformationSF { get; set; }
        public int? DisplayAndExhibitRequestInformationId { get; set; }
        public virtual DisplayAndExhibitRequestInformation RequestInformationDE { get; set; }
        public int? PaymentInformationId { get; set; }
        public virtual PaymentInformation PaymentInformation { get; set; }
        public string EventName { get; set; }
        public DateTime? DateOfEvent { get; set; }
        public int? RequestorId { get; set; }
        public virtual User Requestor { get; set; }
        public StatusRequest StatusRequest { get; set; }
        public virtual ICollection<VoteResult> Votes { get; set; }
        public DateTime? CommitteeMeetingDate { get; set; }
        public decimal? RecommendedAmount { get; set; }
    }

    public class VoteResult
    {
        public int Id { get; set; }
        public int? RequestId { get; set; }
        public virtual Request Request { get; set; }
        public int? UserId { get; set; }
        public virtual User Approver { get; set; }
        public VotingOptions? Voting { get; set; }
        public DateTime? DateTime { get; set; }

    }
    #region ContactInformation
    public class ContactInformation
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public RequestType RequestType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }        
        public string Email { get; set; }
        public SalutationType SalutationType { get; set; }        
        public string JobTitle { get; set; }        
        public string PhoneNumber { get; set; }        
        public string MobileNumber { get; set; }        
        public string FaxNumber { get; set; }        
        public string DBA { get; set; }        
        public string WebSite { get; set; }
        public int? CountryId { get; set; }
        public virtual Country Country { get; set; }
        public int? StateId { get; set; }
        public virtual State State { get; set; }        
        public string StateProvinceTerritory { get; set; }        
        public string AddressLine1 { get; set; }        
        public string AddressLine2 { get; set; }        
        public string AddressLine3 { get; set; }        
        public string City { get; set; }        
        public string PostalCode { get; set; }
        public bool IsValid { get; set; }
    }


    public class Country
    {
        public int Id { get; set; }        
        public string Title { get; set; }
    }
    public class State
    {
        public int Id { get; set; }       
        public string Title { get; set; }
        public string Abbreviation { get; set; }
    }
    #endregion

    #region RequestInformation   

    public class TherapeuticArea
    {

        public int Id { get; set; }        
        public string Title { get; set; }
        public RequestType TypeRequest { get; set; }
        public int? PointPersonalId { get; set; }
        public virtual User PointPersonal { get; set; }
    }

    public class ScientificFundingRequestInformation
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public RequestType RequestType { get; set; }
        public int? TherapeuticAreaId { get; set; }
        public virtual TherapeuticArea TherapeuticArea { get; set; }        
        public bool IsThereEvent { get; set; }        
        public string Title { get; set; }        
        public string EventAgenda { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }        
        public string VenueName { get; set; }
        public int? CountryId { get; set; }
        public virtual Country Country { get; set; }
        public int? StateId { get; set; }
        public virtual State State { get; set; }        
        public string StateProvinceTerritory { get; set; }        
        public string AddressLine1 { get; set; }        
        public string AddressLine2 { get; set; }        
        public string AddressLine3 { get; set; }        
        public string City { get; set; }        
        public string PostalCode { get; set; }
        public int NumberOfAttendees { get; set; }
        public bool? IsEducationalPortion { get; set; }
        public bool IsValid { get; set; }
    }

    public class PatientAdvocasyRequestInformation
    {
        public PatientAdvocasyRequestInformation()
        {
            HealthcareProfessionals = new List<HealthcareProfession>();
        }
        public int Id { get; set; }
        public int RequestId { get; set; }
        public RequestType RequestType { get; set; }
        public int? TherapeuticAreaId { get; set; }
        public virtual TherapeuticArea TherapeuticArea { get; set; }        
        public string DescribeProgramInitiative { get; set; }        
        public string OrgLetterOfIntent { get; set; }        
        public string DescribeProgramCommunicate { get; set; }        
        public string DescribeMethodology { get; set; }        
        public string Title { get; set; }        
        public string EventAgenda { get; set; }        
        public string SampleInvitation { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }        
        public string VenueName { get; set; }
        public int? CountryId { get; set; }
        public virtual Country Country { get; set; }
        public int? StateId { get; set; }
        public virtual State State { get; set; }        
        public string StateProvinceTerritory { get; set; }        
        public string AddressLine1 { get; set; }        
        public string AddressLine2 { get; set; }        
        public string AddressLine3 { get; set; }        
        public string City { get; set; }        
        public string PostalCode { get; set; }
        public int NumberOfAttendees { get; set; }
        public bool? IsEducationalPortion { get; set; }
        public bool? ArePatientsInvited { get; set; }
        public virtual ICollection<HealthcareProfession> HealthcareProfessionals { get; set; }
        public bool IsValid { get; set; }

    }

    public class DisplayAndExhibitRequestInformation
    {
        public DisplayAndExhibitRequestInformation()
        {
            HealthcareProfessionals = new List<HealthcareProfession>();
        }
        public int Id { get; set; }
        public int RequestId { get; set; }
        public RequestType RequestType { get; set; }
        public int? TherapeuticAreaId { get; set; }
        public virtual TherapeuticArea TherapeuticArea { get; set; }
        public bool IsThereEvent { get; set; }        
        public string Title { get; set; }        
        public string EventAgenda { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }        
        public string VenueName { get; set; }
        public int? CountryId { get; set; }
        public virtual Country Country { get; set; }
        public int? StateId { get; set; }
        public virtual State State { get; set; }        
        public string StateProvinceTerritory { get; set; }        
        public string AddressLine1 { get; set; }        
        public string AddressLine2 { get; set; }        
        public string AddressLine3 { get; set; }        
        public string City { get; set; }        
        public string PostalCode { get; set; }
        public int NumberOfAttendees { get; set; }
        public bool? IsEducationalPortion { get; set; }
        public virtual ICollection<HealthcareProfession> HealthcareProfessionals { get; set; }
        public bool? ArePatientsInvited { get; set; }
        public int? TypeOfDisplayExhibitId { get; set; }
        public virtual TypeOfDisplayExhibit TypeOfDisplayExhibit { get; set; }
        public int NumberOfExhibitors { get; set; }
        public WatchTime WatchTime { get; set; }        
        public string LocationOfExhibit { get; set; }
        public bool? IsAccessibleInSession { get; set; }
        public string RequestLetter { get; set; }
        public bool IsValid { get; set; }
    }
    public class TypeOfDisplayExhibit
    {
        public int Id { get; set; }   
        public string Title { get; set; }
    }
    public class HealthcareProfession
    {
        public HealthcareProfession()
        {
            PatientAdvocasyRequestInformation = new List<PatientAdvocasyRequestInformation>();
            DisplayAndExhibitRequestInformation = new List<DisplayAndExhibitRequestInformation>();
        }
        public int Id { get; set; }       
        public string Title { get; set; }
        public virtual ICollection<PatientAdvocasyRequestInformation> PatientAdvocasyRequestInformation { get; set; }
        public virtual ICollection<DisplayAndExhibitRequestInformation> DisplayAndExhibitRequestInformation { get; set; }
    }
    #endregion      

    #region FundingAndPaymentInformation
    public class PaymentInformation
    {
        public PaymentInformation()
        {
            OpportunitiesOrBenefits = new List<Benefit>();
            BudgetBreakdown = new List<BudgetBreakdownLine>();
        }
        public int Id { get; set; }
        public int RequestId { get; set; }
        public RequestType RequestType { get; set; }
        public decimal RequestedFunding { get; set; }
        public decimal TotalProgramCost { get; set; }
        public virtual ICollection<BudgetBreakdownLine> BudgetBreakdown { get; set; }        
        public string FundingRequestLetter { get; set; }
        public bool AreDifferentLevelsOfFunding { get; set; }        
        public string DifferentLevelsOfFundingDescription { get; set; }        
        public string DifferentLevelsOfFunding { get; set; }
        public bool AreOpportunitiesOrBenefits { get; set; }
        public virtual ICollection<Benefit> OpportunitiesOrBenefits { get; set; }
        public bool IsOrganizationOrPhysicianPractice { get; set; }
        public bool IsValid { get; set; }
    }

    public class BudgetBreakdownLine
    {
        public int Id { get; set; }
        public int? FundingAndPaymentInformationId { get; set; }
        public virtual PaymentInformation FundingAndPaymentInformation { get; set; }        
        public string ExpenseType { get; set; }
        public decimal EstimatedTotal { get; set; }        
        public string Description { get; set; }
    }
    public class Benefit
    {
        public Benefit()
        {
            FundingAndPaymentInformations = new List<PaymentInformation>();
        }

        public int Id { get; set; }        
        public string Title { get; set; }
        public virtual ICollection<PaymentInformation> FundingAndPaymentInformations { get; set; }
    }
    #endregion   
}
