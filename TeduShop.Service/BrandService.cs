using System;
using System.Collections.Generic;
using System.Linq;
using TeduShop.Common;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    public interface IBrandService
    {
        Brand Add(Brand Brand);

        void Update(Brand Brand);

        Brand Delete(int id);

        IEnumerable<Brand> GetAll();

        IEnumerable<ModelBrand> GetModelByBrandId(int brandId);

        IEnumerable<Brand> GetAll(string keyWord);


        IEnumerable<Brand> Search(string keyWord, int page, int pageSize, string sort, out int totalRow);

        Brand GetById(int id);

        void Save();
        

    }

    public class BrandService : IBrandService
    {
        private IBrandRepository _brandRepository;
        private IModelBrandRepository _modelBrandRepository;
        private IUnitOfWork _unitOfWork;

        public BrandService(IBrandRepository brandRepository, IModelBrandRepository modelBrandRepository, IUnitOfWork unitOfWork)
        {
            this._brandRepository = brandRepository;
            this._modelBrandRepository = modelBrandRepository;
            this._unitOfWork = unitOfWork;
        }

        public Brand Add(Brand Brand)
        {
            var menu = _brandRepository.Add(Brand);
            _unitOfWork.Commit();
          
            return menu;
        }

        public Brand Delete(int id)
        {
            return _brandRepository.Delete(id);
        }

        public IEnumerable<Brand> GetAll()
        {
            return _brandRepository.GetAll();
        }

        public IEnumerable<Brand> GetAll(string keyWord)
        {
            string[] includes = { "Products", "ModelBrands" };
            if (!string.IsNullOrEmpty(keyWord))
                return _brandRepository.GetMulti(x => x.Name.Contains(keyWord), includes);
            else
                return _brandRepository.GetAll(includes);
        }

        public Brand GetById(int id)
        {
            return _brandRepository.GetSingleById(id);
        }

        public IEnumerable<ModelBrand> GetModelByBrandId(int brandId)
        {
            return _modelBrandRepository.GetMulti(x => x.BrandID == brandId);

        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Brand Brand)
        {
            _brandRepository.Update(Brand);
        }

        public IEnumerable<string> GetListBrandByName(string Name)
        {
            return _brandRepository.GetMulti(x => x.Status && x.Name.Contains(Name)).Select(y => y.Name);
        }

        public IEnumerable<Brand> Search(string keyWord, int page, int pageSize, string sort, out int totalRow)
        {
            var query = _brandRepository.GetMulti(x => x.Status == true && x.Name.Contains(keyWord));
            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<ModelBrand> GetModelByBrandId()
        {
            throw new NotImplementedException();
        }
    }
}