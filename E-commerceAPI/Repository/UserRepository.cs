using E_commerceAPI.Data;
using E_commerceAPI.Models;
using E_commerceAPI.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace E_commerceAPI.Repository
{
    public class UserRepository : IUser
    {
        private readonly UserContext _userContext;

        public UserRepository(UserContext userContext)
        {
            _userContext = userContext;
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _userContext.Users!.FirstOrDefaultAsync(x => x.Email == email)!;
        }

        public async Task<User> Add(User user)
        {
            await _userContext.AddAsync(user);
            await _userContext.SaveChangesAsync();
            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return _userContext.Set<User>().ToList();
        }

        public void Delete(int userId)
        {
            _userContext.Users.Where(u => u.Id == userId).ExecuteDelete();
            _userContext.SaveChanges();
        }
    }
}
