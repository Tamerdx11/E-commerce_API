using E_commerceAPI.Data;
using E_commerceAPI.Models;
using E_commerceAPI.Repository.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace E_commerceAPI.Repository
{
    public class ProductRepository : IProduct
    {
        private  ProductContext _productContext;
        public ProductRepository(ProductContext productContext)
        {
            _productContext = productContext;
        }
        
        public IEnumerable<Product> GetAll()
        {
            return _productContext.Set<Product>().ToList();
        }

        public async Task<Product> GetById(int id)
        {
            return await _productContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> Add(Product product)
        {
            await _productContext.AddAsync(product);
            await _productContext.SaveChangesAsync();
            return product;
        }

        public void Update(int id, ProductDto productDto)
        {
            _productContext.Products.Where(p => p.Id == id).ExecuteUpdate(
                p => p.SetProperty(p => p.Name, productDto.Name)
            .SetProperty(p => p.Category, productDto.Category)
            .SetProperty(p => p.ImageUrl, productDto.ImageUrl)
            .SetProperty(p => p.Description, productDto.Description)
            .SetProperty(p => p.Price, productDto.Price)
            );
        }
    
        public void Delete(int id)
        {
            _productContext.Products!.Where(p => p.Id == id).ExecuteDelete();
            _productContext.SaveChanges();
        }

        public async Task<List<Product>> GetByNameAndCategory(string name, string category)
        {
            var products = from p in _productContext.Products select p;

            if(!category.IsNullOrEmpty())
                products = products.Where(p => p.Category.Contains(category));

            if (!name.IsNullOrEmpty())
                products = products.Where(p => p.Name.Contains(name));

            return await products.ToListAsync();
        }
    }
}
