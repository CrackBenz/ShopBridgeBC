using System.Threading.Tasks;
using Moq;
using ShopBridgeBC.Data;
using Shouldly;
using Xunit;

namespace ShopBridgeTest
{
    public class UserUnitTest
    {
        [Fact]
        public async Task should_register_user()
        {
            var _repo = new Mock<IAuthRepo>();
            _repo.Setup(x => x.Register(It.IsAny<UserRequest>()))
                .ReturnsAsync(new UserResponse
                {
                    UserName = "user1",

                });

            UserRequest user = new UserRequest
            {
                UserName = "user1",
                Password = "pwd"
            };

            var result = await _repo.Object.Register(user);

            result.ShouldNotBeNull();
        }

        [Fact]
        public async Task should_login_user()
        {
            var _repo = new Mock<IAuthRepo>();
            _repo.Setup(x => x.Login(It.IsAny<UserRequest>()))
                .ReturnsAsync(new UserResponse
                {
                    UserName = "user1",

                });

            UserRequest user = new UserRequest
            {
                UserName = "user1",
                Password = "pwd"
            };

            var result = await _repo.Object.Login(user);

            result.ShouldNotBeNull();
        }
    }
}
