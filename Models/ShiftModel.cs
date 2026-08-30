using System;

namespace POS
{
    public class ShiftModel
    {
        public int ShiftId { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public DateTime ClockInTime { get; set; }
        public DateTime? ClockOutTime { get; set; }
        public string Duration { get; set; }
        public double TotalHours { get; set; }
        public string Notes { get; set; }
        public bool IsActive => ClockOutTime == null;
    }
}
