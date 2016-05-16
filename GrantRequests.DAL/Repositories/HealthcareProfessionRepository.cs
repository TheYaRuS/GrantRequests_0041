using GrantRequests.DAL.EFContext;
using GrantRequests.DAL.Entities;

namespace GrantRequests.DAL.Repositories
{
    public class HealthcareProfessionRepository : BaseRepository<HealthcareProfession, GrantRequestsContext>
    {
        public HealthcareProfessionRepository(GrantRequestsContext db) : base(db) { }

    }
}
