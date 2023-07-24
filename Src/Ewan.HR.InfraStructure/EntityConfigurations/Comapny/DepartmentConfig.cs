using Ewan.HR.Core.Domain.Entities.Company;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ewan.HR.InfraStructure.EntityConfigurations.Comapny
{
    public class DepartmentConfig : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasData(
                new Department() { DepartmentId = "1d6654da-91d6-4502-8428-68f73c54faad", CreationDate = DateTime.Now, Title = "HR" },
                     new Department() { DepartmentId = "46465fe5-385d-420a-9d9d-3d694c4488df", CreationDate = DateTime.Now, Title = "Finance" },
                     new Department() { DepartmentId = "777fd61a-4656-4aaa-b650-c9f5709bc572", CreationDate = DateTime.Now, Title = "OverSeas" },
                     new Department() { DepartmentId = "3f18da42-afed-4577-88d0-e07de05da9c7", CreationDate = DateTime.Now, Title = "IT" },
                     new Department() { DepartmentId = "78d6b1a4-37ac-4cf9-b0bf-2a773d70acfe", CreationDate = DateTime.Now, Title = "Operation" },
                     new Department() { DepartmentId = "96d8aed5-e40a-4418-a692-4b06c260a31a", CreationDate = DateTime.Now, Title = "Sales" },
                     new Department() { DepartmentId = "e57bc8c0-cf43-42d5-9f8b-2f9797b906da", CreationDate = DateTime.Now, Title = "Maintenance" },
                     new Department() { DepartmentId = "07f79fab-4fc2-410b-922a-2798ebd26cff", CreationDate = DateTime.Now, Title = "Projects" },
                     new Department() { DepartmentId = "bb92ecba-9058-49fd-a07e-8fd8aab2ef96", CreationDate = DateTime.Now, Title = "Herasat" }
                                   );
        }
    }
}
