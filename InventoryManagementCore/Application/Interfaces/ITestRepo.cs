using InventoryManagementCore.Domain.Entities;

namespace InventoryManagementCore.Application.Interfaces
{
    public interface ITestRepo
    {
        Task<string> CreateTestUserAsync(Test_UserInfo userInfo);
        Task<Test_UserInfo> GetTestUserAsync(string userId);
        Task UpdateTestUserAsync(string userId, Test_UserInfo updatedUserInfo);
        Task DeleteTestUserAsync(string userId);
        Task<List<Test_UserInfo>> GetTestUsersAsync(string role);
        Task<Test_UserInfo> LoginUserAsync(string usernameOrEmail, string password);
        // Item operations
        Task<string> CreateTestItemAsync(Test_ItemInfo itemInfo);
        Task<Test_ItemInfo> GetTestItemAsync(string itemId);
        Task UpdateTestItemAsync(string itemId, Test_ItemInfo updatedItemInfo);
        Task DeleteTestItemAsync(string itemId);
        Task<List<Test_ItemInfo>> GetTestItemsAsync(string? key);

        //Products
        Task<string> CreateTestStockAsync(Test_Stocks stock);
        Task<Test_Stocks> GetTestStockByIdAsync(string id);
        Task<List<Test_Stocks>> ListTestStocksAsync(string itemName, string category, DateTime? purchaseDateFrom, DateTime? purchaseDateTo);
        Task<bool> UpdateTestStockAsync(string id, Test_Stocks updatedStock);
        Task<bool> DeleteTestStockAsync(string id);

        //TestCategory
        Task<string> CreateTestCategoryAsync(TestCategory category);
        Task<List<TestCategory>> ListCategoryAsync();
        //TestSupplier
        Task<string> CreateTestSupplierAsync(TestSupplier supplier);
        Task<TestSupplier> GetTestSupplierByIdAsync(string id);
        Task<List<TestSupplier>> ListTestSuppliersAsync(string? key);
        Task<bool> UpdateTestSupplierAsync(string id, TestSupplier updatedSupplier);
        Task<bool> DeleteTestSupplierAsync(string id);
        Task<string> CreateTestCanteenAsync(TestCanteen canteen);
        Task<List<TestCanteen>> ListCanteensAsync();
        Task<string> CreateTestOrderAsync(Test_Orders order);
        Task<Test_Orders> GetTestOrderByIdAsync(string id);
        Task<List<Test_Orders>> ListTestOrdersAsync(string canteenName, string status, string shopkeeper, DateTime? dateFrom, DateTime? dateTo);
        Task<bool> UpdateTestOrderAsync(string id, Test_Orders updatedOrder);
        Task<bool> DeleteTestOrderAsync(string id);
        Task<bool> RemoveItemsFromOrderAsync(string id, List<string> itemIdsToRemove);
        Task<bool> AddNewItemsToOrderAsync(string id, List<OrderedItem> items);
        Task<bool> UpdateTestOrderStatusAsync(string id, string status);
        Task<byte[]> TestQR(string head, string data, string footer);
        Task<byte[]> OrderToPdfAsync(string id);
        Task<Test_ItemInfo> GetTestItemWithNameAsync(string itemName);
        Task<bool> CancelTestOrderAsync(string id);
    }
}
