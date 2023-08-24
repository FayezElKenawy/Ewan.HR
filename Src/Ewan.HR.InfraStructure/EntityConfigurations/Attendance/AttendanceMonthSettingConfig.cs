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
    internal class AttendanceMonthSettingConfig : IEntityTypeConfiguration<AttendanceMonthSettings>
    {
        public void Configure(EntityTypeBuilder<AttendanceMonthSettings> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
