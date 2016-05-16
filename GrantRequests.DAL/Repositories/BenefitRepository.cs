using GrantRequests.DAL.EFContext;
using GrantRequests.DAL.Entities;

namespace GrantRequests.DAL.Repositories
{
    public class BenefitRepository : BaseRepository<Benefit, GrantRequestsContext>
    {
        public BenefitRepository(GrantRequestsContext db) : base(db) { }


    }
}
