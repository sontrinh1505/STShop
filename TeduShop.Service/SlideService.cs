using System;
using System.Collections.Generic;
using System.Linq;
using TeduShop.Common;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    public interface ISlideService
    {
        Slide Add(Slide Slide);

        void Update(Slide Slide);

        Slide Delete(int id);

        IEnumerable<Slide> GetAll();

        IEnumerable<Slide> GetAll(string keyWord);

        IEnumerable<Slide> Search(string keyWord, int page, int pageSize, string sort, out int totalRow);

        Slide GetById(int id);

        void Save();
        

    }

    public class SlideService : ISlideService
    {
        private ISlideRepository _slideRepository;
        private IUnitOfWork _unitOfWork;

        public SlideService(ISlideRepository slideRepository, IUnitOfWork unitOfWork)
        {
            this._slideRepository = slideRepository;
            this._unitOfWork = unitOfWork;
        }

        public Slide Add(Slide Slide)
        {
            var menu = _slideRepository.Add(Slide);
            _unitOfWork.Commit();
          
            return menu;
        }

        public Slide Delete(int id)
        {
            return _slideRepository.Delete(id);
        }

        public IEnumerable<Slide> GetAll()
        {
            return _slideRepository.GetMulti(x => x.Status == true);
        }

        public IEnumerable<Slide> GetAll(string keyWord)
        {
            //string[] includes = { "SlideGroup" };
            if (!string.IsNullOrEmpty(keyWord))
                return _slideRepository.GetMulti(x => x.Name.Contains(keyWord));
            else
                return _slideRepository.GetAll();
        }

        public Slide GetById(int id)
        {
            return _slideRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Slide Slide)
        {
            _slideRepository.Update(Slide);
        }

        public IEnumerable<string> GetListSlideByName(string Name)
        {
            return _slideRepository.GetMulti(x => x.Status && x.Name.Contains(Name)).Select(y => y.Name);
        }

        public IEnumerable<Slide> Search(string keyWord, int page, int pageSize, string sort, out int totalRow)
        {
            var query = _slideRepository.GetMulti(x => x.Status == true && x.Name.Contains(keyWord));
            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        
    }
}