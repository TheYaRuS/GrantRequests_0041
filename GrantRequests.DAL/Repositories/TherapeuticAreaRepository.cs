using GrantRequests.DAL.EFContext;
using GrantRequests.DAL.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GrantRequests.DAL.Repositories
{
    public class TherapeuticAreaRepository : BaseRepository<TherapeuticArea,GrantRequestsContext>
    {
        public TherapeuticAreaRepository(GrantRequestsContext db) : base(db) { }

        public override IEnumerable<TherapeuticArea> GetAll()
        {
            return db.Set<TherapeuticArea>().Include(ta => ta.PointPersonal).ToList();
        }
    }
}
