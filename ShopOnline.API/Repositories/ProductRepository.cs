using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
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

		public async Task<Product> GetItem(int id)
		{
			var product = await _context.Products.FindAsync(id);
			return product;
		}

		public async Task<ProductCategory> GetCategory(int id)
		{
			var category = await _context.ProductCategories.SingleOrDefaultAsync(c => c.Id == id);
			return category;
			
		}
	}
}
