namespace Ewan.HR.Core.Application.Models.Request.InternalRequest
{
    public class InternalRequestShowVM
    {
        public string TypeId { get; set; }
        public List<InternalRequestItemsShowVM> RequestItems { get; set; }
        public AddRequestVM EmployeeData { get; set; }
        public RequestVM RequestDetails { get; set; }
    }
}
