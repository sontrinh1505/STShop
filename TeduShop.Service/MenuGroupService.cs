using System;
using System.Collections.Generic;
using System.Linq;
using TeduShop.Common;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    public interface IMenuGroupService
    {
        MenuGroup Add(MenuGroup Menu);

        void Update(MenuGroup Menu);

        MenuGroup Delete(int id);

        //IEnumerable<MenuGroup> GetAll(string[] include = null);

        IEnumerable<MenuGroup> GetAll(string keyWord = null);

        IEnumerable<MenuGroup> Search(string keyWord);

        MenuGroup GetById(int id);

        void Save();
        

    }

    public class MenuGroupService : IMenuGroupService
    {
        private IMenuRepository _menuRepository;
        private IMenuGroupRepository _menuGroupRepository;
        private IUnitOfWork _unitOfWork;

        public MenuGroupService(IMenuRepository menuRepository, IUnitOfWork unitOfWork,
            IMenuGroupRepository menuGroupRepository

            )
        {
            this._menuGroupRepository = menuGroupRepository;
            this._menuRepository = menuRepository;
            this._unitOfWork = unitOfWork;
        }

        public MenuGroup Add(MenuGroup Menu)
        {
            var menu = _menuGroupRepository.Add(Menu);
            _unitOfWork.Commit();
          
            return menu;
        }

        public MenuGroup Delete(int id)
        {
            return _menuGroupRepository.Delete(id);
        }

        public IEnumerable<MenuGroup> GetAll(string keyWord = null)
        {
            string[] include = { "MenuGroups", "Menus", "ChildrenGroupMenus" };
            var results = _menuGroupRepository.GetAll(include);

            if (!string.IsNullOrEmpty(keyWord))
            {
                results = results.Where(x => x.Name.ToLower().Contains(keyWord.ToLower()));
            }           
           
            //return _menuGroupRepository.GetAll(includes);
            return results;
        }

        //public IEnumerable<MenuGroup> GetAll(string keyWord)
        //{
        //    if (!string.IsNullOrEmpty(keyWord))
        //        return _menuGroupRepository.GetMulti(x => x.Name.Contains(keyWord));
        //    else
        //        return _menuGroupRepository.GetAll();
        //}

        public MenuGroup GetById(int id)
        {
            return _menuGroupRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(MenuGroup Menu)
        {
            _menuGroupRepository.Update(Menu);
        }

        public IEnumerable<string> GetListMenuByName(string Name)
        {
            return _menuRepository.GetMulti(x => x.Status && x.Name.Contains(Name)).Select(y => y.Name);
        }

        public IEnumerable<MenuGroup> Search(string keyWord)
        {
            var query = _menuGroupRepository.GetMulti(x => x.Name.Contains(keyWord));        
            return query;
        }
    }
}