using System.Threading.Tasks;

namespace ShopBridgeBC.Data
{
    public interface IAuthRepo
    {
        public Task<UserResponse> Register(UserRequest user);

        public Task<UserResponse> Login(UserRequest user);

        public Task<bool> UserExist(UserRequest user);
    }
}