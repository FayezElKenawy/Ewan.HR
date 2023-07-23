namespace Ewan.HR.Core.Application.Enums.Request
{
    public static class RequestStatus
    {
        public static readonly string SendToManager = "Sent To D.Manager   تم الإرسال إلى المدير المباشر";
        public static readonly string SendToCEO = "Sent To CEO   تم الإرسال إلى الرئيس التنفيذي";
        public static readonly string DMApprove = "Approved By D.Manager تمت موافقة المدير المباشر";
        public static readonly string DMReject = "Reject By D.manager   تم الرفض من المدير المباشر";
        public static readonly string CEOApprove = "Approved By CEO  تمت موافقة  الرئيس التنفيذي";
        public static readonly string CEOReject = "Reject By CEO   تم الرفض من  الرئيس التنفيذي";
        public static readonly string RequestApprove = "Request Approved  تم إعتماد الطلب";
    }
    public static class ApprovalStatus
    {
        public static readonly string Approved = "Approved   تم الاعتماد";
        public static readonly string Rejected = "Rejected   تم الرفض";
        public static readonly string Pending = "Pending    في الانتظار";

    }
}