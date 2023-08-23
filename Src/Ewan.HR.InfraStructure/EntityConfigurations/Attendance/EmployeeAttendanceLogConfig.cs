using Ewan.HR.Core.Domain.Entities.Attendance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ewan.HR.InfraStructure.EntityConfigurations.Attendance
{
    internal sealed class EmployeeAttendanceLogConfig : IEntityTypeConfiguration<EmployeeAttendanceLog>
    {
        public void Configure(EntityTypeBuilder<EmployeeAttendanceLog> builder)
        {
            builder.ToTable("EmployeeAttendanceLog", "HR");
            builder.HasKey(builder => builder.Id);
            builder.HasIndex(builder => builder.RowId).IsUnique();
        }
    }
}
