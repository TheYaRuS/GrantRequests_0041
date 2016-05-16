using GrantRequests.DAL.Entities;
using GrantRequests.DAL.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GrantRequests.Common;

namespace GrantRequests.WEB.Services
{
    public class ViewDataService:ServiceBase
    {
        private IEnumerable<Country> listOfCountries;
        private IEnumerable<State> listOfStates;
        private IEnumerable<TherapeuticArea> listOfTherapeuticArea;
        private IEnumerable<Benefit> listOfBenefits;
        private IEnumerable<HealthcareProfession> listOfHealthcareProfessional;
        private IEnumerable<TypeOfDisplayExhibit> listOfDisplayExhibit;
        private IEnumerable<User> listOfPointPersonals;
        private IEnumerable<User> listOfApprovers;

        public ViewDataService(UnitOfWork unitOfWork):base(unitOfWork) { }

        public IEnumerable<SelectListItem> ListOfCountries
        {
            get
            {
                if(listOfCountries == null)
                    listOfCountries = unitOfWork.Countries.GetAll();
                return listOfCountries.Select(o => new SelectListItem
                {
                    Text = o.Title,
                    Value = o.Id.ToString()
                });
            }
        }     

        public IEnumerable<SelectListItem> ListOfStates
        {
            get
            {
                if (listOfStates == null)
                    listOfStates = unitOfWork.States.GetAll();
                return listOfStates.Select(o => new SelectListItem
                {
                    Text = o.Abbreviation,
                    Value = o.Id.ToString()
                });
            }
        }
       
        public IEnumerable<SelectListItem> ListOfTherapeuticAreas(RequestType requestType)
        {
                if (listOfTherapeuticArea == null)
                    listOfTherapeuticArea = unitOfWork.TherapeuticAreas.Find(ta => ta.TypeRequest == requestType);
                return listOfTherapeuticArea.Select(o => new SelectListItem
                {
                    Text = o.Title,
                    Value = o.Id.ToString()
                });
        }

        public IEnumerable<SelectListItem> ListOfDisplayExhibit
        {
            get
            {
                if (listOfDisplayExhibit == null)
                    listOfDisplayExhibit = unitOfWork.TypeOfDisplayExhibits.GetAll();
                return listOfDisplayExhibit.Select(o => new SelectListItem
                {
                    Text = o.Title,
                    Value = o.Id.ToString()
                });
            }
        }

        public IEnumerable<SelectListItem> ListOfPointPersonals
        {
            get
            {
                if (listOfPointPersonals == null)
                    listOfPointPersonals = unitOfWork.Users.Find(u => u.Role == Role.PointPerson);
                return listOfPointPersonals.Select(o => new SelectListItem
                {
                    Text = o.Name,
                    Value = o.Id.ToString()
                });
            }
        }

        public IEnumerable<SelectListItem> ListOfApprovers
        {
            get
            {
                if (listOfApprovers == null)
                    listOfApprovers = unitOfWork.Users.Find(u => u.Role == Role.Approver);
                return listOfApprovers.Select(o => new SelectListItem
                {
                    Text = o.Name,
                    Value = o.Id.ToString()
                });
            }
        }

        public string GetNamePointPersonalById(int id)
        {
            var user = unitOfWork.Users.Get(id);
            if (user != null && user.Role == Role.PointPerson)
                return user.Name;
            return string.Empty;
        }

        public IEnumerable<SelectListItem> ListOfBenefits
        {
            get
            {
                if (listOfBenefits == null)
                    listOfBenefits = unitOfWork.Benefits.GetAll();
                return listOfBenefits.Select(o => new SelectListItem
                {
                    Text = o.Title,
                    Value = o.Id.ToString()
                });
            }
        }

        public IEnumerable<SelectListItem> ListOfHealthcareProfessionals
        {
            get
            {
                if (listOfHealthcareProfessional == null)
                    listOfHealthcareProfessional = unitOfWork.HealthcareProfessions.GetAll();
                return listOfHealthcareProfessional.Select(o => new SelectListItem
                {
                    Text = o.Title,
                    Value = o.Id.ToString()
                });
            }
        }

        public IEnumerable<SelectListItem> ListOfApproversByRequest(int requestId)
        {
            var request = unitOfWork.Requests.GetWithApproversList(requestId);
            return request.TherapeuticArea.PointPersonal.Approvers.Select(o => new SelectListItem
            {
                Text = o.Name,
                Value = o.Id.ToString()
            });
        }


    }
}