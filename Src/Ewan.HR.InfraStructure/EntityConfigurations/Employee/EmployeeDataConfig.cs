using Ewan.HR.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ewan.HR.InfraStructure.EntityConfigurations
{
    internal sealed class EmployeeDataConfig : IEntityTypeConfiguration<EmployeeData>
    {
        public void Configure(EntityTypeBuilder<EmployeeData> builder)
        {
            builder.HasKey(e => e.Id);
            builder.ToTable("EmployeeData", "HR");
            //builder.HasOne(e => e.Department)
            //    .WithMany()
            //    .HasForeignKey(e => e.DepartementId);
        }
    }
}
