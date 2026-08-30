using System;

namespace POS
{
    public class ShiftSummaryModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public int TotalShifts { get; set; }
        public double TotalHours { get; set; }
        public double AverageHoursPerShift { get; set; }
        public DateTime? LastClockIn { get; set; }
    }
}
