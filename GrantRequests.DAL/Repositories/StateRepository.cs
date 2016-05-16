using GrantRequests.DAL.EFContext;
using GrantRequests.DAL.Entities;

namespace GrantRequests.DAL.Repositories
{
    public class StateRepository : BaseRepository<State, GrantRequestsContext>
    {
        public StateRepository(GrantRequestsContext db) : base(db) { }

    }
}