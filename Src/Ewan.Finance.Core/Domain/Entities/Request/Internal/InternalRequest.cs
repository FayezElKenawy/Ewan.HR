using Ewan.HR.Core.Domain.Entities.Request.MasterData;
using SharedCoreLibrary.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ewan.HR.Core.Domain.Entities.Request.Internal
{
    public class InternalRequest : AuditDataWithoutId
    {
        [Key]
        public string Id { get; set; }
        public string RequestId { get; set; }
        public string ItemId { get; set; }

        #region Relations

        [ForeignKey("ItemId")]
        public InternalRequestItem Items { get; set; }
        [ForeignKey("RequestId")]
        public RequestMasterData Request { get; set; }
        #endregion
    }
}
