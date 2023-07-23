namespace Ewan.HR.Core.Application.Models.Request.InternalRequest
{
    public class AddInternalRequestVM : AddRequestVM
    {
        public List<InternalRequestItemsShowVM> RequestItems { get; set; }
    }
}
