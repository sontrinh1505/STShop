using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;
using System.Data.Entity;

namespace TeduShop.Data.Repositories
{
    public interface IApplicationGroupRepository : IRepository<ApplicationGroup>
    {
        IEnumerable<ApplicationGroup> GetListGroupByUserId(string userId);
        IEnumerable<ApplicationUser> GetListUserByGroupId(int groupId);
        IEnumerable<ApplicationPermission> GetListPermissionByGroupId(int groupId);
        IEnumerable<ApplicationRole> GetListRoleByPermissionId(int permissionId, int groupId);

    }
    public class ApplicationGroupRepository : RepositoryBase<ApplicationGroup>, IApplicationGroupRepository
    {
        public ApplicationGroupRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public IEnumerable<ApplicationGroup> GetListGroupByUserId(string userId)
        {
            var query = from g in DbContext.ApplicationGroups
                        join ug in DbContext.ApplicationUserGroups
                        on g.ID equals ug.GroupId
                        where ug.UserId == userId
                        select g;
            return query;
        }

        public IEnumerable<ApplicationUser> GetListUserByGroupId(int groupId)
        {
            var query = from g in DbContext.ApplicationGroups
                        join ug in DbContext.ApplicationUserGroups
                        on g.ID equals ug.GroupId
                        join u in DbContext.Users
                        on ug.UserId equals u.Id
                        where ug.GroupId == groupId
                        select u;
            return query;
        }

        public IEnumerable<ApplicationPermission> GetListPermissionByGroupId(int groupId)
        {
            var query = from g in DbContext.ApplicationGroups
                        join pg in DbContext.ApplicationPermissionGroups
                        on g.ID equals pg.GroupId
                        join u in DbContext.ApplicationPermissions
                        on pg.PermissionId equals u.ID
                        where pg.GroupId == groupId
                        select u;
            //var query = DbContext.Set<ApplicationPermission>().Include("Roles").ToList();
            return query;
        }

        public IEnumerable<ApplicationRole> GetListRoleByPermissionId(int permissionId, int groupId)
        {
            var query = from p in DbContext.ApplicationPermissions
                        join rp in DbContext.ApplicationRolePermissions
                        on p.ID equals rp.PermissonId
                        join r in DbContext.ApplicationRoles
                        on rp.RoleId equals r.Id
                        where rp.PermissonId == permissionId && rp.GroupId == groupId
                        select r;
            return query;
        }
    }
}
