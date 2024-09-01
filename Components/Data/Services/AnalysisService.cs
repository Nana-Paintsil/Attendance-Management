using AttendanceManagement.Models;
using AttendanceManagement.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagement.Components.Data.Services
{   
    public class AttendanceDataModel {
     public string DayOfWeek { get; set; } = default;
    public int EarlyAttendees { get; set; }
    public int LateAttendees { get; set; }
    public int DayOrder { get; set; }
}



    public class TeacherAttendanceDataModel
    {
        public string DayOfWeek { get; set; }
        public int EarlyDays { get; set; }
        public int LateDays { get; set; }
        public int DayOrder { get; set; }
    }

    public class AnalysisService
    {

        public bool ShowLogoutForm { get; private set; }

        // Event to notify UI about changes
        public event Action OnChange;

        public void HandleIsVisibleChanged(bool isVisible)
        {
            ShowLogoutForm = isVisible;
            NotifyStateChanged();
        }

        public void ToggleInvalidCredentials()
        {
            ShowLogoutForm = !ShowLogoutForm;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();


        private MyDbContext MyDbContext;
        public AnalysisService(MyDbContext myDbContext)
        {
            MyDbContext = myDbContext;
        }


        public TeacherAttendanceDataModel GenerateTeacherAttendanceData(Guid teacherId, DateTime fromDate, DateTime toDate, TimeSpan CutOffTime)
        {
            var dayOrder = new Dictionary<DayOfWeek, int>
    {
        { DayOfWeek.Monday, 1 },
        { DayOfWeek.Tuesday, 2 },
        { DayOfWeek.Wednesday, 3 },
        { DayOfWeek.Thursday, 4 },
        { DayOfWeek.Friday, 5 }
    };

            var allDaysOfWeek = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>();

            var attendanceRecords = MyDbContext.AttendanceRecords.ToList()
                .Where(record => record.EmployeeId == teacherId && record.Date >= fromDate && record.Date <= toDate)
                .ToList();

            var groupedData = allDaysOfWeek
                .Where(day => day != DayOfWeek.Sunday && day != DayOfWeek.Saturday)
                .Select(day => new TeacherAttendanceDataModel
                {
                    DayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(day),
                    EarlyDays = attendanceRecords.Count(record => record.Date.DayOfWeek == day && record.SignInTime.HasValue && record.SignInTime < record.Date.Date.Add(CutOffTime)),
                    LateDays = attendanceRecords.Count(record => record.Date.DayOfWeek == day && record.SignInTime.HasValue && record.SignInTime >= record.Date.Date.Add(CutOffTime)),
                    DayOrder = dayOrder[day]
                })
                .OrderBy(item => item.DayOrder)
                .ToList();


            return groupedData[0];
        }


        public List<AttendanceDataModel> GenerateAttendanceData(DateTime fromDate, DateTime toDate, TimeSpan CutOffTime)
        {
            var dayOrder = new Dictionary<DayOfWeek, int>
        {
            { DayOfWeek.Monday, 1 },
            { DayOfWeek.Tuesday, 2 },
            { DayOfWeek.Wednesday, 3 },
            { DayOfWeek.Thursday, 4 },
            { DayOfWeek.Friday, 5 }
        };

            var allDaysOfWeek = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>();

            var groupedData = allDaysOfWeek
            .Where(day => day != DayOfWeek.Sunday && day != DayOfWeek.Saturday)
                .GroupJoin(
                    MyDbContext.AttendanceRecords.ToList()
                        .Where(record => record.Date >= fromDate && record.Date <= toDate),
                    allDay => allDay,
                    record => record.Date.DayOfWeek,
                    (allDay, records) => new
                    {
                        DayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(allDay),
                        EarlyAttendees = records.Count(record => record.SignInTime.HasValue && record.SignInTime < record.Date.Date.Add(CutOffTime)),
                        LateAttendees = records.Count(record => record.SignInTime.HasValue && record.SignInTime >= record.Date.Date.Add(CutOffTime)),
                        DayOrder = dayOrder[allDay]
                    })
                .OrderBy(item => item.DayOrder)
                .Select(item => new AttendanceDataModel
                {
                    DayOfWeek = item.DayOfWeek,
                    EarlyAttendees = item.EarlyAttendees,
                    LateAttendees = item.LateAttendees,
                    DayOrder = item.DayOrder
                })
                .ToList();

            return groupedData;

        }
    }
  

    
}
