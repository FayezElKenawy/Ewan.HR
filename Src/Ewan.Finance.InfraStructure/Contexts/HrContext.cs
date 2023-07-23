using Ewan.Finance.Core;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Ewan.HR.InfraStructure.Contexts
{
    public class HrContext: DbContext
    {
        public HrContext() { }

        public HrContext(DbContextOptions<HrContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);

            configurationBuilder.Properties<string>()
                .HaveMaxLength(Consts.NameLength);

            configurationBuilder.Properties<decimal>()
                .HavePrecision(18, 2);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(),
                conf => conf.IsNotPublic && conf.IsSealed && conf.Namespace.Contains(Consts.ConfigNameSpace));
        }
    }
}
