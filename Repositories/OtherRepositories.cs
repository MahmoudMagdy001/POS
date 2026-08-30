using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace POS.Repositories
{
    public interface ICategoryRepository
    {
        List<CategoryModel> GetAllCategories();
        Task<List<CategoryModel>> GetAllCategoriesAsync();
        (bool Success, string Message, int CategoryId) SaveCategory(string categoryName, int? categoryId = null);
        (bool Success, string Message) DeleteCategory(int categoryId);
    }

    public class CategoryRepository : ICategoryRepository
    {
        public List<CategoryModel> GetAllCategories() => DbHelper.GetAllCategories();
        public Task<List<CategoryModel>> GetAllCategoriesAsync() => DbHelper.GetAllCategoriesAsync();
        public (bool Success, string Message, int CategoryId) SaveCategory(string categoryName, int? categoryId = null) => DbHelper.SaveCategory(categoryName, categoryId);
        public (bool Success, string Message) DeleteCategory(int categoryId) => DbHelper.DeleteCategory(categoryId);
    }

    public interface ISupplierRepository
    {
        List<SupplierModel> GetAllSuppliersList();
        DataTable GetAllSuppliersDataTable(string searchTerm = "");
        (bool Success, string Message, int SupplierId) SaveSupplier(SupplierModel supplier);
        (bool Success, string Message) DeleteSupplier(int supplierId);
    }

    public class SupplierRepository : ISupplierRepository
    {
        public List<SupplierModel> GetAllSuppliersList() => DbHelper.GetAllSuppliersList();
        public DataTable GetAllSuppliersDataTable(string searchTerm = "") => DbHelper.GetAllSuppliersDataTable(searchTerm);
        public (bool Success, string Message, int SupplierId) SaveSupplier(SupplierModel supplier) => DbHelper.SaveSupplier(supplier);
        public (bool Success, string Message) DeleteSupplier(int supplierId) => DbHelper.DeleteSupplier(supplierId);
    }

    public interface IPurchaseRepository
    {
        (bool Success, string Message, int PurchaseId) ProcessPurchaseTransaction(PurchaseModel purchase, List<PurchaseDetailModel> items, bool updateBuyPrice = true);
        DataTable GetAllPurchasesDataTable();
        Task<DataTable> GetAllPurchasesDataTableAsync();
        DataTable GetPurchaseDetailsDataTable(int purchaseId);
        Task<DataTable> GetPurchaseDetailsDataTableAsync(int purchaseId);
    }

    public class PurchaseRepository : IPurchaseRepository
    {
        public (bool Success, string Message, int PurchaseId) ProcessPurchaseTransaction(PurchaseModel purchase, List<PurchaseDetailModel> items, bool updateBuyPrice = true) => DbHelper.ProcessPurchaseTransaction(purchase, items, updateBuyPrice);
        public DataTable GetAllPurchasesDataTable() => DbHelper.GetAllPurchasesDataTable();
        public Task<DataTable> GetAllPurchasesDataTableAsync() => DbHelper.GetAllPurchasesDataTableAsync();
        public DataTable GetPurchaseDetailsDataTable(int purchaseId) => DbHelper.GetPurchaseDetailsDataTable(purchaseId);
        public Task<DataTable> GetPurchaseDetailsDataTableAsync(int purchaseId) => DbHelper.GetPurchaseDetailsDataTableAsync(purchaseId);
    }

    public interface IDashboardRepository
    {
        DashboardStatsModel GetDashboardKPIs(string dateFilter = "الكل");
        Task<DashboardStatsModel> GetDashboardKPIsAsync(string dateFilter = "الكل");
        DataTable GetTopSellingProducts(int topN = 5, string dateFilter = "الكل");
        Task<DataTable> GetTopSellingProductsAsync(int topN = 5, string dateFilter = "الكل");
        DataTable GetRecentTransactions(int topN = 10);
        Task<DataTable> GetRecentTransactionsAsync(int topN = 10);
        DataTable GetUrgentLowStockProducts(int topN = 10);
        Task<DataTable> GetUrgentLowStockProductsAsync(int topN = 10);
    }

    public class DashboardRepository : IDashboardRepository
    {
        public DashboardStatsModel GetDashboardKPIs(string dateFilter = "الكل") => DbHelper.GetDashboardKPIs(dateFilter);
        public Task<DashboardStatsModel> GetDashboardKPIsAsync(string dateFilter = "الكل") => DbHelper.GetDashboardKPIsAsync(dateFilter);
        public DataTable GetTopSellingProducts(int topN = 5, string dateFilter = "الكل") => DbHelper.GetTopSellingProducts(topN, dateFilter);
        public Task<DataTable> GetTopSellingProductsAsync(int topN = 5, string dateFilter = "الكل") => DbHelper.GetTopSellingProductsAsync(topN, dateFilter);
        public DataTable GetRecentTransactions(int topN = 10) => DbHelper.GetRecentTransactions(topN);
        public Task<DataTable> GetRecentTransactionsAsync(int topN = 10) => DbHelper.GetRecentTransactionsAsync(topN);
        public DataTable GetUrgentLowStockProducts(int topN = 10) => DbHelper.GetUrgentLowStockProducts(topN);
        public Task<DataTable> GetUrgentLowStockProductsAsync(int topN = 10) => DbHelper.GetUrgentLowStockProductsAsync(topN);
    }

    public interface ISettingsRepository
    {
        SystemSettingsModel GetSystemSettings();
        Task<SystemSettingsModel> GetSystemSettingsAsync();
        (bool Success, string Message) SaveSystemSettings(SystemSettingsModel settings);
        (bool Success, string Message) BackupDatabase(string backupFilePath);
        (bool Success, string Message) RestoreDatabase(string backupFilePath);
        (bool Success, string Message) ClearTransactionHistory(string adminUsername, string adminPassword);
    }

    public class SettingsRepository : ISettingsRepository
    {
        public SystemSettingsModel GetSystemSettings() => DbHelper.GetSystemSettings();
        public Task<SystemSettingsModel> GetSystemSettingsAsync() => DbHelper.GetSystemSettingsAsync();
        public (bool Success, string Message) SaveSystemSettings(SystemSettingsModel settings) => DbHelper.SaveSystemSettings(settings);
        public (bool Success, string Message) BackupDatabase(string backupFilePath) => DbHelper.BackupDatabase(backupFilePath);
        public (bool Success, string Message) RestoreDatabase(string backupFilePath) => DbHelper.RestoreDatabase(backupFilePath);
        public (bool Success, string Message) ClearTransactionHistory(string adminUsername, string adminPassword) => DbHelper.ClearTransactionHistory(adminUsername, adminPassword);
    }
}
