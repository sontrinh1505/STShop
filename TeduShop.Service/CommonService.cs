using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Common;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    public interface ICommonService
    {
        Footer GetFooter();
        IEnumerable<Slide> GetSlides();
        SystemConfig GetSystemConfig(string code);
    }

    public class CommonService : ICommonService
    {
        IFooterRepository _FooterRepository;
        IUnitOfWork _unitOfWork;
        ISlideRepository _slideRepository;
        ISystemConfigRepository _systemConfigRepository;


        public CommonService(IFooterRepository footerRepository, ISlideRepository slideRepository, IUnitOfWork unitOfWork, ISystemConfigRepository systemConfigRepository)
        {
            this._FooterRepository = footerRepository;
            this._slideRepository = slideRepository;
            this._unitOfWork = unitOfWork;
            this._systemConfigRepository = systemConfigRepository;
        } 

        public Footer GetFooter()
        {
            return _FooterRepository.GetSingleByCondition(x => x.ID == ComomConstants.defaultFooterId);
            
        }

        public IEnumerable<Slide> GetSlides()
        {
            return _slideRepository.GetMulti(x => x.Status == true);
        }

        public SystemConfig GetSystemConfig(string code)
        {
            return _systemConfigRepository.GetSingleByCondition(x => x.Code == code);
        }
    }
}
