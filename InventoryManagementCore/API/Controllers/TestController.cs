using InventoryManagementCore.Application.Helpers;
using InventoryManagementCore.Application.Interfaces;
using InventoryManagementCore.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace InventoryManagementCore.API.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class TestController : ControllerBase
    {
        private readonly ITestRepo testRepo;

        public TestController(ITestRepo testRepo)
        {
            this.testRepo = testRepo;
        }
        [HttpPost("createuser")]
        public async Task<IActionResult> CreateUserAsync(Test_UserInfo userInfo)
        {
            var userId = await testRepo.CreateTestUserAsync(userInfo);


            return Ok(userId);
        }

        // Get user by ID
        [HttpGet("getuser")]
        public async Task<IActionResult> GetUserAsync(string userId)
        {
            var user = await testRepo.GetTestUserAsync(userId);
            if (user == null)
                return NotFound();


            return Ok(user);
        }
        [HttpGet("getalluser")]
        public async Task<IActionResult> GeAlltUserAsync(string role)
        {
            var user = await testRepo.GetTestUsersAsync(role);
            if (user == null)
                return NotFound();


            return Ok(user);
        }


        [HttpPut("updateuser")]
        public async Task<IActionResult> UpdateUserAsync(Test_UserInfo updatedUserInfo)
        {
            await testRepo.UpdateTestUserAsync(updatedUserInfo.Id, updatedUserInfo);

            return Ok();
        }


        [HttpDelete("deleteuser")]
        public async Task<IActionResult> DeleteUserAsync(string userId)
        {
            await testRepo.DeleteTestUserAsync(userId);

            return Ok();
        }


        [HttpPost("login")]
        public async Task<IActionResult> LoginUserAsync(LoginRequest request)
        {
            var user = await testRepo.LoginUserAsync(request.UsernameOrEmail, request.Password);
            if (user == null)
                return BadRequest("Invalid username/email or password");

            return Ok(user);
        }


        [HttpPost("createitem")]
        public async Task<IActionResult> CreateItemAsync(Test_ItemInfo itemInfo)
        {
            var itemId = await testRepo.CreateTestItemAsync(itemInfo);

            return Ok(itemId);
        }


        [HttpGet("getitem")]
        public async Task<IActionResult> GetItemAsync(string itemId)
        {
            var item = await testRepo.GetTestItemAsync(itemId);
            if (item == null)
                return NotFound();

            return Ok(item);
        }
        [HttpGet("getallitems")]
        public async Task<IActionResult> GeAllItemsAsync(string key)
        {
            var user = await testRepo.GetTestItemsAsync(key);
            if (user == null)
                return NotFound();

            return Ok(user);
        }


        [HttpPut("updateitem")]
        public async Task<IActionResult> UpdateItemAsync(Test_ItemInfo updatedItemInfo)
        {
            await testRepo.UpdateTestItemAsync(updatedItemInfo.Id, updatedItemInfo);

            return Ok();
        }

        [HttpDelete("deleteitem")]
        public async Task<IActionResult> DeleteItemAsync(string itemId)
        {
            await testRepo.DeleteTestItemAsync(itemId);
            return Ok();
        }
        //*************----Stocks--------***************//
        [HttpPost("createstock")]
        public async Task<IActionResult> CreateStockAsync(Test_Stocks stock)
        {
            var stockId = await testRepo.CreateTestStockAsync(stock);
            return Ok(stockId);
        }

        [HttpGet("getstockbyid")]
        public async Task<IActionResult> GetStockByIdAsync(string id)
        {
            var stock = await testRepo.GetTestStockByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock);
        }


        [HttpGet("liststocks")]
        public async Task<IActionResult> ListTestStocksAsync(string itemName, string category, DateTime? purchaseDateFrom, DateTime? purchaseDateTo)
        {
            var stocks = await testRepo.ListTestStocksAsync(itemName, category, purchaseDateFrom, purchaseDateTo);

            return Ok(stocks);
        }


        [HttpPut("updatestock")]
        public async Task<IActionResult> UpdateStockAsync(string id, Test_Stocks updatedStock)
        {
            var success = await testRepo.UpdateTestStockAsync(id, updatedStock);
            if (!success)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete("deletestock")]
        public async Task<IActionResult> DeleteStockAsync(string id)
        {
            var success = await testRepo.DeleteTestStockAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return Ok();
        }

        //CATA---------------------------------------

        [HttpPost("createcategory")]
        public async Task<IActionResult> CreateCategoryAsync(TestCategory category)
        {
            var categoryId = await testRepo.CreateTestCategoryAsync(category);

            return Ok(categoryId);
        }

        [HttpGet("listcategories")]
        public async Task<IActionResult> ListCategoriesAsync()
        {
            var categories = await testRepo.ListCategoryAsync();

            return Ok(categories);
        }
        //------------------------------Supplier

        [HttpPost("createsupplier")]
        public async Task<IActionResult> CreateSupplierAsync(TestSupplier supplier)
        {
            var supplierId = await testRepo.CreateTestSupplierAsync(supplier);

            return Ok(supplierId);
        }

        [HttpGet("getsupplier")]
        public async Task<IActionResult> GetSupplierByIdAsync(string id)
        {
            var supplier = await testRepo.GetTestSupplierByIdAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return Ok(supplier);
        }

        [HttpGet("listsuppliers")]
        public async Task<IActionResult> ListSuppliersAsync(string? key)
        {
            var suppliers = await testRepo.ListTestSuppliersAsync(key);

            return Ok(suppliers);
        }

        [HttpPut("updatesupplier")]
        public async Task<IActionResult> UpdateSupplierAsync(string id, TestSupplier updatedSupplier)
        {
            var updated = await testRepo.UpdateTestSupplierAsync(id, updatedSupplier);
            if (!updated)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete("deletesupplier")]
        public async Task<IActionResult> DeleteSupplierAsync(string id)
        {
            var deleted = await testRepo.DeleteTestSupplierAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return Ok();
        }
        //-----------------------------Canteen
        [HttpPost("createcanteen")]
        public async Task<IActionResult> CreateCanteenAsync(TestCanteen canteen)
        {
            var canteenId = await testRepo.CreateTestCanteenAsync(canteen);
            return Ok(canteenId);
        }

        [HttpGet("listcanteens")]
        public async Task<IActionResult> ListCanteensAsync()
        {
            var canteens = await testRepo.ListCanteensAsync();

            return Ok(canteens);
        }
        //--------------------Order
        [HttpPost("createorder")]
        public async Task<IActionResult> CreateOrderAsync(Test_Orders order)
        {
            var orderId = await testRepo.CreateTestOrderAsync(order);

            return Ok(orderId);
        }

        // Read - Get by Id
        [HttpGet("getorderbyid")]
        public async Task<IActionResult> GetOrderByIdAsync(string id)
        {
            var order = await testRepo.GetTestOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // Read - List all
        [HttpGet("listorders")]
        public async Task<IActionResult> ListTestOrdersAsync(string canteenName, string status, string shopkeeper, DateTime? dateFrom, DateTime? dateTo)
        {
            var orders = await testRepo.ListTestOrdersAsync(canteenName, status, shopkeeper, dateFrom, dateTo);

            return Ok(orders);
        }

        // Update
        [HttpPut("updateorder")]
        public async Task<IActionResult> UpdateOrderAsync(string id, Test_Orders updatedOrder)
        {
            var success = await testRepo.UpdateTestOrderAsync(id, updatedOrder);
            if (!success)
            {
                return NotFound();
            }
            return Ok();
        }
        [HttpGet("updateorderstatus")]
        public async Task<IActionResult> UpdateOrderAsync(string id, string newStatus)
        {
            var success = await testRepo.UpdateTestOrderStatusAsync(id, newStatus);
            if (!success)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpGet("cancelorder")]
        public async Task<IActionResult> UpdateOrderAsync(string id)
        {
            var success = await testRepo.CancelTestOrderAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return Ok();
        }
        [HttpPut("additemstoorder")]
        public async Task<IActionResult> AddItemsToOrderAsync(string id, List<OrderedItem> items)
        {
            var success = await testRepo.AddNewItemsToOrderAsync(id, items);
            if (!success)
            {
                return NotFound();
            }
            return Ok();
        }
        [HttpGet("ordertopdf")]
        public async Task<IActionResult> OrderToPdfAsync(string id)
        {
            byte[] docxBytes = await testRepo.OrderToPdfAsync(id);
            string contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            string fileName = "OrderInfo.pdf";
            byte[] pdfBytes = await DocumentConverter.ConvertDocxToPdfAsync(docxBytes);
            return File(pdfBytes, contentType, fileName);
        }

        [HttpPut("removeitemsfromorder")]
        public async Task<IActionResult> RemoveItemsFromOrderAsync(string id, List<string> itemIdsToRemove)
        {
            var success = await testRepo.RemoveItemsFromOrderAsync(id, itemIdsToRemove);
            if (!success)
            {
                return NotFound();
            }
            return Ok();
        }

        //[HttpPut("edititeminorder")]
        //public async Task<IActionResult> EditItemFromOrderAsync(string orderId, OrderedItem orderedItem)
        //{
        //    var success = await testRepo.RemoveItemsFromOrderAsync(id, itemIdsToRemove);
        //    if (!success)
        //    {
        //        return NotFound();
        //    }
        //    return Ok();
        //}

        // Delete
        [HttpDelete("deleteorder")]
        public async Task<IActionResult> DeleteOrderAsync(string id)
        {
            var success = await testRepo.DeleteTestOrderAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return Ok();
        }
        [HttpPost("createqr")]
        public async Task<IActionResult> CreateQRAsync([FromForm] CreateQRvm qrVm)
        {
            Dictionary<string, object> data = new();
            QRManager qRManager = new();
            byte[] qrBytes;
            data.Add("ItemName", qrVm.ItemName);
            var item = await testRepo.GetTestItemWithNameAsync(qrVm.ItemName);
            if (item == null) return NotFound("Item Not Found");

            data.Add("Supplier", qrVm.Supplier ?? "");
            data.Add("BatchId", qrVm.BatchId ?? "");
            data.Add("Unit", qrVm.Unit ?? item.Unit ?? "");
            data.Add("RatePerUnit", qrVm.RatePerUnit ?? item.Rate);
            data.Add("AltUnit", qrVm.AltUnit ?? item.AlternativeUnit);
            data.Add("RatePerAltUnit", qrVm.RatePerAltUnit ?? item.Rate);
            data.Add("ConversionRate", qrVm.ConversionRate ?? item.ConversionRate);
            switch (qrVm.QrFor)
            {
                case QRfor.StockWithSetCount:
                    data.Add("Quantity", qrVm.Quantity);
                    data.Add("AltQuantity", qrVm.AltQuantity);
                    break;
                case QRfor.StockWithoutSetCount:
                    break;
                default:
                    return BadRequest();
            }
            qRManager.LoadQRInfo(qrVm.Heading, footer: qrVm.Footer, JsonConvert.SerializeObject(data));
            qRManager.GenerateQRCode();
            qrBytes = qRManager.GetQRCodeByteArray();
            return File(qrBytes, "application/octet-stream", qrVm.Heading + "qr.jpg");
        }

    }
    public class CreateQRvm
    {
        public QRfor QrFor { get; set; } = QRfor.StockWithoutSetCount;
        public string ItemName { get; set; }
        public string BatchId { get; set; }
        public string Supplier { get; set; }

        public double? Quantity { get; set; }
        public string? Unit { get; set; }
        public double? RatePerUnit { get; set; }

        public double? AltQuantity { get; set; }
        public string? AltUnit { get; set; }
        public double? RatePerAltUnit { get; set; }
        public double? ConversionRate { get; set; }

        public string Heading { get; set; }
        public string Footer { get; set; }
    }
    [System.Text.Json.Serialization.JsonConverter(typeof(JsonStringEnumConverter))]
    public enum QRfor
    {
        StockWithSetCount,
        StockWithoutSetCount
    }
    public class LoginRequest
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
