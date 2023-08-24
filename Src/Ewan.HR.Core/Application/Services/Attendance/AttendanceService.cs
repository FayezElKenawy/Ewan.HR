using AutoMapper;
using Ewan.HR.Core.Application.Models.Attendance;
using Ewan.HR.Core.Application.Services.External.BioTimeData.Clac.CalcAttendance;
using Ewan.HR.Core.Application.Services.External.BioTimeData.GetData;
using Ewan.HR.Core.Domain.Entities.Attendance;
using Ewan.HR.Core.Domain.Interfaces;
using OfficeOpenXml;
using OfficeOpenXml.VBA;
using SharedCoreLibrary.Application.Models.Request;
using SharedCoreLibrary.Application.Models.Request.DynamicSearch;
using SharedCoreLibrary.Domain.Entities;

namespace Ewan.HR.Core.Application.Services.Attendance
{
    public class AttendanceService : IAttendanceService
    {
        #region Private Members
        private readonly IHRUnitOfWork _unitOfWork;
        private readonly ICalcAttendanceService _calcAttendnace;
        private readonly IBioTimeService _bioTimeService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public AttendanceService(IHRUnitOfWork unitOfWork
            , ICalcAttendanceService calcAttendnace
            , IMapper mapper
            , IBioTimeService bioTimeService)
        {
            _unitOfWork = unitOfWork;
            _calcAttendnace = calcAttendnace;
            _mapper = mapper;
            _bioTimeService = bioTimeService;
        }
        #endregion

        #region Functions
        public string GetLAstId()
        {
            var lastAttendanceRow = _unitOfWork.AttendanceRepository.GetList()
                     .OrderByDescending(c => int.Parse(c.RowId))
                     .FirstOrDefault();

            if (lastAttendanceRow != null)
                return lastAttendanceRow.RowId.ToString();
            return string.Empty;
        }

        /// <summary>
        /// 1-Get all employees
        /// 2-Calculate attendance for every employee after last insersion
        /// 3-insert new attendance in db
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="emps"></param>
        /// <returns></returns>
        public async Task<bool> AddEmployeesAttendance(string start, string end, string[] emps)
        {
            try
            {
                var employeeList = await _bioTimeService.GetEmployeeData();
                var attendance = _unitOfWork.AttendanceRepository.GetList(c => c.Date.Date.ToString() == start);
                if (attendance != null)
                {//delete related records due to date
                    _unitOfWork.AttendanceRepository.RemoveRange(attendance);
                    _unitOfWork.Complete();
                }
                if (employeeList != null)
                {
                    var employeesNewAttendance = await _calcAttendnace.GetNewEmployeesAttendanceList(employeeList, start, end, "0");

                    if (employeesNewAttendance != null)
                        await _unitOfWork.AttendanceRepository.AddRangeAsync(_mapper.Map<IEnumerable<EmployeeAttendanceLog>>(employeesNewAttendance));

                    var saveResult = await _unitOfWork.CompleteAsync();
                    if (saveResult <= 0)
                        return false;

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns>Return Attendance Based on Date and Id</returns>
        public List<AttendanceDataVM> GatAttendanceData(string id, string startTime, string endTime)
        {

            if (id != "undefined" && !string.IsNullOrWhiteSpace(id))
            {
                var attendance1 = _mapper.Map<List<AttendanceDataVM>>(_unitOfWork.AttendanceRepository
                                                           .Where(c => c.EmployeeCode == id
                                                                                    && c.ClockIn >= DateTime.Parse(startTime)
                                                                                    && c.ClockOut <= DateTime.Parse(endTime)));
                return attendance1;
            }
            else if (startTime == null || endTime == null)
            {
                var attendnace = _mapper.Map<List<AttendanceDataVM>>(_unitOfWork.AttendanceRepository.GetList());
                return attendnace;
            }

            var attendance = _mapper.Map<List<AttendanceDataVM>>(_unitOfWork
                                                                .AttendanceRepository
                                                                .Where(c => c.ClockIn >= DateTime.Parse(startTime)
                                                                                 && c.ClockOut <= DateTime.Parse(endTime)));

            return attendance;
        }
        /// <summary>
        /// Get All Attendance 
        /// </summary>
        /// <returns>All Attendance List of the current month </returns>
        public List<AttendanceDataVM> GetAttendnaceList()
        {

            var t = _mapper.Map<List<AttendanceDataVM>>(_unitOfWork
                                                        .AttendanceRepository
                                                        .Where(c => c.Month == DateTime.Now.Date.Month.ToString()));
            return t;
        }
        /// <summary>
        /// Downlaod Attendance
        /// </summary>
        /// <returns>Return Detailed Attendance Excel File</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<MemoryStream> DownloadAttendnace(string id, string startTime, string endTime)
        {
            try
            {
                MemoryStream memoryStream = new MemoryStream();
                using (ExcelPackage excelPackage = new ExcelPackage(memoryStream))
                {
                    var attendance = GatAttendanceData(id, startTime, endTime).GroupBy(c => c.EmployeeCode).ToList();
                    if (attendance.Count != 0)
                    {
                        foreach (var item in attendance)
                        {
                            int row = 4;
                            var worksheet = RetrunAttendanceWorksheet(excelPackage, item.FirstOrDefault().EmployeeName, item.FirstOrDefault().EmployeeCode, $"from {startTime} to {endTime}");
                            var dateList = GetDates(DateTime.Parse(startTime), DateTime.Parse(endTime));
                            foreach (var item1 in dateList)
                            {
                                var date = item.Where(c => c.Date == item1.Date);
                                if (date.Count()!=0)
                                {
                                    worksheet.Cells[$"A{row}"].Value = date.FirstOrDefault().Date.ToString("dd-MM-yyyy");
                                    worksheet.Cells[$"B{row}"].Value = date.FirstOrDefault().EmployeeCode.ToString();
                                    worksheet.Cells[$"C{row}"].Value = date.FirstOrDefault().EmployeeName.ToString();
                                    worksheet.Cells[$"D{row}"].Value = date.FirstOrDefault().Day.ToString();
                                    worksheet.Cells[$"E{row}"].Value = date.FirstOrDefault().ClockIn.TimeOfDay.ToString();
                                    worksheet.Cells[$"F{row}"].Value = date.FirstOrDefault().ClockOut.TimeOfDay.ToString();
                                    worksheet.Cells[$"G{row}"].Value = date.FirstOrDefault().AbsentTime.ToString();
                                    worksheet.Cells[$"H{row}"].Value = date.FirstOrDefault().OverTime.ToString();
                                    worksheet.Cells[$"I{row}"].Value = date.FirstOrDefault().ChangeTime.ToString();
                                }
                                else
                                {
                                    worksheet.Cells[$"A{row}"].Value = item1.Date.ToString("dd-MM-yyyy");
                                    worksheet.Cells[$"B{row}"].Value = item.FirstOrDefault().EmployeeCode.ToString();
                                    worksheet.Cells[$"C{row}"].Value = item.FirstOrDefault().EmployeeName.ToString();
                                    worksheet.Cells[$"D{row}"].Value = item1.DayOfWeek.ToString();
                                    worksheet.Cells[$"E{row}"].Value = "0";
                                    worksheet.Cells[$"F{row}"].Value = "0";
                                    worksheet.Cells[$"G{row}"].Value = "0";
                                    worksheet.Cells[$"H{row}"].Value = "0";
                                    worksheet.Cells[$"I{row}"].Value = "0";
                                }

                                row++;
                            }
                            worksheet.Cells[$"A1:I{row}"].AutoFitColumns();
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

     

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Paged List</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<PagedList<AttendanceDataVM>> AttendanceDataPagedList(SearchModel searchModel)
        {
            try
            {
                var intervals = searchModel.SearchFields.ToList();
                var start = "";
                var end = "";
                var intervals1 = new List<SearchFieldModel> { };
                foreach (var item in intervals)
                {
                    if ((item.FieldName == "from" && !string.IsNullOrEmpty(item.Value)) || (item.FieldName == "to" && !string.IsNullOrEmpty(item.Value)))
                    {
                        start = item.Value.Substring(0, 10);
                        intervals1.Add(item);
                        end = item.Value.Substring(0, 10);
                        intervals1.Add(item);
                    }
                    else if(intervals.Count==3)
                    {//first load
                        searchModel.SearchFields.Clear();
                        searchModel.SearchFields.Add(new SearchFieldModel
                        { FieldName = "month", Operator = "equal", Value = DateTime.Now.Month.ToString() });
                        searchModel.SearchFields.Add(new SearchFieldModel
                        { FieldName = "date", Operator = "equal", Value = DateTime.Now.Year.ToString() });
                        break;
                    }

                }
                if (intervals1.Count != 0)
                {
                    var attendnace1 = _mapper.Map<List<AttendanceDataVM>>(await _unitOfWork.AttendanceRepository.GetListAsync(null));//get all

                    var newentities1 = attendnace1.Where(c => c.Date >= DateTime.Parse(start) && c.Date <= DateTime.Parse(end));//filter based on date from-to

                    var attendanceList1 = new PagedList<AttendanceDataVM>()
                    {
                        Entities = newentities1,
                        PagingData = new PagingData()
                        {
                            PageNumber = newentities1.Count() / 10,
                            PageSize = 10,
                            TotalCount = newentities1.Count()
                        }
                    };
                    return attendanceList1;
                }
                var attendnace = _mapper.Map<PagedList<AttendanceDataVM>>(await _unitOfWork.AttendanceRepository.GetPagedListAsync(null, searchModel));
                return attendnace;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public List<AttendanceDataVM> GetAttendnaceListByMonth(string month, string year)
        {

            var t = _mapper.Map<List<AttendanceDataVM>>(_unitOfWork
                                                        .AttendanceRepository
                                                        .Where(c => c.Month == month && c.Date.Year.ToString() == year));
            return t;
        }


        #region Attendance Setting
        public async Task<GetMonthSettingsVM> InsertSettings(int from, int to)
        {

            try
            {
                var months = (await _unitOfWork.MonthSettingsRepository.GetListAsync()).ToList();
                for (int i = 0; i < months.Count; i++)
                {
                    months[i].StartDay = from;
                    months[i].EndDay = to;
                    if (i == 0)
                    {
                        months[i].StartMonth = months[months.Count - 1].MonthName;
                    }
                    else
                    {
                        months[i].StartMonth = months[i - 1].MonthName;
                    }
                    months[i].EndMonth = months[i].MonthName;
                }
                _unitOfWork.MonthSettingsRepository.UpdateRange(months.ToArray());
                _unitOfWork.Complete();
                return await GetMonths();
            }
            catch (Exception)
            {

                throw;
            }


        }

        public async Task<GetMonthSettingsVM> GetMonths()
        {
            try
            {
                var months = _mapper.Map<List<MonthSettingsVM>>(await _unitOfWork.MonthSettingsRepository.GetListAsync());
                return new GetMonthSettingsVM()
                {
                    from = months.FirstOrDefault().StartDay.ToString(),
                    to = months.FirstOrDefault().EndDay.ToString()
                };
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public MonthSettingsVM GetMonthSettings(string month)
        {
            return _mapper.Map<MonthSettingsVM>(_unitOfWork.MonthSettingsRepository.Get(c => c.MonthName == month));
        }
        #endregion

        #region Private Function
        public static List<DateTime> GetDates(DateTime start, DateTime end)
        {
            var dateList=new List<DateTime>();
            var startDate= Enumerable.Range(1, DateTime.DaysInMonth(start.Year, start.Month))
                             .Select(day => new DateTime(start.Year, start.Month, day)) 
                             .ToList();
            dateList.AddRange(startDate.Where(c=>c.Date>=start));

            var endDate= Enumerable.Range(1, DateTime.DaysInMonth(end.Year, end.Month))  // Days: 1, 2 ... 31 etc.
                             .Select(day => new DateTime(end.Year, end.Month, day)) // Map each day to a date
                             .ToList();
            dateList.AddRange(startDate.Where(c => c.Date <= end));

            return dateList;
        }
        private ExcelWorksheet RetrunAttendanceWorksheet(ExcelPackage excelPackage, string empName, string sheetName, string interval)
        {
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add(sheetName);
            worksheet.Cells["A1"].Value = $"Empolyee Name {empName}";
            worksheet.Cells["A2"].Value = interval;
            worksheet.Cells["A3"].Value = "Date";
            worksheet.Cells["B3"].Value = "Employee Code";
            worksheet.Cells["C3"].Value = "Employee Name";
            worksheet.Cells["D3"].Value = "Day";
            worksheet.Cells["E3"].Value = "Time In";
            worksheet.Cells["F3"].Value = "Time Out";
            worksheet.Cells["G3"].Value = "Late";
            worksheet.Cells["H3"].Value = "OverTime";
            worksheet.Cells["I3"].Value = "Change Time";
            return worksheet;
        }
        #endregion

        #endregion
    }
}
