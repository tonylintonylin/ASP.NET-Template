using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ASP.NET.TEMPLATE.Domain
{
    public partial class UltraContext : DbContext
    {
        public UltraContext() { }

        public UltraContext(DbContextOptions<UltraContext> options) : base(options) { }

        public virtual DbSet<Error> Error { get; set; }
        public virtual DbSet<Login> Login { get; set; }
        public virtual DbSet<ThingA> ThingA { get; set; }
        public virtual DbSet<ThingB> ThingB { get; set; }
        public virtual DbSet<ThingC> ThingC { get; set; }
        public virtual DbSet<ThingD> ThingD { get; set; }
        public virtual DbSet<ThingE> ThingE { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Viewed> Viewed { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var currentUser = ServiceLocator.Resolve<ICurrentUser>();

            foreach (var entry in ChangeTracker.Entries<IAuditable>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = entry.Entity.ChangedBy = currentUser.Id;
                        entry.Entity.CreatedOn = entry.Entity.ChangedOn = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ChangedBy = currentUser.Id;
                        entry.Entity.ChangedOn = DateTime.UtcNow;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
