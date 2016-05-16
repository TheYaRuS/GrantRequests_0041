using GrantRequests.WEB.Models;
using GrantRequests.WEB.Services;
using System.Web.Mvc;
using GrantRequests.WEB.Filters;
using GrantRequests.Common;

namespace GrantRequests.WEB.Controllers
{
    [Authorize]
    [CustomRoleAuthorize(Role.Admin)]
    public class AdminController : Controller
    {
        public AdminController(RequestService requestService)
        {
            this.requestService = requestService;
        }
        private readonly RequestService requestService;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TherapeuticArea()
        {
            var model = requestService.GetTherapeuticAreasList();
            return PartialView("DetailsView/_ViewTherapeuticAreaList", model);
        }

        public ActionResult HealthcareProfessionals()
        {
            var model = requestService.GetHealthcareProfessionalsList();
            return PartialView("DetailsView/_ViewHealthcareProfessionalsList", model);
        }

        public ActionResult DisplayExibit()
        {
            var model = requestService.GetDisplayExibitsList();
            return PartialView("DetailsView/_ViewDisplayExibitsList", model);
        }

        public ActionResult Benefits()
        {            
            var model = requestService.GetBenefitsList();
            return PartialView("DetailsView/_ViewBenefitsList", model);
        }

        public ActionResult Users()
        {
            var model = requestService.GetUsersList();
            return PartialView("DetailsView/_ViewUserList", model);
        }

        [HttpGet]
        public ActionResult EditTherapeuticArea(int id = 0)
        {
            var model = requestService.GetTherapeuticAreaViewModel(id);
            return View("EditView/EditTherapeuticArea", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTherapeuticArea(TherapeuticAreaViewModel model)
        {
            requestService.AddItem(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditHealthcareProfessionals(int id = 0)
        {
            var model = requestService.GetHealthcareProfessionViewModel(id);
            return View("EditView/EditHealthcareProfession", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditHealthcareProfessionals(HealthcareProfessionViewModel model)
        {
            requestService.AddItem(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditDisplayExibit(int id = 0)
        {
            var model = requestService.GetDisplayExhibitViewModel(id);
            return View("EditView/EditDisplayExibit", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDisplayExibit(DisplayExhibitViewModel model)
        {
            requestService.AddItem(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditBenefits(int id = 0)
        {
            var model = requestService.GetBenefitViewModel(id);
            return View("EditView/EditBenefits", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBenefits(BenefitViewModel model)
        {
            requestService.AddItem(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeApproversList(int id,int[] approversId)
        {
            requestService.ChangeApproversList(id, approversId);
            return RedirectToAction("Index");
        }
    }
}