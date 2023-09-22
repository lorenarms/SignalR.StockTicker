using Microsoft.EntityFrameworkCore;
using ShopOnline.API.Data;
using ShopOnline.API.Entities;
using ShopOnline.API.Repositories.Contracts;

namespace ShopOnline.API.Repositories
{
	public class ProductRepository : IProductRepository
	{
        private ShopOnlineDbContext _context { get; }
		public ProductRepository(ShopOnlineDbContext context)
		{
            _context = context;			
		}
		public async Task<IEnumerable<Product>> GetItems()
		{
			// var products = await _context.Products.Include(p => p.CategoryId).ToListAsync();
			var products = await _context.Products.ToListAsync();
			return products;
		}

		public async Task<IEnumerable<ProductCategory>> GetCategories()
		{
			var categories = await _context.ProductCategories.ToListAsync();
			return categories;
		}

		public Task<Product> GetItme(int id)
		{
			throw new NotImplementedException();
		}

		public Task<ProductCategory> GetCategory(int id)
		{
			throw new NotImplementedException();
		}
	}
}
