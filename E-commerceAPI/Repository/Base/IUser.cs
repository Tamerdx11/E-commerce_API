using E_commerceAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_commerceAPI.Repository.Base
{
    public interface IUser
    {
        IEnumerable<User> GetAll();
        Task<User> GetByEmail(string email);
        Task<User> Add(User user);
        void Delete(int userId);
    }
}
