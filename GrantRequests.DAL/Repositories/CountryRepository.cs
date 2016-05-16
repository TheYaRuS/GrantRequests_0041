using GrantRequests.DAL.EFContext;
using GrantRequests.DAL.Entities;


namespace GrantRequests.DAL.Repositories
{
    public class CountryRepository:BaseRepository<Country,GrantRequestsContext>
    {
        public CountryRepository(GrantRequestsContext db) : base(db) { }
    }
}
