using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    public interface IApplicationPermissionGroupRepository : IRepository<ApplicationPermissionGroup>
    {

    }
    public class ApplicationPermissionGroupRepository : RepositoryBase<ApplicationPermissionGroup>, IApplicationPermissionGroupRepository
    {
        public ApplicationPermissionGroupRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
