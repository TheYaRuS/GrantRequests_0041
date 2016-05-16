using GrantRequests.DAL.Repositories;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GrantRequests.WEB.Models;
using GrantRequests.DAL.Entities;
using GrantRequests.Common;
using System.Security.Principal;
using System;

namespace GrantRequests.WEB.Services
{
    public class RequestService : ServiceBase
    {
        public RequestService(UnitOfWork unitOfWork) : base(unitOfWork) { }

        private Request currentRequest;

        public IEnumerable<RequestShortViewModel> GetRequestList(IIdentity identity)
        {
            IEnumerable<Request> list;
            IEnumerable<RequestShortViewModel> model = new List<RequestShortViewModel>(); ;
            switch (identity.GetRole())
            {
                case Role.Admin:
                    {
                        list = unitOfWork.Requests.GetForAdminList();
                        break;
                    }
                case Role.Approver:
                    {
                        list = unitOfWork.Requests.GetForApproverList(identity.GetId());
                        break;
                    }

                case Role.PointPerson:
                    {
                        list = unitOfWork.Requests.GetForPoinPersonalList(identity.GetId());
                        break;
                    }
                case Role.Requestor:
                    {
                        list = unitOfWork.Requests.GetForRequestorList(identity.GetId());
                        break;
                    }
                default:
                    return model;
            }
            if (list != null)
                model = Mapper.Map<List<RequestShortViewModel>>(list);
            return model;
        }

        public IEnumerable<UserViewModel> GetUsersList()
        {
            var list = unitOfWork.Users.GetCompleteUsersInformation().ToArray();
            var model = Mapper.Map<UserViewModel[]>(list);
            for (int i = 0; i < model.Length; i++)
            {
                if (list[i].Approvers.Count > 0)
                    model[i].ApproversId = list[i].Approvers.Select(o => o.Id).ToArray();
            }
            return model;
        }

        public TherapeuticAreaViewModel GetTherapeuticAreaViewModel(int id)
        {
            var item = unitOfWork.TherapeuticAreas.Get(id);
            if (item != null)
                return Mapper.Map<TherapeuticAreaViewModel>(item);
            return new TherapeuticAreaViewModel();
        }

        public HealthcareProfessionViewModel GetHealthcareProfessionViewModel(int id)
        {
            var item = unitOfWork.HealthcareProfessions.Get(id);
            if (item != null)
                return Mapper.Map<HealthcareProfessionViewModel>(item);
            return new HealthcareProfessionViewModel();
        }

        public DisplayExhibitViewModel GetDisplayExhibitViewModel(int id)
        {
            var item = unitOfWork.TypeOfDisplayExhibits.Get(id);
            if (item != null)
                return Mapper.Map<DisplayExhibitViewModel>(item);
            return new DisplayExhibitViewModel();
        }

        public BenefitViewModel GetBenefitViewModel(int id)
        {
            var item = unitOfWork.Benefits.Get(id);
            if (item != null)
                return Mapper.Map<BenefitViewModel>(item);
            return new BenefitViewModel();
        }

        public UserViewModel GetUserViewModel(int id)
        {
            var item = unitOfWork.Users.Get(id);
            if (item != null)
                return Mapper.Map<UserViewModel>(item);
            return null;
        }

        public IEnumerable<HealthcareProfessionViewModel> GetHealthcareProfessionalsList()
        {
            var list = unitOfWork.HealthcareProfessions.GetAll();
            return Mapper.Map<HealthcareProfessionViewModel[]>(list);

        }

        public IEnumerable<DisplayExhibitViewModel> GetDisplayExibitsList()
        {
            var list = unitOfWork.TypeOfDisplayExhibits.GetAll();
            return Mapper.Map<DisplayExhibitViewModel[]>(list);
        }

        public IEnumerable<BenefitViewModel> GetBenefitsList()
        {
            var list = unitOfWork.Benefits.GetAll();
            return Mapper.Map<BenefitViewModel[]>(list);
        }

        public void ChangeApproversList(int id, int[] approversId)
        {
            var user = unitOfWork.Users.Get(id);
            if ((user != null) && (user.Role == Role.PointPerson))
            {
                user.Approvers.Clear();
                foreach (var item in approversId)
                    user.Approvers.Add(unitOfWork.Users.Get(item));
            }
            unitOfWork.Save();
        }

        public IEnumerable<TherapeuticAreaViewModel> GetTherapeuticAreasList()
        {
            var list = unitOfWork.TherapeuticAreas.GetAll();
            return Mapper.Map<TherapeuticAreaViewModel[]>(list);
        }

        public RequestLongViewModel GetDetailsRequest(int id)
        {
            var request = unitOfWork.Requests.GetCompleteInformationById(id);

            var requestViewModel = new RequestLongViewModel();
            Mapper.Map(request, requestViewModel);
            requestViewModel.TherapeuticArea = Mapper.Map<TherapeuticAreaViewModel>(request.TherapeuticArea);
            if (request.ContactInformationId.HasValue)
                requestViewModel.ContactInformation =
                    Mapper.Map<ContactInformationViewModel>(request.ContactInformation);
            if (request.ScientificFundingRequestInformationId.HasValue)
                requestViewModel.RequestInformationSF =
                    Mapper.Map<ScientificFundingRequestInformationViewModel>(request.RequestInformationSF);
            if (request.PatientAdvocasyRequestInformationId.HasValue)
            {
                requestViewModel.RequestInformationPA =
                    Mapper.Map<PatientAdvocasyRequestInformationViewModel>(request.RequestInformationPA);
                requestViewModel.RequestInformationPA.HealthcareProfessionals =
                    Mapper.Map<HealthcareProfessionViewModel[]>(request.RequestInformationPA.HealthcareProfessionals);
            }
            if (request.DisplayAndExhibitRequestInformationId.HasValue)
            {
                requestViewModel.RequestInformationDE =
                    Mapper.Map<DisplayAndExhibitRequestInformationViewModel>(request.RequestInformationDE);
                requestViewModel.RequestInformationDE.HealthcareProfessionals =
                    Mapper.Map<HealthcareProfessionViewModel[]>(request.RequestInformationDE.HealthcareProfessionals);
            }
            if (request.PaymentInformationId.HasValue)
            {
                requestViewModel.PaymentInformation =
                    Mapper.Map<PaymentInformationViewModel>(request.PaymentInformation);
                requestViewModel.PaymentInformation.OpportunitiesOrBenefits =
                    Mapper.Map<BenefitViewModel[]>(request.PaymentInformation.OpportunitiesOrBenefits);
                requestViewModel.PaymentInformation.BudgetBreakdown =
                    Mapper.Map<BudgetBreakdownLineViewModel[]>(request.PaymentInformation.BudgetBreakdown);
            }
            if (request.StatusRequest > StatusRequest.Draft)
                requestViewModel.Committee = new CommitteeViewModel
                {
                    CommitteeMeetingDate = request.CommitteeMeetingDate,
                    RecommendedAmount = request.PaymentInformation.TotalProgramCost,
                    RequestId = request.Id,
                    RequestType = request.RequestType,
                    ApproversId = request.Votes.Select(o => o.Id).ToArray()
                };
            if (request.Votes != null)
                requestViewModel.VoteResult = Mapper.Map<VoteResultViewModel[]>(request.Votes);


            return requestViewModel;
        }

        public void PointPersonVote(int requestId, VotingOptions vote)
        {
            var request = unitOfWork.Requests.GetWithCommitteeById(requestId);
            switch (vote)
            {
                case VotingOptions.Approve:
                    {
                        if (request.DateOfEvent <= DateTime.Now)
                            request.StatusRequest = StatusRequest.Occurred;
                        else
                            request.StatusRequest = StatusRequest.Scheduled;
                        break;
                    }
                case VotingOptions.RFI:
                    {
                        request.Votes.Clear();
                        ChangeStatus(requestId, StatusRequest.RFI);
                        break;
                    }
                case VotingOptions.Decline:
                    {
                        request.StatusRequest = StatusRequest.Declined;
                        break;
                    }
                case VotingOptions.Reset:
                    {
                        foreach (var item in request.Votes)
                        {
                            item.DateTime = null;
                            item.Voting = null;
                        }
                        break;
                    }
                default:
                    throw new Exception();
            }
            unitOfWork.Save();
        }

        public void ApproverVote(int requestId, VotingOptions vote, IIdentity identity)
        {
            var request = unitOfWork.Requests.GetWithCommitteeById(requestId);
            var voteResult = request.Votes.FirstOrDefault(o => o.UserId == identity.GetId());
            voteResult.DateTime = DateTime.Now;
            voteResult.Voting = vote;
            unitOfWork.Save();
        }

        public void ChangeStatus(int requestId, StatusRequest status)
        {
            var request = unitOfWork.Requests.GetCompleteInformationById(requestId);
            if (request == null)
                return;
            if (status == StatusRequest.RFI)
            {
                request.ContactInformation.IsValid = false;
                switch (request.RequestType)
                {
                    case RequestType.ScientificFunding:
                        request.RequestInformationSF.IsValid = false;
                        break;
                    case RequestType.PatientAdvocasy:
                        request.RequestInformationPA.IsValid = false;
                        break;
                    case RequestType.DisplayAndExhibit:
                        request.RequestInformationDE.IsValid = false;
                        break;
                }
                request.PaymentInformation.IsValid = false;
            }
            request.StatusRequest = status;
            unitOfWork.Save();

        }

        public int GetIdNewRequest(RequestType typeRequest, IIdentity identity)
        {
            var request = unitOfWork.Requests.CreateEmpty(typeRequest, identity.GetName());
            if (request == null)
                return -1;
            unitOfWork.Requests.Create(request);
            unitOfWork.Save();
            return request.Id;
        }

        TViewModel CreateInformationViewModel<TViewModel>(int requestId, RequestType requestType) where TViewModel : InformationViewModelBase, new()
        {
            var model = new TViewModel();
            model.RequestId = requestId;
            model.RequestType = requestType;
            return model;
        }

        public ContactInformationViewModel GetContactInformationViewModel(int requestId, RequestType requestType)
        {

            var contactInformation = unitOfWork.Requests.GetWithContactInformationById(requestId).ContactInformation;
            if (contactInformation != null)
                return Mapper.Map<ContactInformationViewModel>(contactInformation);
            var model = CreateInformationViewModel<ContactInformationViewModel>(requestId, requestType);
            model.CountryId = 1;
            return model;
        }

        public PaymentInformationViewModel GetPaymentInformationViewModel(int requestId, RequestType requestType)
        {
            var paymentInformation = unitOfWork.Requests.GetWithPaymentInformationById(requestId).PaymentInformation;
            if (paymentInformation != null)
            {
                var model = Mapper.Map<PaymentInformationViewModel>(paymentInformation);
                model.OpportunitiesOrBenefitsId = paymentInformation.OpportunitiesOrBenefits.Select(o => o.Id).ToArray();
                model.BudgetBreakdown = Mapper.Map<BudgetBreakdownLineViewModel[]>(paymentInformation.BudgetBreakdown);
                return model;
            }
            return CreateInformationViewModel<PaymentInformationViewModel>(requestId, requestType);
        }

        public InformationViewModelBase GetRequestInformationViewModel(int requestId, RequestType requestType)
        {
            var request = unitOfWork.Requests.GetWithRequestInformationById(requestId, requestType);
            switch (requestType)
            {
                case RequestType.ScientificFunding:
                    {
                        if (request.RequestInformationSF == null)
                            return CreateInformationViewModel<ScientificFundingRequestInformationViewModel>(requestId, requestType);
                        return Mapper.Map<ScientificFundingRequestInformationViewModel>(request.RequestInformationSF);
                    }
                case RequestType.PatientAdvocasy:
                    {
                        if (request.RequestInformationPA == null)
                            return CreateInformationViewModel<PatientAdvocasyRequestInformationViewModel>(requestId, requestType);
                        var model = Mapper.Map<PatientAdvocasyRequestInformationViewModel>(request.RequestInformationPA);
                        model.HealthcareProfessionalsId = request.RequestInformationPA.HealthcareProfessionals.Select(o => o.Id).ToArray();
                        return model;
                    }
                case RequestType.DisplayAndExhibit:
                    {
                        if (request.RequestInformationDE == null)
                            return CreateInformationViewModel<DisplayAndExhibitRequestInformationViewModel>(requestId, requestType);
                        var model = Mapper.Map<DisplayAndExhibitRequestInformationViewModel>(request.RequestInformationDE);
                        model.HealthcareProfessionalsId = request.RequestInformationDE.HealthcareProfessionals.Select(o => o.Id).ToArray();
                        return model;
                    }
                default:
                    return null;
            }
        }

        public void AddInformationToRequest(InformationViewModelBase model)
        {
            if (model is ContactInformationViewModel)
                AddInformationToRequest(model as ContactInformationViewModel);
            if (model is ScientificFundingRequestInformationViewModel)
                AddInformationToRequest(model as ScientificFundingRequestInformationViewModel);
            if (model is PatientAdvocasyRequestInformationViewModel)
                AddInformationToRequest(model as PatientAdvocasyRequestInformationViewModel);
            if (model is DisplayAndExhibitRequestInformationViewModel)
                AddInformationToRequest(model as DisplayAndExhibitRequestInformationViewModel);
            if (model is PaymentInformationViewModel)
                AddInformationToRequest(model as PaymentInformationViewModel);
            if (model is CommitteeViewModel)
                AddInformationToRequest(model as CommitteeViewModel);
            unitOfWork.Save();
        }

        void AddInformationToRequest(ContactInformationViewModel model)
        {
            var request = unitOfWork.Requests.GetWithContactInformationById(model.RequestId);
            var contactInformation = request.ContactInformation;
            if (contactInformation == null)
            {
                contactInformation = new ContactInformation();
                request.ContactInformation = contactInformation;

            }
            Mapper.Map(model, contactInformation);
        }

        void AddInformationToRequest(ScientificFundingRequestInformationViewModel model)
        {
            var request = unitOfWork.Requests.GetWithRequestInformationById(model.RequestId, RequestType.ScientificFunding);
            var requestInformation = request.RequestInformationSF;
            if (requestInformation == null)
            {
                requestInformation = new ScientificFundingRequestInformation();
                request.RequestInformationSF = requestInformation;

            }
            Mapper.Map(model, requestInformation);
            request.TherapeuticAreaId = requestInformation.TherapeuticAreaId;
            if (model.IsThereEvent)
            {
                request.EventName = model.Title;
                request.DateOfEvent = model.StartDate;
            }
        }

        void AddInformationToRequest(PatientAdvocasyRequestInformationViewModel model)
        {
            var request = unitOfWork.Requests.GetWithRequestInformationById(model.RequestId, RequestType.PatientAdvocasy);
            var requestInformation = request.RequestInformationPA;
            if (requestInformation == null)
            {
                requestInformation = new PatientAdvocasyRequestInformation();
                request.RequestInformationPA = requestInformation;

            }
            Mapper.Map(model, requestInformation);
            if (model.HealthcareProfessionalsId != null)
            {
                requestInformation.HealthcareProfessionals.Clear();
                foreach (var item in model.HealthcareProfessionalsId)
                    requestInformation.HealthcareProfessionals.Add(unitOfWork.HealthcareProfessions.Get(item));
            }

            request.TherapeuticAreaId = requestInformation.TherapeuticAreaId;
            if (true)
            {
                request.EventName = model.Title;
                request.DateOfEvent = model.StartDate;
            }
        }

        void AddInformationToRequest(DisplayAndExhibitRequestInformationViewModel model)
        {
            var request = unitOfWork.Requests.GetWithRequestInformationById(model.RequestId, RequestType.DisplayAndExhibit);
            var requestInformation = request.RequestInformationDE;
            if (requestInformation == null)
            {
                requestInformation = new DisplayAndExhibitRequestInformation();
                request.RequestInformationDE = requestInformation;

            }
            Mapper.Map(model, requestInformation);
            if (model.HealthcareProfessionalsId != null)
            {
                requestInformation.HealthcareProfessionals.Clear();
                foreach (var item in model.HealthcareProfessionalsId)
                    requestInformation.HealthcareProfessionals.Add(unitOfWork.HealthcareProfessions.Get(item));
            }
            request.TherapeuticAreaId = requestInformation.TherapeuticAreaId;
            if (model.IsThereEvent)
            {
                request.EventName = model.Title;
                request.DateOfEvent = model.StartDate;
            }
        }

        void AddInformationToRequest(PaymentInformationViewModel model)
        {
            var request = unitOfWork.Requests.GetWithPaymentInformationById(model.RequestId);
            var paymentInformation = request.PaymentInformation;
            if (paymentInformation == null)
            {
                paymentInformation = new PaymentInformation();
                request.PaymentInformation = paymentInformation;
            }
            Mapper.Map(model, paymentInformation);

            if (model.AreOpportunitiesOrBenefits == true && model.OpportunitiesOrBenefitsId != null)
            {
                paymentInformation.OpportunitiesOrBenefits.Clear();
                foreach (var item in model.OpportunitiesOrBenefitsId)
                {
                   var benefit = unitOfWork.Benefits.Get(item);
                    paymentInformation.OpportunitiesOrBenefits.Add(benefit);
                }
            }
            var budgetBreakdownLineList = new List<BudgetBreakdownLine>();
            Mapper.Map(model.BudgetBreakdown, budgetBreakdownLineList);
            paymentInformation.BudgetBreakdown.Clear();
            paymentInformation.BudgetBreakdown = budgetBreakdownLineList;
        }

        void AddInformationToRequest(CommitteeViewModel model)
        {
            var request = unitOfWork.Requests.GetWithCommitteeById(model.RequestId);

            foreach (var item in model.ApproversId)
                request.Votes.Add(new VoteResult
                {
                    Approver = unitOfWork.Users.Get(item),
                    Request = request
                });
            request.CommitteeMeetingDate = model.CommitteeMeetingDate;
            request.RecommendedAmount = model.RecommendedAmount;
            request.StatusRequest = StatusRequest.CommitteeReview;
        }

        public void AddItem(TherapeuticAreaViewModel model)
        {
            var item = unitOfWork.TherapeuticAreas.Get(model.Id);
            if (item == null)
            {
                item = new TherapeuticArea();
                unitOfWork.TherapeuticAreas.Create(item);
            }
            Mapper.Map(model, item);
            if (item.PointPersonalId.HasValue)
                item.PointPersonal = unitOfWork.Users.Get((int)item.PointPersonalId);
            unitOfWork.Save();
        }
        public void AddItem(HealthcareProfessionViewModel model)
        {
            var item = unitOfWork.HealthcareProfessions.Get(model.Id);
            if (item == null)
            {
                item = new HealthcareProfession();
                unitOfWork.HealthcareProfessions.Create(item);
            }
            Mapper.Map(model, item);
            unitOfWork.Save();
        }
        public void AddItem(DisplayExhibitViewModel model)
        {
            var item = unitOfWork.TypeOfDisplayExhibits.Get(model.Id);
            if (item == null)
            {
                item = new TypeOfDisplayExhibit();
                unitOfWork.TypeOfDisplayExhibits.Create(item);
            }
            Mapper.Map(model, item);
            unitOfWork.Save();
        }
        public void AddItem(BenefitViewModel model)
        {
            var item = unitOfWork.Benefits.Get(model.Id);
            if (item == null)
            {
                item = new Benefit();
                unitOfWork.Benefits.Create(item);
            }
            Mapper.Map(model, item);
            unitOfWork.Save();
        }
    }
}