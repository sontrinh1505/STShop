using System;
using System.Collections.Generic;
using System.Linq;
using TeduShop.Common;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    public interface ISystemConfigService
    {
        SystemConfig Add(SystemConfig SystemConfig);

        void Update(SystemConfig SystemConfig);

        SystemConfig Delete(int id);

        IEnumerable<SystemConfig> GetAll();

        IEnumerable<SystemConfig> GetAll(string keyWord);

        IEnumerable<SystemConfig> Search(string keyWord, int page, int pageSize, string sort, out int totalRow);

        IEnumerable<SystemConfig> GetListSystemConfigByName(string Name);

        SystemConfig GetById(int id);

        void Save();
        

    }

    public class SystemConfigService : ISystemConfigService
    {
        private ISystemConfigRepository _systemConfigRepository;
        private IUnitOfWork _unitOfWork;

        public SystemConfigService(ISystemConfigRepository systemConfigRepository, IUnitOfWork unitOfWork)
        {
            this._systemConfigRepository = systemConfigRepository;
            this._unitOfWork = unitOfWork;
        }

        public SystemConfig Add(SystemConfig SystemConfig)
        {
            var menu = _systemConfigRepository.Add(SystemConfig);
            _unitOfWork.Commit();
          
            return menu;
        }

        public SystemConfig Delete(int id)
        {
            return _systemConfigRepository.Delete(id);
        }

        public IEnumerable<SystemConfig> GetAll()
        {
            return _systemConfigRepository.GetAll();
        }

        public IEnumerable<SystemConfig> GetAll(string keyWord)
        {
            //string[] includes = { "SystemConfigGroup" };
            if (!string.IsNullOrEmpty(keyWord))
                return _systemConfigRepository.GetMulti(x => x.ValueString.Contains(keyWord));
            else
                return _systemConfigRepository.GetAll();
        }

        public SystemConfig GetById(int id)
        {
            return _systemConfigRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(SystemConfig SystemConfig)
        {
            _systemConfigRepository.Update(SystemConfig);
        }

        public IEnumerable<SystemConfig> GetListSystemConfigByName(string Name)
        {
            return _systemConfigRepository.GetMulti(x => x.Code.Contains(Name));
        }

        public IEnumerable<SystemConfig> Search(string keyWord, int page, int pageSize, string sort, out int totalRow)
        {
            var query = _systemConfigRepository.GetMulti(x => x.ValueString.Contains(keyWord));
            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        
    }
}