using System.Web.Mvc;
using GrantRequests.WEB.Models;
using GrantRequests.Common;
using GrantRequests.WEB.Services;
using GrantRequests.WEB.Filters;

namespace GrantRequests.WEB.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public HomeController(RequestService requestService)
        {
            this.requestService = requestService;
        }
        private readonly RequestService requestService;

        public ActionResult Index()
        {
            return View(requestService.GetRequestList(User.Identity));
        }

        [CustomRoleAuthorize(Role.Requestor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRequest(RequestType requestType)
        {
            var requestId = requestService.GetIdNewRequest(requestType, User.Identity);
            return RedirectToAction("CreatePartRequest", new { requestId = requestId });

        }


        [CustomRoleAuthorize(Role.Requestor,Role.Admin)]
        public ActionResult CreatePartRequest(int requestId)
        {
            var model = requestService.GetDetailsRequest(requestId);
            if (model.StatusRequest != StatusRequest.Draft && model.StatusRequest != StatusRequest.RFI)
                return RedirectToAction("Index");
            if (model.ContactInformation == null || !model.ContactInformation.IsValid)
            {
                model.ContactInformation = requestService.GetContactInformationViewModel(model.Id, model.RequestType);
                model.ContactInformation.verificationPart = model.VerificationPart();
                return View("CreateRequestStep1", model.ContactInformation);
            }
            if ((   model.RequestInformationSF == null || !model.RequestInformationSF.IsValid)
                && (model.RequestInformationPA == null || !model.RequestInformationPA.IsValid)
                && (model.RequestInformationDE == null || !model.RequestInformationDE.IsValid))
            {
                var requestInformation = requestService.GetRequestInformationViewModel(model.Id, model.RequestType);
                requestInformation.verificationPart = model.VerificationPart();
                switch (model.RequestType)
                {
                    case RequestType.ScientificFunding:
                        return View("CreateRequestStep2SF", requestInformation as ScientificFundingRequestInformationViewModel);
                    case RequestType.PatientAdvocasy:
                        return View("CreateRequestStep2PA", requestInformation as PatientAdvocasyRequestInformationViewModel); ;
                    case RequestType.DisplayAndExhibit:
                        return View("CreateRequestStep2DE", requestInformation as DisplayAndExhibitRequestInformationViewModel);
                }
                return View("CreateRequestStep2", model);
            }
            if (model.PaymentInformation == null || !model.PaymentInformation.IsValid)
            {
                model.PaymentInformation = requestService.GetPaymentInformationViewModel(model.Id, model.RequestType);
                model.PaymentInformation.verificationPart = model.VerificationPart();
                if (model.PaymentInformation.BudgetBreakdown == null)
                    model.PaymentInformation.BudgetBreakdown = PaymentInformationViewModel.GetDefaultBudgetBreakdown();
                return View("CreateRequestStep3", model.PaymentInformation);
            }
            requestService.ChangeStatus(requestId,StatusRequest.Pending);
            return View("Success");
        }


        [HttpGet]
        [CustomRoleAuthorize(Role.Requestor, Role.Admin, Role.Approver, Role.PointPerson)]
        public ActionResult MainView(int requestId)
        {
            var model = requestService.GetDetailsRequest(requestId);
            return View(RequestLongViewModel.detailsView, model);
        }

        #region Contact Information
        [HttpGet]
        [CustomRoleAuthorize(Role.Requestor, Role.Admin)]
        public ActionResult ContactInformation(int requestId, RequestType requestType, bool[] verificationPart)
        {
            var model = requestService.GetContactInformationViewModel(requestId, requestType);
            model.verificationPart = verificationPart;
            return View("CreateRequestStep1", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomRoleAuthorize(Role.Requestor, Role.Admin)]
        public ActionResult ContactInformation(ContactInformationViewModel model, string action)
        {
            if (action == "Save")
            {
                ModelState.Clear();
                Server.SetAllFileOnServer(model);
                requestService.AddInformationToRequest(model);
                var modelNew = requestService.GetContactInformationViewModel(model.RequestId, model.RequestType);
                modelNew.verificationPart = model.verificationPart;
                return View("CreateRequestStep1", modelNew);
            }
            if (action == "Submit" || action == "Proceed")
            {
                if (!ModelState.IsValid)
                    return View("CreateRequestStep1", model);
                model.IsValid = ModelState.IsValid;
                return SaveInformation(model);
            }
            return new HttpStatusCodeResult(404);

        }
        #endregion

        #region Request Information

        [HttpGet]
        [CustomRoleAuthorize(Role.Requestor, Role.Admin)]
        public ActionResult RequestInformation(int requestId, RequestType requestType, bool[] verificationPart)
        {
            var model = requestService.GetRequestInformationViewModel(requestId, requestType);
            model.verificationPart = verificationPart;
            switch (requestType)
            {
                case RequestType.ScientificFunding:
                    return View("CreateRequestStep2SF", model as ScientificFundingRequestInformationViewModel);
                case RequestType.PatientAdvocasy:
                    return View("CreateRequestStep2PA", model as PatientAdvocasyRequestInformationViewModel);
                case RequestType.DisplayAndExhibit:
                    return View("CreateRequestStep2DE", model as DisplayAndExhibitRequestInformationViewModel);
                default:
                    return new HttpStatusCodeResult(404);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomRoleAuthorize(Role.Requestor, Role.Admin)]
        public ActionResult RequestInformationSF(ScientificFundingRequestInformationViewModel model, string action)
        {
            if (action == "Save")
            {
                ModelState.Clear();
                Server.SetAllFileOnServer(model);
                requestService.AddInformationToRequest(model);
                var modelNew = requestService.GetRequestInformationViewModel(model.RequestId, model.RequestType);
                modelNew.verificationPart = model.verificationPart;
                return View("CreateRequestStep2SF", modelNew);
            }
            if (action == "Submit" || action == "Proceed")
            {
                if (!ModelState.IsValid)
                    return View("CreateRequestStep2SF", model);
                model.IsValid = ModelState.IsValid;
                return SaveInformation(model);
            }
            return new HttpStatusCodeResult(404);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomRoleAuthorize(Role.Requestor, Role.Admin)]
        public ActionResult RequestInformationPA(PatientAdvocasyRequestInformationViewModel model, string action)
        {
            if (action == "Save")
            {
                ModelState.Clear();
                Server.SetAllFileOnServer(model);
                requestService.AddInformationToRequest(model);
                var modelNew = requestService.GetRequestInformationViewModel(model.RequestId, model.RequestType);
                modelNew.verificationPart = model.verificationPart;
                return View("CreateRequestStep2PA", modelNew);
            }
            if (action == "Submit" || action == "Proceed")
            {
                if (!ModelState.IsValid)
                    return View("CreateRequestStep2PA", model);
                model.IsValid = ModelState.IsValid;
                return SaveInformation(model);
            }
            return new HttpStatusCodeResult(404);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomRoleAuthorize(Role.Requestor, Role.Admin)]
        public ActionResult RequestInformationDE(DisplayAndExhibitRequestInformationViewModel model, string action)
        {
            if (action == "Save")
            {
                ModelState.Clear();
                Server.SetAllFileOnServer(model);
                requestService.AddInformationToRequest(model);
                var modelNew = requestService.GetRequestInformationViewModel(model.RequestId, model.RequestType);
                modelNew.verificationPart = model.verificationPart;
                return View("CreateRequestStep2DE", modelNew);
            }
            if (action == "Submit" || action == "Proceed")
            {
                if (!ModelState.IsValid)
                    return View("CreateRequestStep2DE", model);
                model.IsValid = ModelState.IsValid;
                return SaveInformation(model);
            }
            return new HttpStatusCodeResult(404);
        }
        #endregion

        #region Payment Information
        [HttpGet]
        [CustomRoleAuthorize(Role.Requestor, Role.Admin)]
        public ActionResult PaymentInformation(int requestId, RequestType requestType, bool[] verificationPart)
        {
            var model = requestService.GetPaymentInformationViewModel(requestId, requestType);
            model.verificationPart = verificationPart;
            if (model.BudgetBreakdown == null)
                model.BudgetBreakdown = PaymentInformationViewModel.GetDefaultBudgetBreakdown();
            return View("CreateRequestStep3", model); ;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomRoleAuthorize(Role.Requestor, Role.Admin)]
        public ActionResult PaymentInformation(PaymentInformationViewModel model, string action)
        {
            if (action == "Save")
            {
                ModelState.Clear();
                Server.SetAllFileOnServer(model);
                requestService.AddInformationToRequest(model);
                var modelNew = requestService.GetPaymentInformationViewModel(model.RequestId, model.RequestType);
                modelNew.verificationPart = model.verificationPart;
                return View("CreateRequestStep3", modelNew);
            }
            if (action == "Submit" || action == "Proceed")
            {
                if (!ModelState.IsValid)
                    return View("CreateRequestStep3", model);
                model.IsValid = ModelState.IsValid;
                return SaveInformation(model);
            }
            return new HttpStatusCodeResult(404);

        }
        #endregion

        private RedirectToRouteResult SaveInformation<TModel>(TModel model)
        where TModel : InformationViewModelBase
        {
            Server.SetAllFileOnServer(model);
            requestService.AddInformationToRequest(model);
            return RedirectToAction("CreatePartRequest", new { requestId = model.RequestId });
        }

        #region Change request status

        [HttpGet]
        [CustomRoleAuthorize(Role.Requestor)]
        public ActionResult Submit(int requestId)
        {
            requestService.ChangeStatus(requestId, StatusRequest.Pending);
            return View("Success");
        }

        [HttpGet]
        [CustomRoleAuthorize(Role.Admin, Role.PointPerson)]
        public ActionResult RequestForInformation(int requestId)
        {

            requestService.ChangeStatus(requestId, StatusRequest.RFI);
            return RedirectToAction("MainView", new { requestId = requestId });

        }

        [HttpGet]
        [CustomRoleAuthorize(Role.Admin, Role.PointPerson)]
        public ActionResult Decline(int requestId)
        {
            requestService.ChangeStatus(requestId, StatusRequest.Declined);
            return RedirectToAction("MainView", new { requestId = requestId });
        }

        [HttpGet]
        [CustomRoleAuthorize(Role.Admin, Role.PointPerson)]
        public ActionResult TransferRequestType(int requestId)
        {
            return RedirectToAction("MainView", new { requestId = requestId });
        }

        [HttpGet]
        [CustomRoleAuthorize(Role.Admin, Role.PointPerson)]
        public ActionResult SubmitForDecision(int requestId)
        {
            requestService.ChangeStatus(requestId, StatusRequest.Submitted);
            return RedirectToAction("MainView", new { requestId = requestId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomRoleAuthorize(Role.Admin, Role.PointPerson)]
        public ActionResult ManageCommittee(CommitteeViewModel model)
        {
            if (ModelState.IsValid)
            {
                requestService.AddInformationToRequest(model);
            }

            return RedirectToAction("MainView", new { requestId = model.RequestId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomRoleAuthorize(Role.Approver)]
        public ActionResult ApproverVote(int requestId, VotingOptions vote)
        {
            requestService.ApproverVote(requestId, vote, User.Identity);

            return RedirectToAction("MainView", new { requestId = requestId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomRoleAuthorize(Role.PointPerson, Role.Admin)]
        public ActionResult PointPersonVote(int requestId, VotingOptions vote)
        {
            requestService.PointPersonVote(requestId, vote);
            return RedirectToAction("MainView", new { requestId = requestId });
        }


        #endregion

        [HttpGet]
        [CustomRoleAuthorize(Role.Requestor, Role.Admin, Role.Approver, Role.PointPerson)]
        public FileResult GetFile(string fileName)
        {
            return File(Server.GetFilePathFromServer(fileName), Server.GetFileContentTypeFromServer(fileName), fileName);
        }

        [CustomRoleAuthorize(Role.Requestor, Role.Admin)]
        public ActionResult AddLine()
        {
            return PartialView("EditView/_EditField", new BudgetBreakdownLineViewModel());
        }
    }
}