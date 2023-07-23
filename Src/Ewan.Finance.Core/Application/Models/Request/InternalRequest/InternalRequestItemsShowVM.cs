using Ewan.HR.Core.Application.Models.Employee;

namespace Ewan.HR.Core.Application.Models.Request.InternalRequest
{
    public class InternalRequestItemsShowVM
    {
        public string Id { get; set; }
        public string ArName { get; set; }
        public string EnName { get; set; }
        public bool IsSelected { get; set; }
        public EmployeeVM Employee { get; set; }
    }
}
