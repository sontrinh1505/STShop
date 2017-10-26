using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    public interface IModelBrandRepository : IRepository<ModelBrand>
    {
    }

    public class ModelBrandRepository : RepositoryBase<ModelBrand>, IModelBrandRepository
    {
        public ModelBrandRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}