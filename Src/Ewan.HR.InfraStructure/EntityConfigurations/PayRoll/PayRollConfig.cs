using DocumentFormat.OpenXml.Presentation;
using Ewan.HR.Core.Domain.Entities.Attendance;
using Ewan.HR.Core.Domain.Entities.PayRoll;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewan.HR.InfraStructure.EntityConfigurations.PayRoll
{
    internal sealed class PAyRollConfig : IEntityTypeConfiguration<PayRollData>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<PayRollData> builder)
        {
            builder.ToTable("PayRollData", "HR");
            builder.HasKey(x => x.Id);
        }
    }
}
