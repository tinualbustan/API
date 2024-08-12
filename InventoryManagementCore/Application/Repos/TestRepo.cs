using InventoryManagementCore.Application.DTOs;
using InventoryManagementCore.Application.Helpers;
using InventoryManagementCore.Application.Interfaces;
using InventoryManagementCore.Domain.Entities;
using InventoryManagementCore.Infrastructure.Services;
using MongoDB.Bson;
using MongoDB.Driver;

namespace InventoryManagementCore.Application.Repos
{
    public class TestRepo : ITestRepo
    {
        private readonly IDbContext dbContext;
        private readonly IDocManager documentManager;

        public TestRepo(IDbContext dbContext, IDocManager documentManager)
        {
            this.dbContext = dbContext;
            this.documentManager = documentManager;
        }

        public async Task<string> CreateTestUserAsync(Test_UserInfo userInfo)
        {
            IMongoCollection<Test_UserInfo> col = dbContext.GetCollection<Test_UserInfo>();
            userInfo.Id = Guid.NewGuid().ToString();
            await col.InsertOneAsync(userInfo);
            return userInfo.Id;
        }

        public async Task<Test_UserInfo> GetTestUserAsync(string userId)
        {
            IMongoCollection<Test_UserInfo> col = dbContext.GetCollection<Test_UserInfo>();
            var x = await col.Find(u => u.Id == userId).FirstOrDefaultAsync();
            x.Password = null;
            return x;
        }
        public async Task<List<Test_UserInfo>> GetTestUsersAsync(string role)
        {
            IMongoCollection<Test_UserInfo> col = dbContext.GetCollection<Test_UserInfo>();
            FilterDefinition<Test_UserInfo> filter = Builders<Test_UserInfo>.Filter.Empty;
            if (!string.IsNullOrEmpty(role))
            {
                filter = Builders<Test_UserInfo>.Filter.AnyEq(u => u.Roles, role);
            }
            return await col.Find(filter).SortBy(x => x.FullName).ToListAsync();
        }

        public async Task UpdateTestUserAsync(string userId, Test_UserInfo updatedUserInfo)
        {
            IMongoCollection<Test_UserInfo> col = dbContext.GetCollection<Test_UserInfo>();
            await col.ReplaceOneAsync(u => u.Id == userId, updatedUserInfo);
        }

        public async Task DeleteTestUserAsync(string userId)
        {
            IMongoCollection<Test_UserInfo> col = dbContext.GetCollection<Test_UserInfo>();
            await col.DeleteOneAsync(u => u.Id == userId);
        }
        public async Task<Test_UserInfo> LoginUserAsync(string usernameOrEmail, string password)
        {
            IMongoCollection<Test_UserInfo> col = dbContext.GetCollection<Test_UserInfo>();
            var filter = Builders<Test_UserInfo>.Filter.Where(u => u.UserName == usernameOrEmail || u.Email == usernameOrEmail);
            var user = await col.Find(filter).FirstOrDefaultAsync();

            if (user != null && user.Password == password)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        //-----------------------------ITEM---------------------------------------------------------------------
        public async Task<string> CreateTestItemAsync(Test_ItemInfo itemInfo)
        {
            IMongoCollection<Test_ItemInfo> col = dbContext.GetCollection<Test_ItemInfo>();
            itemInfo.Id = Guid.NewGuid().ToString();
            await col.InsertOneAsync(itemInfo);
            return itemInfo.Id;
        }

        public async Task<Test_ItemInfo> GetTestItemAsync(string itemId)
        {
            IMongoCollection<Test_ItemInfo> col = dbContext.GetCollection<Test_ItemInfo>();
            return await col.Find(i => i.Id == itemId || i.ItemCode == itemId || i.ItemName == itemId).FirstOrDefaultAsync();
        }
        public async Task<Test_ItemInfo> GetTestItemWithNameAsync(string itemName)
        {
            IMongoCollection<Test_ItemInfo> col = dbContext.GetCollection<Test_ItemInfo>();
            return await col.Find(i => i.ItemName == itemName || i.ItemCode == itemName).FirstOrDefaultAsync();
        }
        public async Task<List<Test_ItemInfo>> GetTestItemsAsync(string? key)
        {
            IMongoCollection<Test_ItemInfo> col = dbContext.GetCollection<Test_ItemInfo>();
            return string.IsNullOrWhiteSpace(key) ? await col.Find(_ => true).ToListAsync() :
                await col.Find(x => x.ItemName.ToLower().Contains(key)).SortBy(x => x.ItemName).ToListAsync();
        }

        public async Task UpdateTestItemAsync(string itemId, Test_ItemInfo updatedItemInfo)
        {
            IMongoCollection<Test_ItemInfo> col = dbContext.GetCollection<Test_ItemInfo>();
            await col.ReplaceOneAsync(i => i.Id == itemId, updatedItemInfo);
        }

        public async Task DeleteTestItemAsync(string itemId)
        {
            IMongoCollection<Test_ItemInfo> col = dbContext.GetCollection<Test_ItemInfo>();
            await col.DeleteOneAsync(i => i.Id == itemId);
        }


        //----------------------------------------Stocks------------------------------------------
        public async Task<string> CreateTestStockAsync(Test_Stocks stock)
        {
            IMongoCollection<Test_Stocks> col = dbContext.GetCollection<Test_Stocks>();
            stock.Id = Guid.NewGuid().ToString();
            stock.Status = "New";
            stock.Active = true;
            IMongoCollection<Test_ItemInfo> itemCol = dbContext.GetCollection<Test_ItemInfo>();
            var item = await itemCol.Find(x => x.ItemName == stock.ItemName).FirstOrDefaultAsync();
            if (item == null)
                throw new Exception("Item Not Found");
            stock.ActualUnit = stock.Unit;
            stock.ActualQuantity = stock.Quantity;


            stock.ActualQuantity = ((stock.Quantity * (stock.ConvertionRate == 0 ? 1 : stock.ConvertionRate)) + (stock.AlternativeQuantity ?? 0)) / (item.ConversionRate == 0 ? 1 : item.ConversionRate);
            stock.ActualQuantity = stock.ActualQuantity.HasValue ? Math.Round(stock.ActualQuantity.Value, 2) : null;


            item.ItemCount += stock.ActualQuantity;
            stock.LastUpdatedAt = DateTime.UtcNow;

            await itemCol.ReplaceOneAsync(i => i.Id == item.Id, item);
            await col.InsertOneAsync(stock);

            return stock.Id;
        }

        public async Task<Test_Stocks> GetTestStockByIdAsync(string id)
        {
            IMongoCollection<Test_Stocks> col = dbContext.GetCollection<Test_Stocks>();
            return await col.Find(stock => stock.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Test_Stocks>> ListTestStocksAsync(string itemName, string category, DateTime? purchaseDateFrom, DateTime? purchaseDateTo)
        {
            IMongoCollection<Test_Stocks> col = dbContext.GetCollection<Test_Stocks>();

            var filterBuilder = Builders<Test_Stocks>.Filter;
            var filter = filterBuilder.Empty;

            if (!string.IsNullOrEmpty(itemName))
            {
                filter &= filterBuilder.Eq(s => s.ItemName, itemName);
            }

            if (!string.IsNullOrEmpty(category))
            {
                filter &= filterBuilder.Eq(s => s.Catagory, category);
            }

            if (purchaseDateFrom.HasValue)
            {
                filter &= filterBuilder.Gte(s => s.PurchaseDate, purchaseDateFrom.Value);
            }

            if (purchaseDateTo.HasValue)
            {
                filter &= filterBuilder.Lte(s => s.PurchaseDate, purchaseDateTo.Value);
            }

            var sort = Builders<Test_Stocks>.Sort.Descending(s => s.LastUpdatedAt);

            return await col.Find(filter).Sort(sort).ToListAsync();
        }

        public async Task<bool> UpdateTestStockAsync(string id, Test_Stocks stock)
        {
            IMongoCollection<Test_Stocks> col = dbContext.GetCollection<Test_Stocks>();
            var result = await col.ReplaceOneAsync(stock => stock.Id == id, stock);


            IMongoCollection<Test_ItemInfo> itemCol = dbContext.GetCollection<Test_ItemInfo>();
            var productInfo = await itemCol.Find(x => x.ItemName == stock.ItemName).FirstOrDefaultAsync();
            if (productInfo == null)
                throw new Exception("Item Not Found");

            var ActualQuantity = ((stock.Quantity * (stock.ConvertionRate == 0 ? 1 : stock.ConvertionRate)) + (stock.AlternativeQuantity ?? 0)) / (productInfo.ConversionRate == 0 ? 1 : productInfo.ConversionRate);
            ActualQuantity = stock.ActualQuantity.HasValue ? Math.Round(stock.ActualQuantity.Value, 2) : null;

            var changes = stock.ActualQuantity - ActualQuantity;
            stock.ActualQuantity = ActualQuantity;

            productInfo.ItemCount += changes;
            await itemCol.ReplaceOneAsync(x => x.Id == productInfo.Id, productInfo);

            stock.LastUpdatedAt = DateTime.UtcNow;
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteTestStockAsync(string id)
        {
            IMongoCollection<Test_Stocks> col = dbContext.GetCollection<Test_Stocks>();
            IMongoCollection<Test_ItemInfo> itemCol = dbContext.GetCollection<Test_ItemInfo>();
            var stock = await col.Find(x => x.Id == id).FirstOrDefaultAsync();
            var item = await itemCol.Find(x => x.ItemName == stock.ItemName).FirstOrDefaultAsync();
            item.ItemCount = (item.ItemCount - stock.ActualQuantity);//<= 0 ? 0 : (item.ItemCount - stock.ActualQuantity);
            var result = await col.DeleteOneAsync(stock => stock.Id == id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
        //----------------------------------------CATEGORY-------------------------------------------
        public async Task<List<TestCategory>> ListCategoryAsync()
        {
            IMongoCollection<TestCategory> col = dbContext.GetCollection<TestCategory>();
            return await col.Find(_ => true).ToListAsync();
        }
        public async Task<string> CreateTestCategoryAsync(TestCategory category)
        {
            IMongoCollection<TestCategory> col = dbContext.GetCollection<TestCategory>();
            category.Id = Guid.NewGuid().ToString();
            await col.InsertOneAsync(category);
            return category.Id;
        }
        //----------------------------------------CATEGORY--------------------------------------------
        public async Task<string> CreateTestSupplierAsync(TestSupplier supplier)
        {
            IMongoCollection<TestSupplier> col = dbContext.GetCollection<TestSupplier>();
            supplier.Id = Guid.NewGuid().ToString();
            await col.InsertOneAsync(supplier);
            return supplier.Id;
        }

        public async Task<TestSupplier> GetTestSupplierByIdAsync(string id)
        {
            IMongoCollection<TestSupplier> col = dbContext.GetCollection<TestSupplier>();
            return await col.Find(supplier => supplier.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<TestSupplier>> ListTestSuppliersAsync(string? key)
        {
            IMongoCollection<TestSupplier> col = dbContext.GetCollection<TestSupplier>();
            return string.IsNullOrWhiteSpace(key) ? await col.Find(_ => true).ToListAsync() :
                await col.Find(x => x.Name.ToLower().Contains(key)).SortBy(x => x.Name).ToListAsync();
        }

        public async Task<bool> UpdateTestSupplierAsync(string id, TestSupplier updatedSupplier)
        {
            IMongoCollection<TestSupplier> col = dbContext.GetCollection<TestSupplier>();
            var result = await col.ReplaceOneAsync(supplier => supplier.Id == id, updatedSupplier);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteTestSupplierAsync(string id)
        {
            IMongoCollection<TestSupplier> col = dbContext.GetCollection<TestSupplier>();
            var result = await col.DeleteOneAsync(supplier => supplier.Id == id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
        //-----------------------------Canteen---------------------------------------
        public async Task<string> CreateTestCanteenAsync(TestCanteen canteen)
        {
            IMongoCollection<TestCanteen> col = dbContext.GetCollection<TestCanteen>();
            canteen.Id = Guid.NewGuid().ToString();
            await col.InsertOneAsync(canteen);
            return canteen.Id;
        }

        public async Task<List<TestCanteen>> ListCanteensAsync()
        {
            IMongoCollection<TestCanteen> col = dbContext.GetCollection<TestCanteen>();
            return await col.Find(_ => true).SortBy(x => x.Name).ToListAsync();
        }
        //-------------------------Order=--------------------------------------------------
        public async Task<string> CreateTestOrderAsync(Test_Orders order)
        {
            IMongoCollection<Test_Orders> col = dbContext.GetCollection<Test_Orders>();
            var itemCol = dbContext.GetCollection<Test_ItemInfo>();
            order.Id = Guid.NewGuid().ToString();
            order.Date = DateTime.UtcNow;
            order.OrderCode = GenerateUniqueCode();
            foreach (var orderdItem in order.ProductsData)
            {
                var item = await itemCol.Find(x => x.ItemName == orderdItem.ItemName).FirstOrDefaultAsync();
                orderdItem.ActualItemCount = ((orderdItem.ItemCount
                                               * (item.ConversionRate == 0 ? 1 : item.ConversionRate)) + (orderdItem.AltItemCount ?? 0)) / (item.ConversionRate == 0 ? 1 : item.ConversionRate);

                orderdItem.ActualItemCount = orderdItem.ActualItemCount.HasValue ? Math.Round(orderdItem.ActualItemCount.Value, 2) : null;
                item.ItemCount = item.ItemCount - orderdItem.ActualItemCount;
                await itemCol.ReplaceOneAsync(i => i.Id == item.Id, item);
            }
            order.LastUpdatedAt = DateTime.UtcNow;
            await col.InsertOneAsync(order);
            return order.Id;
        }

        public async Task<Test_Orders> GetTestOrderByIdAsync(string id)
        {
            IMongoCollection<Test_Orders> col = dbContext.GetCollection<Test_Orders>();
            return await col.Find(order => order.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Test_Orders>> ListTestOrdersAsync(string canteenName, string status, string shopkeeper, DateTime? dateFrom, DateTime? dateTo)
        {
            IMongoCollection<Test_Orders> col = dbContext.GetCollection<Test_Orders>();

            var filterBuilder = Builders<Test_Orders>.Filter;
            var filter = filterBuilder.Empty;

            if (!string.IsNullOrEmpty(canteenName))
            {
                var regexCanteenName = new BsonRegularExpression($".*{canteenName}.*", "i");
                filter &= filterBuilder.Regex(o => o.CanteenName, regexCanteenName);
            }

            if (!string.IsNullOrEmpty(status))
            {
                var regexStatus = new BsonRegularExpression($".*{status}.*", "i");
                filter &= filterBuilder.Regex(o => o.Status, regexStatus);
            }

            if (!string.IsNullOrEmpty(shopkeeper))
            {
                var regexShopkeeper = new BsonRegularExpression($".*{shopkeeper}.*", "i");
                filter &= filterBuilder.Regex(o => o.Shopkeeper, regexShopkeeper);
            }

            if (dateFrom.HasValue)
            {
                filter &= filterBuilder.Gte(o => o.Date, dateFrom.Value);
            }

            if (dateTo.HasValue)
            {
                filter &= filterBuilder.Lte(o => o.Date, dateTo.Value);
            }


            var sort = Builders<Test_Orders>.Sort.Descending(o => o.Date);

            return await col.Find(filter).Sort(sort).ToListAsync();
        }

        public async Task<bool> UpdateTestOrderAsync(string id, Test_Orders updatedOrder)
        {
            IMongoCollection<Test_Orders> col = dbContext.GetCollection<Test_Orders>();
            IMongoCollection<Test_ItemInfo> productCol = dbContext.GetCollection<Test_ItemInfo>();
            var oldOder = await col.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (oldOder == null) return false;
            foreach (var newProduct in updatedOrder.ProductsData)
            {
                var oldItem = oldOder.ProductsData.FirstOrDefault(x => x.ItemId == newProduct.ItemId);
                if (oldItem == null)
                {

                    var product = await productCol.Find(x => x.Id == newProduct.ItemId).FirstOrDefaultAsync();
                    if (product == null) return false;
                    newProduct.ActualItemCount = ((newProduct.ItemCount * (product.ConversionRate == 0 ? 1 : product.ConversionRate)) + (newProduct.AltItemCount ?? 0)) / (product.ConversionRate == 0 ? 1 : product.ConversionRate);
                    newProduct.ActualItemCount = newProduct.ActualItemCount.HasValue ? Math.Round(newProduct.ActualItemCount.Value, 2) : null;
                    product.ItemCount -= newProduct.ActualItemCount;
                    await productCol.ReplaceOneAsync(x => x.Id == product.Id, product);
                }
                else
                {
                    var productInfo = await productCol.Find(x => x.Id == newProduct.ItemId).FirstOrDefaultAsync();
                    if (productInfo == null) throw new Exception("Product Info not found");
                    newProduct.ActualItemCount = ((newProduct.ItemCount * (productInfo.ConversionRate == 0 ? 1 : productInfo.ConversionRate)) + (newProduct.AltItemCount ?? 0)) / (productInfo.ConversionRate == 0 ? 1 : productInfo.ConversionRate);
                    newProduct.ActualItemCount = newProduct.ActualItemCount.HasValue ? Math.Round(newProduct.ActualItemCount.Value, 2) : null;
                    double? changes = oldItem.ActualItemCount - newProduct.ActualItemCount;
                    productInfo.ItemCount = productInfo.ItemCount + changes;
                    await productCol.ReplaceOneAsync(x => x.Id == productInfo.Id, productInfo);
                }
            }
            foreach (var product in oldOder.ProductsData)
            {
                if (!updatedOrder.ProductsData.Any(x => x.ItemId == product.ItemId))
                {
                    var productInfo = await productCol.Find(x => x.Id == product.ItemId).FirstOrDefaultAsync();
                    productInfo.ItemCount = productInfo.ItemCount + product.ActualItemCount;
                    await productCol.ReplaceOneAsync(x => x.Id == productInfo.Id, productInfo);
                }
            }
            updatedOrder.LastUpdatedAt = DateTime.UtcNow;
            var result = await col.ReplaceOneAsync(order => order.Id == id, updatedOrder);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
        public async Task<bool> UpdateTestOrderStatusAsync(string id, string status)
        {
            IMongoCollection<Test_Orders> col = dbContext.GetCollection<Test_Orders>();
            IMongoCollection<Test_ItemInfo> productCol = dbContext.GetCollection<Test_ItemInfo>();
            var order = await col.Find(order => order.Id == id).FirstOrDefaultAsync();
            if (order == null) { return false; }
            order.Status = status;
            if (order.Status == "Cancelled")
            {
                foreach (var item in order.ProductsData)
                {
                    var productInfo = await productCol.Find(x => x.Id == item.ItemId).FirstOrDefaultAsync();
                    if (productInfo == null) return false;
                    productInfo.ItemCount = productInfo.ItemCount + item.ActualItemCount;
                    await productCol.ReplaceOneAsync(x => x.Id == productInfo.Id, productInfo);
                }
                var cancelResult = await col.ReplaceOneAsync(order => order.Id == id, order);
                return true;
            }
            var result = await col.ReplaceOneAsync(order => order.Id == id, order);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
        public async Task<bool> AddNewItemsToOrderAsync(string id, List<OrderedItem> items)
        {
            IMongoCollection<Test_Orders> col = dbContext.GetCollection<Test_Orders>();
            var oldOrder = await col.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (oldOrder != null)
            {
                if (oldOrder.ProductsData == null || oldOrder.ProductsData.Count == 0)
                {
                    oldOrder.ProductsData = items;
                }
                else
                {
                    var existingItems = oldOrder.ProductsData.ToDictionary(item => item.ItemId);

                    foreach (var newItem in items)
                    {
                        if (existingItems.TryGetValue(newItem.ItemId, out var existingItem))
                        {
                            existingItem.ItemCount += newItem.ItemCount;
                            existingItem.ExpireDate = newItem.ExpireDate;
                        }
                        else
                        {
                            oldOrder.ProductsData.Add(newItem);
                        }
                    }
                }

                var result = await col.ReplaceOneAsync(order => order.Id == id, oldOrder);
                return result.IsAcknowledged && result.ModifiedCount > 0;
            }
            return false;
        }
        public async Task<byte[]> TestQR(string head, string data, string footer)
        {
            QRManager qrManager = new QRManager();
            qrManager.LoadQRInfo(head, footer, data);
            qrManager.GenerateQRCode();
            byte[] x = qrManager.GetQRCodeByteArray();
            return x;
        }
        public async Task<bool> RemoveItemsFromOrderAsync(string id, List<string> itemIdsToRemove)
        {
            IMongoCollection<Test_Orders> col = dbContext.GetCollection<Test_Orders>();
            IMongoCollection<Test_ItemInfo> productCol = dbContext.GetCollection<Test_ItemInfo>();

            var oldOrder = await col.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (oldOrder != null && oldOrder.ProductsData != null)
            {
                var itemsToRemove = oldOrder.ProductsData.Where(item => itemIdsToRemove.Contains(item.ItemId)).ToList();

                foreach (var item in itemsToRemove)
                {
                    var product = await productCol.Find(x => x.Id == item.ItemId).FirstOrDefaultAsync();
                    if (product == null)
                    {
                        return false;
                    }

                    product.ItemCount += item.ActualItemCount;
                    await productCol.ReplaceOneAsync(x => x.Id == product.Id, product);
                    oldOrder.ProductsData.Remove(item);
                }

                await col.ReplaceOneAsync(x => x.Id == id, oldOrder);
                return true;
            }
            return false;
        }
        //public async Task<bool> EditItemFromOrderAsync(string orderId, OrderedItem orderdItem)
        //{
        //    IMongoCollection<Test_Orders> col = dbContext.GetMongoDbCollection<Test_Orders>();
        //    IMongoCollection<Test_ItemInfo> productCol = dbContext.GetMongoDbCollection<Test_ItemInfo>();
        //    var oldOrder = await col.Find(x => x.Id == orderId).FirstOrDefaultAsync() ?? throw new Exception("Order not found");
        //    var oldItem = oldOrder.ProductsData.First(x=>x.ItemId== orderdItem.ItemId) ?? throw new Exception("Product not found");
        //    var productInfo = await productCol.Find(x => x.Id == orderdItem.ItemId).FirstOrDefaultAsync();
        //    if (productInfo == null) throw new Exception("Produc Info not found");
        //    orderdItem.ActualItemCount = ((orderdItem.ItemCount * (productInfo.ConversionRate == 0 ? 1 : productInfo.ConversionRate)) + orderdItem.AltItemCount) / (productInfo.ConversionRate == 0 ? 1 : productInfo.ConversionRate);
        //    double? changes = oldItem.ActualItemCount - orderdItem.AltItemCount;
        //    productInfo.ItemCount = productInfo.ItemCount + changes;
        //}

        public async Task<bool> DeleteTestOrderAsync(string id)
        {
            IMongoCollection<Test_Orders> col = dbContext.GetCollection<Test_Orders>();
            IMongoCollection<Test_ItemInfo> productCol = dbContext.GetCollection<Test_ItemInfo>();
            var order = await col.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (order == null) { return false; }
            foreach (var item in order.ProductsData)
            {
                var productInfo = await productCol.Find(x => x.Id == item.ItemId).FirstOrDefaultAsync();
                if (productInfo == null) return false;
                productInfo.ItemCount = productInfo.ItemCount + item.ActualItemCount;
                await productCol.ReplaceOneAsync(x => x.Id == productInfo.Id, productInfo);
            }
            var result = await col.DeleteOneAsync(order => order.Id == id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public async Task<bool> CancelTestOrderAsync(string id)
        {
            IMongoCollection<Test_Orders> col = dbContext.GetCollection<Test_Orders>();
            IMongoCollection<Test_ItemInfo> productCol = dbContext.GetCollection<Test_ItemInfo>();
            var order = await col.Find(x => x.Id == id).FirstOrDefaultAsync();
            order.Status = "Cancelled";
            if (order == null) { return false; }
            foreach (var item in order.ProductsData)
            {
                var productInfo = await productCol.Find(x => x.Id == item.ItemId).FirstOrDefaultAsync();
                if (productInfo == null) return false;
                productInfo.ItemCount = productInfo.ItemCount + item.ActualItemCount;
                await productCol.ReplaceOneAsync(x => x.Id == productInfo.Id, productInfo);
            }
            var result = await col.ReplaceOneAsync(order => order.Id == id, order);
            return true;
        }


        static string GenerateUniqueCode()
        {
            string prefix = "AL";
            string year = DateTime.Now.ToString("yy");

            string dateTimeString = DateTime.Now.ToString("MMddHHmmss");

            string obfuscatedNumber = ObfuscateDateTimeString(dateTimeString);

            return $"{prefix}-{year}-{obfuscatedNumber}";
        }

        static string ObfuscateDateTimeString(string dateTimeString)
        {
            int[] map = { 9, 7, 5, 6, 1, 8, 3, 4, 2, 0 };

            char[] obfuscatedChars = new char[dateTimeString.Length];

            for (int i = 0; i < dateTimeString.Length; i++)
            {
                int digit = int.Parse(dateTimeString[i].ToString());
                obfuscatedChars[i] = map[digit].ToString()[0];
            }

            return new string(obfuscatedChars);
        }

        public async Task<byte[]> OrderToPdfAsync(string id)
        {
            var collection = dbContext.GetCollection<Test_Orders>();
            var productCollection = dbContext.GetCollection<Test_ItemInfo>();
            var order = await collection.Find(x => x.Id == id).FirstOrDefaultAsync();

            var createDocumentDTO = new CreateDocumentDTO
            {
                TemplateName = "TestTemplate"
            };

            var list = new List<StringData>
            {
                new StringData
                {
                    BookMark = "ReferanceNo",
                    Data = order.OrderCode
                },
                new StringData
                {
                    BookMark = "Party",
                    Data = order.CanteenName
                },
                new StringData
                {
                    BookMark = "DATE",
                    Data = order.Date?.ToString("dd/MM/yyyy")
                }
            };
            var newList = new List<List<CellData>>();
            foreach (var product in order.ProductsData)
            {
                var tempRow = new List<CellData>();
                var productinfo = await productCollection.Find(x => x.Id == product.ItemId).FirstOrDefaultAsync();
                var tempname = new CellData { Header = "Description Of Goods", Data = productinfo.ItemName };
                var altDisplay = string.IsNullOrWhiteSpace(productinfo.AlternativeUnit) ? "" : $"({product.ActualItemCount * productinfo.ConversionRate} {productinfo.AlternativeUnit})";
                var tempItems = new CellData { Header = "Quantity", Data = $"{product.ActualItemCount}{productinfo.Unit} {altDisplay}" };
                tempRow.Add(tempname);
                tempRow.Add(tempItems);
                newList.Add(tempRow);
            }
            for (int i = 0; i <= 4; i++)
            {
                var tempRow = new List<CellData>();
                var tempname = new CellData { Header = "Description Of Goods", Data = "" };
                var tempItems = new CellData { Header = "Quantity", Data = "" };
                tempRow.Add(tempname);
                tempRow.Add(tempItems);
                newList.Add(tempRow);
            }
            var docTable = new DocTable
            {
                TableBookMark = "table",
                Headers = null,
                Rows = newList
            };
            createDocumentDTO.DocTables = new List<DocTable> { docTable };

            createDocumentDTO.DocData = list;

            byte[] pdfBytes = documentManager.CreateDocument(createDocumentDTO);
            return pdfBytes;
        }
    }
}
