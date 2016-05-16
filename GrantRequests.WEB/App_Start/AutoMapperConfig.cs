using AutoMapper;
using GrantRequests.DAL.Entities;
using GrantRequests.Common;
using GrantRequests.WEB.Models;
using GrantRequests.WEB.Models.Account;

namespace GrantRequests.WEB.App_Start
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<Request, RequestShortViewModel>()
                 .ForMember(opt => opt.TherapeuticArea, opt => opt.MapFrom(c => c.TherapeuticArea.Title));
            Mapper.CreateMap<Request, RequestLongViewModel>()
                .Ignore(o => o.TherapeuticArea)
                .Ignore(o => o.ContactInformation)
                .Ignore(o => o.PaymentInformation)
                .Ignore(o => o.RequestInformationSF)
                .Ignore(o => o.RequestInformationPA)
                .Ignore(o => o.RequestInformationDE)
                .Ignore(o => o.Committee)
                .Ignore(o => o.VoteResult);
            Mapper.CreateMap<ContactInformationViewModel, ContactInformation>()
                .Ignore(o => o.Country)
                .Ignore(o => o.State);
            Mapper.CreateMap<ContactInformation, ContactInformationViewModel>()
                .ForMember(opt => opt.Country, opt => opt.MapFrom(c => c.Country.Title))
                .ForMember(opt => opt.State, opt => opt.MapFrom(c => c.State.Title))
                .Ignore(o => o.verificationPart);

            Mapper.CreateMap<ScientificFundingRequestInformationViewModel, ScientificFundingRequestInformation>()
                .Ignore(o => o.TherapeuticArea)
                .Ignore(o => o.Country)
                .Ignore(o => o.State);
            Mapper.CreateMap<ScientificFundingRequestInformation, ScientificFundingRequestInformationViewModel>()
                .ForMember(opt => opt.Country, opt => opt.MapFrom(c => c.Country.Title))
                .ForMember(opt => opt.State, opt => opt.MapFrom(c => c.State.Title))
                .ForMember(opt => opt.TherapeuticArea, opt => opt.MapFrom(c => c.TherapeuticArea.Title))
                .Ignore(o => o.EventAgendaFile)
                .Ignore(o => o.verificationPart);

            Mapper.CreateMap<PatientAdvocasyRequestInformationViewModel, PatientAdvocasyRequestInformation>()
                .Ignore(o => o.HealthcareProfessionals)
                .Ignore(o => o.TherapeuticArea)
                .Ignore(o => o.Country)
                .Ignore(o => o.State);
            Mapper.CreateMap<PatientAdvocasyRequestInformation, PatientAdvocasyRequestInformationViewModel>()
                 .ForMember(opt => opt.Country, opt => opt.MapFrom(c => c.Country.Title))
                .ForMember(opt => opt.State, opt => opt.MapFrom(c => c.State.Title))
                .ForMember(opt => opt.TherapeuticArea, opt => opt.MapFrom(c => c.TherapeuticArea.Title))
                .Ignore(o => o.HealthcareProfessionals)
                .Ignore(o => o.OrgLetterOfIntentFile)
                .Ignore(o => o.EventAgendaFile)
                .Ignore(o => o.SampleInvitationFile)
                .Ignore(o => o.HealthcareProfessionalsId)
                .Ignore(o => o.verificationPart);

            Mapper.CreateMap<DisplayAndExhibitRequestInformationViewModel, DisplayAndExhibitRequestInformation>()
                .Ignore(o => o.HealthcareProfessionals)
                .Ignore(o => o.TherapeuticArea)
                .Ignore(o => o.Country)
                .Ignore(o => o.State)
                .Ignore(o => o.TypeOfDisplayExhibit);
            Mapper.CreateMap<DisplayAndExhibitRequestInformation, DisplayAndExhibitRequestInformationViewModel>()
                .ForMember(opt => opt.Country, opt => opt.MapFrom(c => c.Country.Title))
                .ForMember(opt => opt.State, opt => opt.MapFrom(c => c.State.Title))
                .ForMember(opt => opt.TypeOfDisplayExhibit, opt => opt.MapFrom(c => c.TypeOfDisplayExhibit.Title))
                .ForMember(opt => opt.TherapeuticArea, opt => opt.MapFrom(c => c.TherapeuticArea.Title))
                .Ignore(o => o.HealthcareProfessionals)
                .Ignore(o => o.EventAgendaFile)
                .Ignore(o => o.HealthcareProfessionalsId)
                .Ignore(o => o.RequestLetterFile)
                .Ignore(o => o.verificationPart);

            Mapper.CreateMap<PaymentInformationViewModel, PaymentInformation>()
                .Ignore(o => o.BudgetBreakdown)
                .Ignore(o => o.OpportunitiesOrBenefits);
            Mapper.CreateMap<PaymentInformation, PaymentInformationViewModel>()
                .Ignore(o => o.BudgetBreakdown)
                .Ignore(o => o.OpportunitiesOrBenefits)
                .Ignore(o => o.BudgetBreakdownLinesId)
                .Ignore(o => o.FundingRequestLetterFile)
                .Ignore(o => o.DifferentLevelsOfFundingFile)
                .Ignore(o => o.OpportunitiesOrBenefitsId)
                .Ignore(o => o.verificationPart);

            Mapper.CreateMap<BudgetBreakdownLineViewModel, BudgetBreakdownLine>()
                .Ignore(o => o.FundingAndPaymentInformationId)
                .Ignore(o => o.FundingAndPaymentInformation);
            Mapper.CreateMap<BudgetBreakdownLine, BudgetBreakdownLineViewModel>();

            Mapper.CreateMap<HealthcareProfession, HealthcareProfessionViewModel>();
            Mapper.CreateMap<HealthcareProfessionViewModel, HealthcareProfession>()
                .Ignore(o => o.PatientAdvocasyRequestInformation)
                .Ignore(o => o.DisplayAndExhibitRequestInformation);

            Mapper.CreateMap<Benefit, BenefitViewModel>();
            Mapper.CreateMap<BenefitViewModel, Benefit>()
                .Ignore(o => o.FundingAndPaymentInformations);

            Mapper.CreateMap<User, RegisterViewModel>()
                .Ignore(o => o.ConfirmPassword);
            Mapper.CreateMap<RegisterViewModel, User>()
                .Ignore(o => o.Id)
                .Ignore(o => o.Requests)
                .Ignore(o => o.TherapeuticAreas)
                .Ignore(o => o.Approvers)
                .Ignore(o => o.PointPersonals)
                .Ignore(o => o.VoteResults);

            Mapper.CreateMap<User, UserViewModel>()
                .Ignore(o => o.PointPersonals)
                .Ignore(o => o.Approvers)
                .Ignore(o => o.ApproversId);
            Mapper.CreateMap<UserViewModel, User>()
                .Ignore(o => o.Password)
                .Ignore(o => o.Requests)
                .Ignore(o => o.TherapeuticAreas)
                .Ignore(o => o.VoteResults);

            Mapper.CreateMap<TherapeuticArea, TherapeuticAreaViewModel>();
            Mapper.CreateMap<TherapeuticAreaViewModel, TherapeuticArea>();

            Mapper.CreateMap<DisplayExhibitViewModel, TypeOfDisplayExhibit>();
            Mapper.CreateMap<TypeOfDisplayExhibit, DisplayExhibitViewModel>();

            Mapper.CreateMap<VoteResult, VoteResultViewModel>()
                .ForMember(opt => opt.Approver, opt => opt.MapFrom(c => c.Approver.Name));

            Mapper.AssertConfigurationIsValid();
        }
    }
}