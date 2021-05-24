using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopBridgeBC.Data;
using ShopBridgeBC.Model;

namespace ShopBridgeBC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryController : ControllerBase
    {       

        private IInventoryRepo _repo;

        public InventoryController(IInventoryRepo repo)
        {            
            _repo = repo;
        }

        /// <summary>
        /// list items in inventory
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<Inventory>>> GetItems()
        {
            try
            {
                List<Inventory> items = _repo.GetItems();
                return items;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Add Items to Inventory
        /// </summary>
        /// <param name="inventoryItem"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Inventory>> AddItems(Inventory inventoryItem)
        {
            try
            {
                await _repo.AddItems(inventoryItem);

                return inventoryItem;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);                
            }
        }
        /// <summary>
        ///  Update item details in inventory
        /// </summary>
        /// <param name="inventoryItem"></param>
        /// <returns></returns>

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<Inventory>> UpdateItemDetails(Inventory inventoryItem)
        {
            try
            {
                await _repo.UpdateItem(inventoryItem);

                return inventoryItem;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE item from Inventory

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Inventory>> DeleteItem(int id)
        {
            try
            {
                var deleted = await _repo.DeleteItem(id);


                if (!deleted)
                {
                    return NotFound($"Item with Id = {id} not found");
                }
                return Ok("Deleted");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}
