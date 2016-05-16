using System.Data.Entity;
using GrantRequests.DAL.Entities;

namespace GrantRequests.DAL.EFContext
{
    public class GrantRequestsContext:DbContext
    {
        public GrantRequestsContext()
        {
            Configuration.LazyLoadingEnabled = false;
        }
        static GrantRequestsContext()
        {
            Database.SetInitializer(new GrantRequestsContextInitializer());
        }
        public GrantRequestsContext(string connectionString):base(connectionString){ }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(c => c.PointPersonals)
                .WithMany(s => s.Approvers)
                .Map(t => t.MapLeftKey("PointPersonalId")
                .MapRightKey("ApproverId")
                .ToTable("PointPersonalApprover"));
        }
    }
}
