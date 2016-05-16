using GrantRequests.DAL.EFContext;
using GrantRequests.DAL.Entities;

namespace GrantRequests.DAL.Repositories
{
    public class TypeOfDisplayExhibitRepository : BaseRepository<TypeOfDisplayExhibit,GrantRequestsContext>
    {
        public TypeOfDisplayExhibitRepository(GrantRequestsContext db) : base(db) { }

    }
}
