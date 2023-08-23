using Ewan.Finance.Core;
using Ewan.HR.Core.Domain.Entities.Attendance;
using Ewan.HR.Core.Domain.Entities.PayRoll;
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
            //optionsBuilder.UseSqlServer("Server=.;Database=EwanERP.Hr.Local;Integrated Security=True");
            optionsBuilder.UseSqlServer("Server=tcp:ewanerp.database.windows.net,1433;Initial Catalog=ewanerp.hr.local;Persist Security Info=False;User ID=ewan_admin;Password=J35EX0NQVU5Y0G54$;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
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

        #region DbSets
        #region Attendance
        public DbSet<EmployeeAttendanceLog>EmployeeAttendanceLogs { get; set; }
        public DbSet<AttendanceMonthSettings> MonthSettings { get; set; }

        #endregion

        #region PayRoll
        public DbSet<PayRollData> PayRollData { get; set; }

        #endregion

        //#region Company
        //public DbSet<Department> Departments { get; set; }
        //#endregion

        //#region Employee
        //public DbSet<EmployeeData>Employees { get; set; }
        //#endregion
        #endregion
    }
}
