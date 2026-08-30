using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using POS.Repositories;

namespace POS.Services
{
    public interface IAuthService
    {
        (bool Success, string Message, UserModel User) Login(string username, string password);
        Task<(bool Success, string Message, UserModel User)> LoginAsync(string username, string password);
    }

    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository = null)
        {
            _userRepository = userRepository ?? new UserRepository();
        }

        public (bool Success, string Message, UserModel User) Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return (false, "يرجى إدخال اسم المستخدم وكلمة المرور.", null);

            return _userRepository.Authenticate(username, password);
        }

        public async Task<(bool Success, string Message, UserModel User)> LoginAsync(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return (false, "يرجى إدخال اسم المستخدم وكلمة المرور.", null);

            return await _userRepository.AuthenticateAsync(username, password);
        }
    }

    public interface ISaleService
    {
        Task<(bool Success, string Message, int SaleId)> ProcessSaleAsync(SaleModel sale, List<CartItemModel> items);
        (bool Success, string Message, int ReturnId) ProcessReturn(int saleId, int? userId, string reason, List<ReturnItemModel> returnItems);
    }

    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;

        public SaleService(ISaleRepository saleRepository = null)
        {
            _saleRepository = saleRepository ?? new SaleRepository();
        }

        public async Task<(bool Success, string Message, int SaleId)> ProcessSaleAsync(SaleModel sale, List<CartItemModel> items)
        {
            if (items == null || items.Count == 0)
                return (false, "عربة المبيعات فارغة، لا يمكن إتمام الفاتورة.", 0);

            return await _saleRepository.ProcessSaleTransactionAsync(sale, items);
        }

        public (bool Success, string Message, int ReturnId) ProcessReturn(int saleId, int? userId, string reason, List<ReturnItemModel> returnItems)
        {
            if (returnItems == null || returnItems.Count == 0)
                return (false, "لم يتم تحديد أي أصناف للإرجاع.", 0);

            return _saleRepository.ProcessSaleReturnTransaction(saleId, userId, reason, returnItems);
        }
    }

    public interface IShiftService
    {
        ShiftModel GetActiveShift(int userId);
        (bool Success, string Message, int ShiftId) ClockIn(int userId, string notes = null);
        (bool Success, string Message) ClockOut(int userId, string notes = null);
    }

    public class ShiftService : IShiftService
    {
        private readonly IShiftRepository _shiftRepository;

        public ShiftService(IShiftRepository shiftRepository = null)
        {
            _shiftRepository = shiftRepository ?? new ShiftRepository();
        }

        public ShiftModel GetActiveShift(int userId) => _shiftRepository.GetActiveShift(userId);
        public (bool Success, string Message, int ShiftId) ClockIn(int userId, string notes = null) => _shiftRepository.ClockIn(userId, notes);
        public (bool Success, string Message) ClockOut(int userId, string notes = null) => _shiftRepository.ClockOut(userId, notes);
    }
}
