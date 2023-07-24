using Ewan.Finance.Core;
using Ewan.HR.Core.Domain.Entities.Attendance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Ewan.HR.InfraStructure.Contexts
{
    public class HrContext: DbContext
    {
        public HrContext() { }

        public HrContext(DbContextOptions<HrContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=ewan1.uksouth.cloudapp.azure.com;Database=EwanERP.HR.Dev;MultipleActiveResultSets=true;User Id=nawe1;Password=ylaQB@4$1bbZ;");
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

        public DbSet<EmployeeAttendanceLog> EmployeeAttendanceLog { get; set; }

    }
}
