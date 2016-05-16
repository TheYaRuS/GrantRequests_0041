using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GrantRequests.Common
    {
    public enum VotingOptions
    {
        Approve = 1,
        RFI = 2,
        Decline = 3,
        Reset = 4
    }

    public enum Role
    {
        [Description("None")]
        None = 0,
        [Description("Administrator")]
        Admin = 1,
        [Description("Point Personal")]
        PointPerson = 2,
        [Description("Approver")]
        Approver = 3,
        [Description("Requestor")]
        Requestor = 4
    }
    public enum RequestType
    {
        [Description("SF")]
        [Display(Name = "Scientific Funding")]
        ScientificFunding = 1,
        [Description("PA")]
        [Display(Name = "Patient Advocasy")]
        PatientAdvocasy = 2,
        [Description("DE")]
        [Display(Name = "Display & Exhibit")]
        DisplayAndExhibit = 3
    }
    public enum StatusRequest
    {
        [Description("Draft")]
        [Display(Name = "Draft")]
        Draft = 1,
        [Description("Pending")]
        [Display(Name = "Pending")]
        Pending = 2,
        [Description("Declined")]
        [Display(Name = "Declined")]
        Declined = 3,
        [Description("Request for information")]
        [Display(Name = "Request for information")]
        RFI = 4,
        [Description("Submitted")]
        [Display(Name = "Submitted")]
        Submitted = 5,
        [Description("Committee Review")]
        [Display(Name = "Committee Review")]
        CommitteeReview = 6,
        [Description("Scheduled")]
        [Display(Name = "Scheduled")]
        Scheduled = 7,
        [Description("Occurred")]
        [Display(Name = "Occurred")]
        Occurred = 8
    }
    public enum SalutationType
    {
        [Description("")]
        [Display(Name = "None")]
        None,
        [Description("Dr.")]
        [Display(Name = "Dr.")]
        Dr,
        [Description("Mr.")]
        [Display(Name = "Mr.")]
        Mr,
        [Description("Ms.")]
        [Display(Name = "Ms.")]
        Ms,
        [Description("Prof.")]
        [Display(Name = "Prof.")]
        Prof
    }
    public enum WatchTime
    {
        [Description("<1 hour")]
        [Display(Name = "<1 hour")]
        LessOneHour = 1,
        [Description("1-3 hours")]
        [Display(Name = "1-3 hours")]
        OneToThreeHours = 2,
        [Description("4-6 hours")]
        [Display(Name = "4-6 hours")]
        FourToSixHours = 3,
        [Description("7-8 hours")]
        [Display(Name = "7-8 hours")]
        SevenToEightHours = 4,
        [Description(">8 hours")]
        [Display(Name = ">8 hours")]
        MoreEightHours = 5
    }
}
