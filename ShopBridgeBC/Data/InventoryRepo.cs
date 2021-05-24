using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopBridgeBC.Model;

namespace ShopBridgeBC.Data
{
    public class InventoryRepo : IInventoryRepo
    {
        readonly DataContext _dbcontext;
        public InventoryRepo(DataContext context)
        {
            _dbcontext = context;
        }
        public async Task<Inventory> AddItems(Inventory inventoryItem)
        {
            var exist = await _dbcontext.InventoryItems.FirstOrDefaultAsync(x => x.ItemName == inventoryItem.ItemName);
            if (exist != null)
            {
                return null;
            }
            else
            {
                await _dbcontext.AddAsync(inventoryItem);
                await _dbcontext.SaveChangesAsync();
                return inventoryItem;
            }
        }

        public async Task<bool> DeleteItem(int id)
        {
            var exist = await _dbcontext.InventoryItems.FirstOrDefaultAsync(x => x.Id == id);
            if (exist != null)
            {
                _dbcontext.Remove(exist);
                _dbcontext.SaveChanges();
                return true;
            }
            else return false;
        }

        public List<Inventory> GetItems()
        {
            var Items = _dbcontext.InventoryItems.ToList();
            return Items;
        }

        public async Task<Inventory> UpdateItem(Inventory inventoryItem)
        {
            var exist = await _dbcontext.InventoryItems.FirstOrDefaultAsync(x => x.ItemName == inventoryItem.ItemName);
            if (exist != null)
            {
                _dbcontext.Update<Inventory>(inventoryItem);
                _dbcontext.SaveChanges();
                return exist;
            }
            else return null;
        }
    }
}
