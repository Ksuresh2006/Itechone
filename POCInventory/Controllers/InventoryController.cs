using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POCInventory.DTO.Request;
using POCInventory.IBusinessRepo;
using POCInventory.Model;

namespace POCInventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IIventoryService iventoryService;
        public InventoryController(IIventoryService _iventoryService)
        {
            this.iventoryService = _iventoryService;
        }

        [HttpPost("AddInventoryDetails")]
        public async Task<IActionResult> addInventoryDetails([FromBody] AddInventoryRequest inventoryRequest)
        {
            string _resStr = string.Empty;
            try
            {
                
                _resStr =await iventoryService.addInventoryDetails(inventoryRequest); 
                if (_resStr == null)
                {
                    return BadRequest("Unable to Process Request");
                }
                if (_resStr == "true")
                { return Ok("Inventory added sucessfully"); }
                return Ok(_resStr);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("GetInventoryDetails")]
        public IActionResult getInventoryDetails()
        {
            try
            {
                List<Inventory> inventories = iventoryService.getInventoryDetails();
                if (inventories == null)
                {
                    return Ok("No Recoed found");
                }
                return Ok(inventories);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("UpdateInventoryDetails")]
        public async Task<IActionResult> updateInventoryDetails(UpdateInventoryRequest updateInventory)
        {
            string _resUpdate = string.Empty;
            try
            {
                if ( updateInventory.ProductId > 0)
                {
                    _resUpdate = await iventoryService.updateInventoryDetails(updateInventory);
                    if (_resUpdate == null)
                    {
                        return BadRequest("No Recoed found");
                    }
                    if (_resUpdate == "true")
                    { return Ok("Inventory updated sucessfully"); }
                    return Ok(_resUpdate);
                }
                else { return BadRequest("Does not exist inventory"); }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("DeleteInventoryDetails/{id}")]
        public async Task<IActionResult> deleteInventoryDetails( long id)
        {
            string _resStr = string.Empty;
            try
            {
                _resStr = await iventoryService.deleteInventoryDetails(id);
                if (_resStr == null)
                {
                    return BadRequest("Unable to Process Request");
                }
                if (_resStr == "true")
                { return Ok("Inventory deleted sucessfully"); }
                return Ok(_resStr);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("ImportInventoryDetails")]
        public async Task<IActionResult> ImportInventoryDetails(IFormFile  _invertoryFile)
        {
            string _resStr = string.Empty;
            try
            {

                _resStr = await iventoryService.importInventoryDetails(_invertoryFile);
                if (_resStr == null)
                {
                    return BadRequest("Unable to Process Request");
                }
                if (_resStr == "true")
                { return Ok("Inventory added sucessfully"); }
                return Ok(_resStr);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
