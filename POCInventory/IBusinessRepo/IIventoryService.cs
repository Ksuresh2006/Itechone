using POCInventory.DTO.Request;
using POCInventory.Model;

namespace POCInventory.IBusinessRepo
{
    public interface IIventoryService
    {
        public List<Inventory> getInventoryDetails();
        Task<string> addInventoryDetails(AddInventoryRequest inventoryRequest);
        Task<string> updateInventoryDetails(UpdateInventoryRequest updateInventory);
        Task<string> deleteInventoryDetails(long id);
        Task<string> importInventoryDetails(IFormFile _invertoryFile);


    }
}
