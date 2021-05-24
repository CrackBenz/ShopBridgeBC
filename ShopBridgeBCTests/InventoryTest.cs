using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using ShopBridgeBC.Data;
using ShopBridgeBC.Model;
using Shouldly;
using Xunit;

namespace ShopBridgeTest
{
    public class InventoryTest
    {
        [Fact]
        public void GetItems_should_return_allInventoryItems()
        {
            //Arrange
            var _repo = new Mock<IInventoryRepo>();
            _repo.Setup(x => x.GetItems())
                .Returns(new List<Inventory>() {
                new Inventory
                {
                    Id = 1001,
                    ItemName = "Phone",
                    Description="Nokia",
                    Price = 5000
                }

        });
            //Act
            var result = _repo.Object.GetItems();
            //Assert
            result.Count.ShouldBe(1);
        }

        [Fact]
        public async Task AddItems_should_add_newItem()
        {
            var _repo = new Mock<IInventoryRepo>();
            _repo.Setup(x => x.AddItems(It.IsAny<Inventory>()))
                .ReturnsAsync(new Inventory
                {
                    Id = 1001,
                    ItemName = "Phone",
                    Description = "Nokia",
                    Price = 5000
                });

            Inventory Item = new Inventory
            {
                Id = 1001,
                ItemName = "Phone",
                Description = "Nokia",
                Price = 5000
            };

            var result = await _repo.Object.AddItems(Item);

            result.ShouldNotBeNull();
        }

        [Fact]
        public async Task UpdateItem_should_update_existingItem()
        {
            var _repo = new Mock<IInventoryRepo>();
            _repo.Setup(x => x.UpdateItem(It.IsAny<Inventory>()))
                .ReturnsAsync(new Inventory
                {
                    Id = 1001,
                    ItemName = "Phone",
                    Description = "Nokia",
                    Price = 5000
                });

            Inventory Item = new Inventory
            {
                Id = 1001,
                ItemName = "Phone",
                Description = "Nokia",
                Price = 5000
            };

            var result = await _repo.Object.UpdateItem(Item);

            result.ShouldNotBeNull();
        }

        [Fact]
        public async Task DeleteItem_should_delete_existingItem()
        {
            var _repo = new Mock<IInventoryRepo>();
            _repo.Setup(x => x.DeleteItem(It.IsAny<int>()))
                .ReturnsAsync(true);

            Inventory Item = new Inventory
            {
                Id = 1001,
                ItemName = "Phone",
                Description = "Nokia",
                Price = 5000
            };

            var result = await _repo.Object.DeleteItem(1001);

            result.ShouldBeTrue();
        }
    }
}
