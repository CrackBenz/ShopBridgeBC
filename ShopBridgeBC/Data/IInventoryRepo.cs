using System.Collections.Generic;
using System.Threading.Tasks;
using ShopBridgeBC.Model;

namespace ShopBridgeBC.Data
{
    public interface IInventoryRepo
    {
        public Task<Inventory> AddItems(Inventory inventoryItem);
        public List<Inventory> GetItems();
        public Task<Inventory> UpdateItem(Inventory inventoryItem);
        public Task<bool> DeleteItem(int id);
    }
}
