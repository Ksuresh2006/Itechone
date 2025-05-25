using POCInventory.DBContext;
using POCInventory.Model;
using Microsoft.EntityFrameworkCore;
using POCInventory.IBusinessRepo;
using POCInventory.DTO.Request;
using System.Transactions;
using OfficeOpenXml;

namespace POCInventory.BusinessRepo
{
    public class IventoryService: IIventoryService
    {
        private readonly InventoryDbContext inventoryDbContext;
        public IventoryService(InventoryDbContext _inventoryDbContext)
        {
            this.inventoryDbContext = _inventoryDbContext;
        }
        public List<Inventory> getInventoryDetails()
        {
            List<Inventory> _inve = new List<Inventory>();


            try
            {
                _inve = inventoryDbContext.inventory.FromSqlRaw("EXEC GetAllProduct").ToList();
            
            }
            catch (Exception ex) { throw ex; }
            return _inve;
        }
        public async Task<string> addInventoryDetails(AddInventoryRequest inventoryRequest)
        {
            string _RequestRetuenVal = "false";
            
                using (var transaction = await inventoryDbContext.Database.BeginTransactionAsync())
                {
                try
                {
                    Inventory inv = new Inventory
                    {
                        ProductName = inventoryRequest.ProductName,
                        Productdescription = inventoryRequest.Productdescription,
                        ProductUOM = inventoryRequest.ProductUOM,
                        OpeningBalance = inventoryRequest.OpeningBalance,
                        Quantity = inventoryRequest.Quantity,                     
                        TaxPer = inventoryRequest.TaxPer,
                        ProductCatagory = inventoryRequest.ProductCatagory,
                        ProductCode = inventoryRequest.ProductCode,
                        HSNNo = inventoryRequest.HSNNo,
                        IsActive = true,
                        UnitPrice = inventoryRequest.UnitPrice,

                    };
                    await inventoryDbContext.Database.ExecuteSqlRawAsync(
                           "EXEC sp_AddInventory @ProductName = {0}, @Productdescription = {1}, @ProductUOM = {2}, @OpeningBalance = {3}, @Quantity = {4}, @TaxPer = {5}, @ProductCatagory = {6}, @ProductCode = {7}, @HSNNo = {8}, @IsActive = {9}, @CreatedBy = {10},@CreatedDate={11},@TaxAmt={12},@UnitPrice={13}",
                    
                inv.ProductName,
                inv.Productdescription,
                inv.ProductUOM,
                inv.OpeningBalance,
                inv.Quantity,
                inv.TaxPer,
                inv.ProductCatagory,
                inv.ProductCode,
                inv.HSNNo,
                inv.IsActive,
                inv.CreatedBy,
                inv.CreatedDate,
                inv.TaxAmt= ((inv.UnitPrice!=0) && (inv.TaxPer!=0)) ? (inv.UnitPrice*(inv.TaxPer/100)):0,
                inv.UnitPrice);
                    await transaction.CommitAsync();
                    _RequestRetuenVal = "true";

                }
                catch (Exception ex) { await transaction.RollbackAsync(); throw ex; }

                
               
                };

            return _RequestRetuenVal;
        }
        public async Task<string> updateInventoryDetails(UpdateInventoryRequest updateInventory)
        {
            string _RequestRetuenVal = "false";
            Inventory inv = inventoryDbContext.inventory.Where(x => x.ProductId == updateInventory.ProductId).FirstOrDefault();

            using (var transaction = await inventoryDbContext.Database.BeginTransactionAsync())
            {
                try
                {

                   if(inv!=null) 


                    {
                        inv.ProductName = updateInventory.ProductName;
                        inv.Productdescription = updateInventory.Productdescription;
                        inv.ProductUOM = updateInventory.ProductUOM;
                        inv.OpeningBalance = updateInventory.OpeningBalance;
                        inv.Quantity = updateInventory.Quantity ?? 0;
                        inv.TaxPer = updateInventory.TaxPer??0;
                        inv.ProductCatagory = updateInventory.ProductCatagory;
                        inv.ProductCode = updateInventory.ProductCode;
                       inv. HSNNo = updateInventory.HSNNo;
                  
                       inv.UnitPrice = updateInventory.UnitPrice;

                    };
                    await inventoryDbContext.Database.ExecuteSqlRawAsync(
                           "EXEC sp_UpdateInventory @ProductName = {0}, @Productdescription = {1}, @ProductUOM = {2}, @OpeningBalance = {3}, @Quantity = {4}, @TaxPer = {5}, @ProductCatagory = {6}, @ProductCode = {7}, @HSNNo = {8},@TaxAmt={9},@UnitPrice={10},@ModifiedBy={11},@ModifiedDate={12},@ProductId={13}",

                inv.ProductName,
                inv.Productdescription,
                inv.ProductUOM,
                inv.OpeningBalance,
                inv.Quantity,
                inv.TaxPer,
                inv.ProductCatagory,
                inv.ProductCode,
                inv.HSNNo,
               
                inv.TaxAmt = ((inv.UnitPrice != 0 && inv.UnitPrice!=null) && (inv.TaxPer != 0)) ? (inv.UnitPrice * (inv.TaxPer / 100)) : 0,
                inv.UnitPrice,
                inv.ModifiedBy="Admin",
                inv.ModifiedDate=DateTime.Now,
                inv.ProductId

                        );
                    await transaction.CommitAsync();
                    _RequestRetuenVal = "true";

                }
                catch (Exception ex) { await transaction.RollbackAsync(); throw ex; }
            };

            return _RequestRetuenVal;
        }
        public async Task<string> deleteInventoryDetails(long id)
        {
            string _RequestRetuenVal = "false";
            Inventory inv = inventoryDbContext.inventory.Where(x => x.ProductId == id).FirstOrDefault();

            try
            {
                if (inv != null)
                {
                    inv.IsActive = false;
                }
                ;
                await inventoryDbContext.Database.ExecuteSqlRawAsync(
                       "EXEC sp_DeleteInventory @IsActive = {0},@ProductId={1}",
                        inv.IsActive,
                       inv.ProductId);

                _RequestRetuenVal = "true";

            }
            catch (Exception ex) { throw ex; }           

            return _RequestRetuenVal;
        }

        
        public async Task<string> importInventoryDetails(IFormFile _invertoryFile)
        {
            string _RequestRetuenVal = "false";

            if (_invertoryFile == null || _invertoryFile.Length <= 0)
            {
                return _RequestRetuenVal;   
            }
            if (!Path.GetExtension(_invertoryFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                return _RequestRetuenVal;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;


            using (var stream = new MemoryStream())
            {
                await _invertoryFile.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {

                    ExcelWorksheet worksheet = package.Workbook.Worksheets.First();
                    int rowCount = worksheet.Dimension.Rows;

                    var inventoryList = new List<Inventory>();
                    for (int row = 2; row <= rowCount; row++)
                    {
                        var unitPrice = double.TryParse(worksheet.Cells[row, 10].Text, out double up) ? up : 0;
                        var taxPer = double.TryParse(worksheet.Cells[row, 6].Text, out double tp) ? tp : 0;

                        inventoryList.Add(new Inventory
                        {
                            ProductName = worksheet.Cells[row, 1].Text,
                            Productdescription = worksheet.Cells[row, 2].Text,
                            ProductUOM = worksheet.Cells[row, 3].Text,
                            OpeningBalance = double.TryParse(worksheet.Cells[row, 4].Text, out double ob) ? ob : 0,
                            Quantity = double.TryParse(worksheet.Cells[row, 5].Text, out double q) ? q : 0,
                            TaxPer = taxPer,
                            ProductCatagory = worksheet.Cells[row, 7].Text,
                            ProductCode = worksheet.Cells[row, 8].Text,
                            HSNNo = worksheet.Cells[row, 9].Text,
                            UnitPrice = unitPrice,
                            TaxAmt = (unitPrice * (taxPer / 100)),
                            IsActive = true,
                          
                      
                        });
                    }

                    await using var transaction = await inventoryDbContext.Database.BeginTransactionAsync();
                    try
                    {
                        await inventoryDbContext.inventory.AddRangeAsync(inventoryList);
                        await inventoryDbContext.SaveChangesAsync();
                        await transaction.CommitAsync();
                        _RequestRetuenVal = "true";
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        throw; 
                    }
                }
            }            

            return _RequestRetuenVal;
        }

    }
}
