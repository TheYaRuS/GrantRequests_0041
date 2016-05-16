using GrantRequests.DAL.EFContext;
using GrantRequests.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GrantRequests.DAL.Repositories
{
   public class UserRepository:BaseRepository<User,GrantRequestsContext>
    {
        public UserRepository(GrantRequestsContext db) : base(db) { }

        public IEnumerable<User> GetCompleteUsersInformation()
        {
            return db.Set<User>()
                .Include(o => o.Approvers)
                .Include(o => o.PointPersonals)
                .ToList();

        }

    }
}
