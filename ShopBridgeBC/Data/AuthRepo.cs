using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopBridgeBC.Model;

namespace ShopBridgeBC.Data
{
    public class AuthRepo : IAuthRepo
    {
        private readonly DataContext _context;
        public AuthRepo(DataContext context)
        {
            _context = context;
        }

        public async Task<UserResponse> Login(UserRequest user)
        {
            var exist = await _context.Users.FirstOrDefaultAsync(x => x.UserName == user.UserName);

            if (!VerifyPasswordHash(user.Password, exist.PasswordHash, exist.PasswordSalt))
            {
                return null;
            }
            else
            {
                UserResponse newUserResponse = new UserResponse
                {
                    UserName = exist.UserName
                };

                return newUserResponse;
            }
        }

        public async Task<UserResponse> Register(UserRequest user)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);
            User newUser = new User
            {
                UserName = user.UserName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            await _context.AddAsync(newUser);
            await _context.SaveChangesAsync();
            UserResponse newUserResponse = new UserResponse
            {
                UserName = user.UserName
            };
            return newUserResponse;
        }

        public async Task<bool> UserExist(UserRequest user)
        {
            var exist = await _context.Users.FirstOrDefaultAsync(x => x.UserName == user.UserName);
            if (exist != null)
            {
                return true;
            }
            else return false;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt);
            var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < computeHash.Length; i++)
            {
                if (computeHash[i] != passwordHash[i])
                {
                    return false;
                }
            }
            return true;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
}