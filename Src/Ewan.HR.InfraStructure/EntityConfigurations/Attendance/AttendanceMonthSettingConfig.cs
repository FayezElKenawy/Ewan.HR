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
            builder.HasData(
             new AttendanceMonthSettings() {CreationDate = DateTime.Now,MonthName="يناير"},
                new AttendanceMonthSettings() { CreationDate = DateTime.Now, MonthName = "فبراير"},
                new AttendanceMonthSettings() { CreationDate = DateTime.Now, MonthName = "مارس"},
                new AttendanceMonthSettings() { CreationDate = DateTime.Now, MonthName = "أبريل" },
                new AttendanceMonthSettings() { CreationDate = DateTime.Now, MonthName = "مايو"},
                new AttendanceMonthSettings() { CreationDate = DateTime.Now, MonthName = "يونيه"},
                new AttendanceMonthSettings() { CreationDate = DateTime.Now, MonthName = "يوليو"},
                new AttendanceMonthSettings() { CreationDate = DateTime.Now, MonthName = "أغسطس"},
                new AttendanceMonthSettings() { CreationDate = DateTime.Now, MonthName = "سبتمبر"},
                new AttendanceMonthSettings() { CreationDate = DateTime.Now, MonthName = "أكتوبر" },
                new AttendanceMonthSettings() { CreationDate = DateTime.Now, MonthName = "نوفمبر"},
                new AttendanceMonthSettings() { CreationDate = DateTime.Now, MonthName = "ديسمبر"}
                );
        }
    }
}
