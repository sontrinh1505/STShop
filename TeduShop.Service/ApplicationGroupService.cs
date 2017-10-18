using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Common.Exceptions;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;


namespace TeduShop.Service
{
    public interface IApplicationGroupService
    {
        ApplicationGroup GetDetail(int id);

        ApplicationGroup GetDetail(string name);

        IEnumerable<ApplicationGroup> GetAll(int page, int pageSize, out int totalRow, string filter);

        IEnumerable<ApplicationGroup> GetAll();

        IEnumerable<ApplicationPermission> GetAllPermissions();

        ApplicationGroup Add(ApplicationGroup appGroup);

        void Update(ApplicationGroup appGroup);

        ApplicationGroup Delete(int id);

        bool AddUserToGroups(IEnumerable<ApplicationUserGroup> groups, string userId);

        bool AddPermissionsToGroup(IEnumerable<ApplicationPermissionGroup> permisions, int groupId);

        bool AddRolesToPermission(IEnumerable<ApplicationRolePermission> rolePermissions);

        IEnumerable<ApplicationGroup> GetListGroupByUserId(string userId);

        IEnumerable<ApplicationUser> GetListUserByGroupId(int groupId);

        IEnumerable<ApplicationPermission> GetListPermissionByGroupId(int groupId);

        IEnumerable<ApplicationRole> GetListRoleByPermissionId(int permissionId, int groupId);

        void Save();
    }
    public class ApplicationGroupService : IApplicationGroupService
    {
        private IApplicationGroupRepository _appGroupRepository;
        private IApplicationPermissionRepository _applicationPermissionRepository;
        private IUnitOfWork _unitOfWork;
        private IApplicationUserGroupRepository _appUserGroupRepository;
        private IApplicationPermissionGroupRepository _applicationPermissionGroupRepository;
        private IApplicationRolePermissionRepository _applicationRolePermissionRepository;

        public ApplicationGroupService(IUnitOfWork unitOfWork,
            IApplicationUserGroupRepository appUserGroupRepository,
            IApplicationGroupRepository appGroupRepository,
            IApplicationPermissionGroupRepository applicationPermissionGroupRepository,
            IApplicationPermissionRepository applicationPermissionRepository,
            IApplicationRolePermissionRepository applicationRolePermissionRepository
            )
        {
            this._appGroupRepository = appGroupRepository;
            this._appUserGroupRepository = appUserGroupRepository;
            this._unitOfWork = unitOfWork;
            this._applicationPermissionGroupRepository = applicationPermissionGroupRepository;
            this._applicationPermissionRepository = applicationPermissionRepository;
            this._applicationRolePermissionRepository = applicationRolePermissionRepository;
        }

        public ApplicationGroup Add(ApplicationGroup appGroup)
        {
            if (_appGroupRepository.CheckContains(x => x.Name == appGroup.Name))
                throw new NameDuplicatedException("The Name already exist");
            return _appGroupRepository.Add(appGroup);
        }

        public ApplicationGroup Delete(int id)
        {
            var appGroup = this._appGroupRepository.GetSingleById(id);
            return _appGroupRepository.Delete(appGroup);
        }

        public IEnumerable<ApplicationGroup> GetAll()
        {
            return _appGroupRepository.GetAll();
        }

        public IEnumerable<ApplicationGroup> GetAll(int page, int pageSize, out int totalRow, string filter = null)
        {
            var query = _appGroupRepository.GetAll();
            if (!string.IsNullOrEmpty(filter))
                query = query.Where(x => x.Name.Contains(filter));

            totalRow = query.Count();
            return query.OrderBy(x => x.Name).Skip(page * pageSize).Take(pageSize);
        }

        public ApplicationGroup GetDetail(int id)
        {
            return _appGroupRepository.GetSingleById(id);
        }

        public ApplicationGroup GetDetail(string name)
        {
            return _appGroupRepository.GetSingleByCondition(x => x.Name == name);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ApplicationGroup appGroup)
        {
            if (_appGroupRepository.CheckContains(x => x.Name == appGroup.Name && x.ID != appGroup.ID))
                throw new NameDuplicatedException("Tên không được trùng");
            _appGroupRepository.Update(appGroup);
        }

        public bool AddUserToGroups(IEnumerable<ApplicationUserGroup> userGroups, string userId)
        {
            _appUserGroupRepository.DeleteMulti(x => x.UserId == userId);
            foreach (var userGroup in userGroups)
            {
                _appUserGroupRepository.Add(userGroup);
            }
            return true;
        }

        public bool AddPermissionsToGroup(IEnumerable<ApplicationPermissionGroup> permissionGroups, int groupId)
        {
            _applicationPermissionGroupRepository.DeleteMulti(x => x.GroupId == groupId);
            foreach (var permissionGroup in permissionGroups)
            {
                _applicationPermissionGroupRepository.Add(permissionGroup);
            }
            return true;
        }

        public bool AddRolesToPermission(IEnumerable<ApplicationRolePermission> rolePermissions)
        {
           // _applicationRolePermissionRepository.DeleteMulti(x => x.PermissonId == permissionId);
            foreach (var rolePermission in rolePermissions)
            {
                _applicationRolePermissionRepository.Add(rolePermission);
            }
            return true;
        }
        

        public IEnumerable<ApplicationGroup> GetListGroupByUserId(string userId)
        {
            return _appGroupRepository.GetListGroupByUserId(userId);
        }

        public IEnumerable<ApplicationUser> GetListUserByGroupId(int groupId)
        {
            return _appGroupRepository.GetListUserByGroupId(groupId);
        }

        public IEnumerable<ApplicationPermission> GetAllPermissions()
        {
            return _applicationPermissionRepository.GetAll();
        }

        public IEnumerable<ApplicationPermission> GetListPermissionByGroupId(int groupId)
        {
            var listPermission = _appGroupRepository.GetListPermissionByGroupId(groupId).ToList();
            foreach(var permission in listPermission)
            {
                permission.Roles = GetListRoleByPermissionId(permission.ID, groupId);
            }
            return listPermission;
        }

        public IEnumerable<ApplicationRole> GetListRoleByPermissionId(int permissionId, int groupId)
        {
            return _appGroupRepository.GetListRoleByPermissionId(permissionId, groupId).ToList();
            

        }
    }
}
