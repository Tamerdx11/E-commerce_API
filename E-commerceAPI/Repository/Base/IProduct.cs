using E_commerceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace E_commerceAPI.Repository.Base
{
    public interface IProduct
    {
        Task<Product> GetById(int id);
        IEnumerable<Product> GetAll();
        Task<List<Product>> GetByNameAndCategory(string name, string category);
        Task<Product> Add(Product product);
        void Update(int id, ProductDto productDto);
        void Delete(int id);


    }
}
