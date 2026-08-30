using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace POS.Repositories
{
    public interface IProductRepository
    {
        ProductModel GetById(int productId);
        Task<ProductModel> GetByIdAsync(int productId);
        ProductModel GetByBarcode(string barcode);
        Task<ProductModel> GetByBarcodeAsync(string barcode);
        DataTable GetAllProductsDataTable(string searchTerm = "", int? categoryId = null, bool lowStockOnly = false);
        Task<DataTable> GetAllProductsDataTableAsync(string searchTerm = "", int? categoryId = null, bool lowStockOnly = false);
        (bool Success, string Message, int ProductId) SaveProduct(ProductModel product);
        (bool Success, string Message) DeleteProduct(int productId);
        string GenerateUniqueBarcode();
    }

    public class ProductRepository : IProductRepository
    {
        public ProductModel GetById(int productId) => DbHelper.GetProductById(productId);
        public Task<ProductModel> GetByIdAsync(int productId) => DbHelper.GetProductByIdAsync(productId);
        public ProductModel GetByBarcode(string barcode) => DbHelper.GetProductByBarcode(barcode);
        public Task<ProductModel> GetByBarcodeAsync(string barcode) => DbHelper.GetProductByBarcodeAsync(barcode);
        public DataTable GetAllProductsDataTable(string searchTerm = "", int? categoryId = null, bool lowStockOnly = false) => DbHelper.GetAllProductsDataTable(searchTerm, categoryId, lowStockOnly);
        public Task<DataTable> GetAllProductsDataTableAsync(string searchTerm = "", int? categoryId = null, bool lowStockOnly = false) => DbHelper.GetAllProductsDataTableAsync(searchTerm, categoryId, lowStockOnly);
        public (bool Success, string Message, int ProductId) SaveProduct(ProductModel product) => DbHelper.SaveProduct(product);
        public (bool Success, string Message) DeleteProduct(int productId) => DbHelper.DeleteProduct(productId);
        public string GenerateUniqueBarcode() => DbHelper.GenerateUniqueBarcode();
    }
}
