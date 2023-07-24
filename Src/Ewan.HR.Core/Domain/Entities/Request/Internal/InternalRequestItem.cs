using SharedCoreLibrary.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Ewan.HR.Core.Domain.Entities.Request.Internal
{
    public class InternalRequestItem : AuditDataWithoutId
    {
        [Key]
        public string Id { get; set; }
        public string ArName { get; set; }
        public string EnName { get; set; }

    }
}
