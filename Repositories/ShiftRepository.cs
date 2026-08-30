using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace POS.Repositories
{
    public interface IShiftRepository
    {
        ShiftModel GetActiveShift(int userId);
        (bool Success, string Message, int ShiftId) ClockIn(int userId, string notes = null);
        (bool Success, string Message) ClockOut(int userId, string notes = null);
        Task<DataTable> GetShiftsAsync(int? userIdFilter = null, DateTime? dateFrom = null, DateTime? dateTo = null, string searchTerm = "");
        Task<DataTable> GetShiftsSummaryAsync(DateTime? dateFrom = null, DateTime? dateTo = null);
        Task<DataTable> GetActiveUsersForShiftsAsync();
        (bool Success, string Message) UpdateShift(int shiftId, DateTime clockInTime, DateTime? clockOutTime, string notes);
        (bool Success, string Message) DeleteShift(int shiftId);
    }

    public class ShiftRepository : IShiftRepository
    {
        public ShiftModel GetActiveShift(int userId) => DbHelper.GetActiveShift(userId);
        public (bool Success, string Message, int ShiftId) ClockIn(int userId, string notes = null) => DbHelper.ClockIn(userId, notes);
        public (bool Success, string Message) ClockOut(int userId, string notes = null) => DbHelper.ClockOut(userId, notes);
        public Task<DataTable> GetShiftsAsync(int? userIdFilter = null, DateTime? dateFrom = null, DateTime? dateTo = null, string searchTerm = "") => DbHelper.GetShiftsAsync(userIdFilter, dateFrom, dateTo, searchTerm);
        public Task<DataTable> GetShiftsSummaryAsync(DateTime? dateFrom = null, DateTime? dateTo = null) => DbHelper.GetShiftsSummaryAsync(dateFrom, dateTo);
        public Task<DataTable> GetActiveUsersForShiftsAsync() => DbHelper.GetActiveUsersForShiftsAsync();
        public (bool Success, string Message) UpdateShift(int shiftId, DateTime clockInTime, DateTime? clockOutTime, string notes) => DbHelper.UpdateShift(shiftId, clockInTime, clockOutTime, notes);
        public (bool Success, string Message) DeleteShift(int shiftId) => DbHelper.DeleteShift(shiftId);
    }
}
