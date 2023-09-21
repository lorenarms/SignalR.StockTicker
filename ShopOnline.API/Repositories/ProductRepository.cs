using ShopOnline.API.Entities;
using ShopOnline.API.Repositories.Contracts;

namespace ShopOnline.API.Repositories
{
	public class ProductRepository : IProductRepository
	{
		public Task<IEnumerable<Product>> GetItems()
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<ProductCategory>> GetCategories()
		{
			throw new NotImplementedException();
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
