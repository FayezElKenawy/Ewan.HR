using Ewan.HR.Core.Domain.Entities.Employee;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ewan.HR.InfraStructure.EntityConfigurations.Employee
{
    internal sealed class EmployeeDataConfig : IEntityTypeConfiguration<Core.Domain.Entities.Employee.Employee>
    {
        public void Configure(EntityTypeBuilder<Core.Domain.Entities.Employee.Employee> builder)
        {
            builder.HasKey(e => e.Id);
            builder.ToTable("EmployeeData", "HR");
            //builder.HasOne(e => e.Department)
            //    .WithMany()
            //    .HasForeignKey(e => e.DepartementId);
        }
    }
}
