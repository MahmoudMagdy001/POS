using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace POS.Repositories
{
    public interface ISaleRepository
    {
        (bool Success, string Message, int SaleId) ProcessSaleTransaction(SaleModel sale, List<CartItemModel> items);
        Task<(bool Success, string Message, int SaleId)> ProcessSaleTransactionAsync(SaleModel sale, List<CartItemModel> items);
        SaleModel GetSaleById(int saleId);
        DataTable GetAllSalesDataTable(string dateFilter = "اليوم", DateTime? fromDate = null, DateTime? toDate = null, string searchTerm = "");
        Task<DataTable> GetAllSalesDataTableAsync(string dateFilter = "اليوم", DateTime? fromDate = null, DateTime? toDate = null, string searchTerm = "");
        DataTable GetSaleDetailsDataTable(int saleId);
        Task<DataTable> GetSaleDetailsDataTableAsync(int saleId);
        List<ReturnItemModel> GetSaleDetailsForReturn(int saleId);
        Task<List<ReturnItemModel>> GetSaleDetailsForReturnAsync(int saleId);
        (bool Success, string Message, int ReturnId) ProcessSaleReturnTransaction(int saleId, int? userId, string reason, List<ReturnItemModel> returnItems);
    }

    public class SaleRepository : ISaleRepository
    {
        public (bool Success, string Message, int SaleId) ProcessSaleTransaction(SaleModel sale, List<CartItemModel> items) => DbHelper.ProcessSaleTransaction(sale, items);
        public Task<(bool Success, string Message, int SaleId)> ProcessSaleTransactionAsync(SaleModel sale, List<CartItemModel> items) => DbHelper.ProcessSaleTransactionAsync(sale, items);
        public SaleModel GetSaleById(int saleId) => DbHelper.GetSaleById(saleId);
        public DataTable GetAllSalesDataTable(string dateFilter = "اليوم", DateTime? fromDate = null, DateTime? toDate = null, string searchTerm = "") => DbHelper.GetAllSalesDataTable(dateFilter, fromDate, toDate, searchTerm);
        public Task<DataTable> GetAllSalesDataTableAsync(string dateFilter = "اليوم", DateTime? fromDate = null, DateTime? toDate = null, string searchTerm = "") => DbHelper.GetAllSalesDataTableAsync(dateFilter, fromDate, toDate, searchTerm);
        public DataTable GetSaleDetailsDataTable(int saleId) => DbHelper.GetSaleDetailsDataTable(saleId);
        public Task<DataTable> GetSaleDetailsDataTableAsync(int saleId) => DbHelper.GetSaleDetailsDataTableAsync(saleId);
        public List<ReturnItemModel> GetSaleDetailsForReturn(int saleId) => DbHelper.GetSaleDetailsForReturn(saleId);
        public Task<List<ReturnItemModel>> GetSaleDetailsForReturnAsync(int saleId) => DbHelper.GetSaleDetailsForReturnAsync(saleId);
        public (bool Success, string Message, int ReturnId) ProcessSaleReturnTransaction(int saleId, int? userId, string reason, List<ReturnItemModel> returnItems) => DbHelper.ProcessSaleReturnTransaction(saleId, userId, reason, returnItems);
    }
}
