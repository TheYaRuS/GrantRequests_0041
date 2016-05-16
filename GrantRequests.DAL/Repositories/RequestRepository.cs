using GrantRequests.DAL.EFContext;
using GrantRequests.DAL.Entities;
using GrantRequests.Common;
using System.Data.Entity;
using System.Linq;
using System;
using System.Collections.Generic;

namespace GrantRequests.DAL.Repositories
{
    public class RequestRepository : BaseRepository<Request, GrantRequestsContext>
    {
        public RequestRepository(GrantRequestsContext db) : base(db) { }

        public Request CreateEmpty(RequestType requestType, string userName)
        {
            var user = db.Set<User>().FirstOrDefault(o => o.Name == userName);
            if (user.Role != Role.Requestor)
                return null;
            var request = new Request
            {
                RequestType = requestType,
                StatusRequest = StatusRequest.Draft,
                Requestor = user
            };           
            
            return request;
        }

        public int? GetRequestor(int requestId)
        {
           return db.Set<Request>().Where(r => r.Id == requestId).Select(r => r.RequestorId).FirstOrDefault();
        }

        public IEnumerable<Request> GetForAdminList()
        {
            return db.Set<Request>()
                .Include(o => o.TherapeuticArea)
                .ToList();
        }

        public IEnumerable<Request> GetForApproverList(int approverId)
        {
            return db.Set<Request>()
                .Include(o => o.TherapeuticArea)
                .Where(r => r.StatusRequest == StatusRequest.CommitteeReview 
                && r.Votes.Any(x => x.UserId == approverId));
        }       

        public IEnumerable<Request> GetForPoinPersonalList(int pointPersonalId)
        {
            return db.Set<Request>()
                .Include(o => o.TherapeuticArea)
                .Where(r => r.TherapeuticArea.PointPersonalId == pointPersonalId 
                && r.StatusRequest > StatusRequest.Draft)
                .ToList();
        }

        public IEnumerable<Request> GetForRequestorList(int requestorId)
        {
            return db.Set<Request>()
                .Include(o => o.TherapeuticArea)
                .Where(r => r.RequestorId == requestorId).ToList();
        }

        public Request GetWithApproversList(int id)
        {
            return db.Set<Request>()
                .Include(o => o.TherapeuticArea.PointPersonal.Approvers)
                .SingleOrDefault(o => o.Id == id);                
        }

        public Request GetCompleteInformationById(int id)
        {
            return db.Set<Request>()
                .Include(o => o.TherapeuticArea)
                //.Include(o => o.ContactInformation)
                .Include(o => o.ContactInformation.Country)
                .Include(o => o.ContactInformation.State)
                //.Include(o => o.RequestInformationSF)
                .Include(o => o.RequestInformationSF.TherapeuticArea)
                .Include(o => o.RequestInformationSF.Country)
                .Include(o => o.RequestInformationSF.State)
                /*.Include(o => o.RequestInformationPA)*/
                .Include(o => o.RequestInformationPA.HealthcareProfessionals)
                .Include(o => o.RequestInformationPA.TherapeuticArea)
                .Include(o => o.RequestInformationPA.Country)
                .Include(o => o.RequestInformationPA.State)
                /*.Include(o => o.RequestInformationDE)*/
                .Include(o => o.RequestInformationDE.HealthcareProfessionals)
                .Include(o => o.RequestInformationDE.TherapeuticArea)
                .Include(o => o.RequestInformationDE.Country)
                .Include(o => o.RequestInformationDE.State)
                .Include(o => o.RequestInformationDE.TypeOfDisplayExhibit)
                /*.Include(o => o.PaymentInformation)*/
                .Include(o => o.PaymentInformation.BudgetBreakdown)
                .Include(o => o.PaymentInformation.OpportunitiesOrBenefits)
                .Include(o => o.Requestor)
                .Include(o => o.Votes)
                .SingleOrDefault(o => o.Id == id);
        }

        public Request GetWithCommitteeById(int id)
        {
            return db.Set<Request>()
                .Include(o => o.Votes)
                .SingleOrDefault(o => o.Id == id);
        } 
           
        public Request GetWithContactInformationById(int id)
        {
            return db.Set<Request>()
                .Include(o => o.ContactInformation.Country)
                .Include(o => o.ContactInformation.State)
                .SingleOrDefault(o => o.Id == id);
        }

        public Request GetWithPaymentInformationById(int id)
        {
            return db.Set<Request>()
                .Include(o => o.PaymentInformation.BudgetBreakdown)
                .Include(o => o.PaymentInformation.OpportunitiesOrBenefits)
                .SingleOrDefault(o => o.Id == id);                
        }

        public Request GetWithRequestInformationById(int id, RequestType requestType)
        {
            switch (requestType)
            {
                case RequestType.ScientificFunding:
                    return db.Set<Request>()
                        .Include(o => o.RequestInformationSF.TherapeuticArea)
                        .Include(o => o.RequestInformationSF.Country)
                        .Include(o => o.RequestInformationSF.State)
                        .SingleOrDefault(o => o.Id == id);
                case RequestType.PatientAdvocasy:
                    return db.Set<Request>()
                        .Include(o => o.RequestInformationPA.HealthcareProfessionals)
                        .Include(o => o.RequestInformationPA.TherapeuticArea)
                        .Include(o => o.RequestInformationPA.Country)
                        .Include(o => o.RequestInformationPA.State)
                        .SingleOrDefault(o => o.Id == id);
                case RequestType.DisplayAndExhibit:
                    return db.Set<Request>()
                        .Include(o => o.RequestInformationDE.HealthcareProfessionals)
                        .Include(o => o.RequestInformationDE.TherapeuticArea)
                        .Include(o => o.RequestInformationDE.Country)
                        .Include(o => o.RequestInformationDE.State)
                        .SingleOrDefault(o => o.Id == id);
                default:
                    return null;
            }
        }        
    }
}
