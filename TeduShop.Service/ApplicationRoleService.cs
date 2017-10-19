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
    public interface IApplicationRoleService
    {
        ApplicationRole GetDetail(string id);

        IEnumerable<ApplicationRole> GetAll(int page, int pageSize, out int totalRow, string filter);

        IEnumerable<ApplicationRole> GetAll();

        ApplicationRole Add(ApplicationRole appRole);

        void Update(ApplicationRole AppRole);

        void Delete(string id);

        //Add roles to a sepcify group
        //bool AddRolesToGroup(IEnumerable<ApplicationRolePermission> roleGroups, int groupId);

        //Get list roles by userId and permissonName
        IEnumerable<ApplicationRole> GetListRoleByUserId(string userId, string permissionName);

        void Save();
    }

    public class ApplicationRoleService : IApplicationRoleService
    {
        private IApplicationRoleRepository _appRoleRepository;
        private IApplicationRoleGroupRepository _appRoleGroupRepository;
        private IUnitOfWork _unitOfWork;
        private IApplicationGroupService _applicationGroupService;

        public ApplicationRoleService(IUnitOfWork unitOfWork,
            IApplicationRoleRepository appRoleRepository, 
            IApplicationRoleGroupRepository appRoleGroupRepository,
            IApplicationGroupService applicationGroupService
            )
        {
            this._appRoleRepository = appRoleRepository;
            this._appRoleGroupRepository = appRoleGroupRepository;
            this._unitOfWork = unitOfWork;
            this._applicationGroupService = applicationGroupService;
        }

        public ApplicationRole Add(ApplicationRole appRole)
        {
            if (_appRoleRepository.CheckContains(x => x.Description == appRole.Description))
                throw new NameDuplicatedException("Tên không được trùng");
            return _appRoleRepository.Add(appRole);
        }

        //public bool AddRolesToGroup(IEnumerable<ApplicationRolePermission> roleGroups, int groupId)
        //{
        //    _appRoleGroupRepository.DeleteMulti(x => x.GroupId == groupId);
        //    foreach (var roleGroup in roleGroups)
        //    {
        //        _appRoleGroupRepository.Add(roleGroup);
        //    }
        //    return true;
        //}

        public void Delete(string id)
        {
            _appRoleRepository.DeleteMulti(x => x.Id == id);
        }

        public IEnumerable<ApplicationRole> GetAll()
        {
            return _appRoleRepository.GetAll();
        }

        public IEnumerable<ApplicationRole> GetAll(int page, int pageSize, out int totalRow, string filter = null)
        {
            var query = _appRoleRepository.GetAll();
            if (!string.IsNullOrEmpty(filter))
                query = query.Where(x => x.Description.Contains(filter));

            totalRow = query.Count();
            return query.OrderBy(x => x.Description).Skip(page * pageSize).Take(pageSize);
        }

        public ApplicationRole GetDetail(string id)
        {
            return _appRoleRepository.GetSingleByCondition(x => x.Id == id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ApplicationRole AppRole)
        {
            if (_appRoleRepository.CheckContains(x => x.Description == AppRole.Description && x.Id != AppRole.Id))
                throw new NameDuplicatedException("Tên không được trùng");
            _appRoleRepository.Update(AppRole);
        }

        public  IEnumerable<ApplicationRole> GetListRoleByUserId(string userId, string permissionName)
        {
            var roles = _appRoleRepository.GetListRoleByUserId(userId, permissionName).ToList();
            return roles;
        }

    }
}
