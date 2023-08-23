using Ewan.HR.Core.Domain.Entities.PayRoll;
using Microsoft.EntityFrameworkCore;

namespace Ewan.HR.InfraStructure.EntityConfigurations.PayRoll
{
    internal sealed class PayRollConfig : IEntityTypeConfiguration<PayRollData>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<PayRollData> builder)
        {
            builder.ToTable("PayRollData", "HR");
            builder.HasKey(x => x.Id);
        }
    }
}
