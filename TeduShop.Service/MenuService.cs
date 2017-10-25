using System;
using System.Collections.Generic;
using System.Linq;
using TeduShop.Common;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    public interface IMenuService
    {
        Menu Add(Menu Menu);

        void Update(Menu Menu);

        Menu Delete(int id);

        IEnumerable<Menu> GetAll();

        IEnumerable<Menu> GetAll(string keyWord);

        IEnumerable<Menu> GetListMenuByMenuGroupId(int groupId);

        IEnumerable<Menu> Search(string keyWord, int page, int pageSize, string sort, out int totalRow);

        Menu GetById(int id);

        void Save();
        

    }

    public class MenuService : IMenuService
    {
        private IMenuRepository _menuRepository;
        private IUnitOfWork _unitOfWork;

        public MenuService(IMenuRepository menuRepository, IUnitOfWork unitOfWork)
        {
            this._menuRepository = menuRepository;
            this._unitOfWork = unitOfWork;
        }

        public Menu Add(Menu Menu)
        {
            var menu = _menuRepository.Add(Menu);
            _unitOfWork.Commit();
          
            return menu;
        }

        public Menu Delete(int id)
        {
            return _menuRepository.Delete(id);
        }

        public IEnumerable<Menu> GetAll()
        {
            return _menuRepository.GetAll();
        }

        public IEnumerable<Menu> GetAll(string keyWord)
        {
            string[] includes = { "MenuGroup" };
            if (!string.IsNullOrEmpty(keyWord))
                return _menuRepository.GetMulti(x => x.Name.Contains(keyWord), includes);
            else
                return _menuRepository.GetAll(includes);
        }

        public Menu GetById(int id)
        {
            return _menuRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Menu Menu)
        {
            _menuRepository.Update(Menu);
        }

        public IEnumerable<string> GetListMenuByName(string Name)
        {
            return _menuRepository.GetMulti(x => x.Status && x.Name.Contains(Name)).Select(y => y.Name);
        }

        public IEnumerable<Menu> Search(string keyWord, int page, int pageSize, string sort, out int totalRow)
        {
            var query = _menuRepository.GetMulti(x => x.Status == true && x.Name.Contains(keyWord));
            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<Menu> GetListMenuByMenuGroupId(int groupId)
        {
            var query = _menuRepository.GetMulti(x => x.Status == true && x.GroupID == groupId);
            return query;
        }
    }
}