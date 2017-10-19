using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    public interface IApplicationRoleRepository : IRepository<ApplicationRole>
    {
        IEnumerable<ApplicationRole> GetListRoleByUserId(string userId, string permissonName);
    }
    public class ApplicationRoleRepository : RepositoryBase<ApplicationRole>, IApplicationRoleRepository
    {
        public ApplicationRoleRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public IEnumerable<ApplicationRole> GetListRoleByUserId(string userId, string permissonName)
        {
            //var query = from g in DbContext.ApplicationRoles
            //            join ug in DbContext.ApplicationRoleGroups
            //            on g.Id equals ug.RoleId
            //            where ug.GroupId == groupId
            //            select g;
            //return query;
            var permissonId = DbContext.ApplicationPermissions.Where(x => x.Name == permissonName).Select(x => x.ID).FirstOrDefault();
            var groupId = DbContext.ApplicationUserGroups.Where(x => x.UserId == userId).Select(x => x.ApplicationGroup.ID).Distinct().ToList();
            var roles = DbContext.ApplicationRolePermissions.Where(x => groupId.Contains(x.GroupId) && x.PermissonId == permissonId).Select(x => x.ApplicationRole).ToList();

            return roles;


        }
    }
}
