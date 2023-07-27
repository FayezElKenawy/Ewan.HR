using Ewan.HR.Core.Domain.Entities.Attendance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewan.HR.InfraStructure.EntityConfigurations.Attendance
{
    internal sealed class EmployeeAttendanceLogConfig : IEntityTypeConfiguration<EmployeeAttendanceLog>
    {
        public void Configure(EntityTypeBuilder<EmployeeAttendanceLog> builder)
        {
            builder.ToTable("EmployeeAttendanceLog", "HR");
            builder.HasKey(builder => builder.Id);
        }
    }
}
