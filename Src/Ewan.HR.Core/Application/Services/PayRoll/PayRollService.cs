using AutoMapper;
using Ewan.HR.Core.Application.Models.PayRoll;
using Ewan.HR.Core.Application.Services.Attendance;
using Ewan.HR.Core.Domain.Entities.PayRoll;
using Ewan.HR.Core.Domain.Interfaces;
using MongoDB.Driver.Linq;
using OfficeOpenXml;
using SharedCoreLibrary.Application.Models.Request;
using SharedCoreLibrary.Domain.Entities;
using System.Globalization;

namespace Ewan.HR.Core.Application.Services.PayRoll
{
    public class PayRollService : IPayRollService
    {
        private IHRUnitOfWork _unitOfWork;
        private IAttendanceService _attendanceService;
        private IMapper _mapper;
        public PayRollService(
            IHRUnitOfWork unitOfWork,
            IAttendanceService attendanceService,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _attendanceService = attendanceService;
            _mapper = mapper;
        }
        public List<PayRollAddVM> Calculate(string dateFrom)
        {
            var dateFrom1 = DateTime.Parse(dateFrom);
            var month = dateFrom.Split('/')[0];
            var endYear = dateFrom.Split('/')[1];
            var startYear = int.Parse(dateFrom.Split('/')[1]);

            if (month == "Jan")
                startYear = startYear - 1;

            var datesetting = _attendanceService.GetMonthSettings(month);

            var Attendance = _attendanceService.GatAttendanceData(null,
                DateTime.Parse($"{datesetting.StartMonth}/{datesetting.StartDay}/{startYear}").ToShortDateString(),
                 DateTime.Parse($"{datesetting.EndMonth}/{datesetting.EndDay}/{endYear}").ToShortDateString()
                ).GroupBy(c => c.EmployeeCode);

            var list = new List<PayRollAddVM>();

            foreach (var item in Attendance)
            {
                var monthDays = DateTime.DaysInMonth(item.FirstOrDefault().Date.Year, item.FirstOrDefault().Date.Month);
                list.Add(new PayRollAddVM
                {

                    EmployeeCode = item.Key,
                    EmployeeName = item.FirstOrDefault().EmployeeName,
                    MonthDays = monthDays,
                    DelayWithoutPermission = item.Sum(c => c.ChangeTime) > 0 ? 0 : item.Sum(c => c.ChangeTime) * -1,
                    Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(int.Parse(dateFrom1.Month.ToString())),
                    Year = endYear,
                });
            }
            Inseret(list);
            return list;
        }

        public PagedList<PayRollAddVM> GetAll(SearchModel searchModel)
        {
            return _mapper.Map<PagedList<PayRollAddVM>>(_unitOfWork.PayRollRepository.GetPagedList(null, searchModel));
        }

        public async Task<MemoryStream> PayRollDownload(string month)
        {

            try
            {
                MemoryStream memoryStream = new MemoryStream();
                var monthName = CultureInfo.GetCultureInfo("Ar").DateTimeFormat.GetMonthName(int.Parse(DateTime.Parse(month).Month.ToString()));
                var payroll = _unitOfWork.PayRollRepository.GetList(c => c.Month == monthName).ToList();

                if (payroll.Count == 0)
                {
                    payroll = _mapper.Map<List<PayRollData>>(Calculate(month));
                }

                using (ExcelPackage excelPackage = new ExcelPackage(memoryStream))
                {

                    if (payroll.Count != 0)
                    {
                        var worksheet = ReturnPayrollWorkSheet(excelPackage, $"PayRollSheet-{DateTime.Now.ToShortDateString()}", $"مسير رواتب المكتب الرئيسي والفروع لشهر {monthName}");
                        int row = 4;
                        foreach (var item in payroll)
                        {
                            worksheet.Cells[$"A{row}"].Value = item.EmployeeCode;
                            worksheet.Cells[$"B{row}"].Value = item.EmployeeName;
                            worksheet.Cells[$"C{row}"].Value = item.IdType;
                            worksheet.Cells[$"D{row}"].Value = item.IdNumber;
                            worksheet.Cells[$"E{row}"].Value = item.BankName;
                            worksheet.Cells[$"F{row}"].Value = item.IbanNumber;
                            worksheet.Cells[$"G{row}"].Value = item.WorkDate.ToString();
                            worksheet.Cells[$"H{row}"].Value = item.MonthDays.ToString();
                            worksheet.Cells[$"I{row}"].Value = item.DirectAbsent;
                            worksheet.Cells[$"J{row}"].Value = item.AbsentWithPermission;
                            worksheet.Cells[$"K{row}"].Value = item.AbsentWithouPermission;
                            worksheet.Cells[$"L{row}"].Value = item.MedicalAbsent;
                            worksheet.Cells[$"M{row}"].Value = item.DelayWithoutPermission;
                            worksheet.Cells[$"N{row}"].Value = item.DelayWithPermission;
                            worksheet.Cells[$"O{row}"].Value = item.DelayWithoutCutting;
                            row++;

                            worksheet.Cells[$"A1:O{row}"].AutoFitColumns();
                        }
                        await excelPackage.SaveAsync();
                    }
                    memoryStream.Position = 0;
                    return memoryStream;
                }

            }
            catch (Exception ex)
            {

                throw (ex);
            }
        }


        private ExcelWorksheet ReturnPayrollWorkSheet(ExcelPackage excelPackage, string sheetName, string Month)
        {
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add(sheetName);
            worksheet.Cells["A2"].Value = Month;
            worksheet.Cells["A3"].Value = "الرقم الوظيفي";
            worksheet.Cells["B3"].Value = "الاسم";
            worksheet.Cells["C3"].Value = "نوع الهوية";
            worksheet.Cells["D3"].Value = "رقم الهوية/الإقامة";
            worksheet.Cells["E3"].Value = "اسم البنك";
            worksheet.Cells["F3"].Value = "رقم الأيبان";
            worksheet.Cells["G3"].Value = "تاريخ التعيين";
            worksheet.Cells["H3"].Value = "أيام الشهر";
            worksheet.Cells["I3"].Value = "أيام الغياب المباشرة";
            worksheet.Cells["J3"].Value = "أيام الغياب باستئذان";
            worksheet.Cells["K3"].Value = "أيام الغياب بدون استئذان";
            worksheet.Cells["L3"].Value = "أيام الغياب مرضي";
            worksheet.Cells["M3"].Value = "التأخير بدون باستئذان";
            worksheet.Cells["N3"].Value = "التأخير باستئذان";
            worksheet.Cells["O3"].Value = "الإعفاء من خصم التأخير";
            return worksheet;
        }
        private void Inseret(List<PayRollAddVM> list)
        {
            try
            {
                _unitOfWork.PayRollRepository.AddRange(_mapper.Map<List<PayRollData>>(list));
                _unitOfWork.Complete();
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
