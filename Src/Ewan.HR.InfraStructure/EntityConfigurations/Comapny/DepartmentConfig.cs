using Ewan.HR.Core.Domain.Entities.Company;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ewan.HR.InfraStructure.EntityConfigurations.Comapny
{
    internal sealed class DepartmentConfig : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            //builder.HasKey(x=>x.Id);
            //builder.ToTable("Departments", "HR");
            //builder.HasData(
            //    new Department() { CreationDate = DateTime.Now, Title = "HR" },
            //         new Department() { CreationDate = DateTime.Now, Title = "Finance" },
            //         new Department() { CreationDate = DateTime.Now, Title = "OverSeas" },
            //         new Department() { CreationDate = DateTime.Now, Title = "IT" },
            //         new Department() { CreationDate = DateTime.Now, Title = "Operation" },
            //         new Department() { CreationDate = DateTime.Now, Title = "Sales" },
            //         new Department() { CreationDate = DateTime.Now, Title = "Maintenance" },
            //         new Department() { CreationDate = DateTime.Now, Title = "Projects" },
            //         new Department() { CreationDate = DateTime.Now, Title = "Herasat" });
        }
    }
}
