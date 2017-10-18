using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    public interface IApplicationRolePermissionRepository : IRepository<ApplicationRolePermission>
    {

    }
    public class ApplicationRolePermissionRepository : RepositoryBase<ApplicationRolePermission>, IApplicationRolePermissionRepository
    {
        public ApplicationRolePermissionRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
