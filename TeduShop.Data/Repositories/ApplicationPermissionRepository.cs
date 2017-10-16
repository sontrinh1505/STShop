using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    public interface IApplicationPermissionRepository : IRepository<ApplicationPermission>
    {

    }
    public class ApplicationPermissionRepository : RepositoryBase<ApplicationPermission>, IApplicationPermissionRepository
    {
        public ApplicationPermissionRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
