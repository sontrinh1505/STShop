using System;
using System.Collections.Generic;
using System.Linq;
using TeduShop.Common;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    public interface IProductService
    {
        Product Add(Product Product);

        void Update(Product Product);

        Product Delete(int id);

        IEnumerable<Product> GetAll();

        IEnumerable<Product> GetAll(string keyWord);

        IEnumerable<Product> GetLastest(int top);

        IEnumerable<Product> GetTopSale(int top);

        IEnumerable<Product> GetBestSeller(int top);

        IEnumerable<Product> GetListProductByCategoryIdPaging(int categoryId, int page, int pageSize, string sort, out int totalRow);

        IEnumerable<Product> Search(string keyWord, int page, int pageSize, string sort, out int totalRow);

        IEnumerable<string> GetListProductByName(string Name);

        IEnumerable<Product> GetRelatedProducts(int id, int top);

        Product GetById(int id);

        Tag GetTag(string tagId);

        void Save();

        IEnumerable<Tag> GetListTagByProductId(int id);

        void IncreaseView(int id);

        IEnumerable<Product> GetListTProductByTag(string id, int page, int pageSize, out int totalRow);

        bool SellProduct(int productId, int quantity);
        

    }

    public class ProductService : IProductService
    {
        private IProductRepository _ProductRepository;
        private ITagRepository _TagRepository;
        private IProductTagRepository _ProductTagRepository;
        private IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository ProductRepository, ITagRepository tagRepository,
            IProductTagRepository productTagRepository, IUnitOfWork unitOfWork)
        {
            _ProductRepository = ProductRepository;
            this._TagRepository = tagRepository;
            this._ProductTagRepository = productTagRepository;
            this._unitOfWork = unitOfWork;
        }

        public Product Add(Product Product)
        {
            Product.Brand = null;
            Product.ProductCategory = null;
            var product = _ProductRepository.Add(Product);          
            _unitOfWork.Commit();
            if (!string.IsNullOrEmpty(Product.Tags))
            {
                string[] tags = Product.Tags.Split(',');
                for (var i = 0; i < tags.Length; i++)
                {
                    var tagId = StringHelper.ToUnsignString(tags[i]);
                    if (_TagRepository.Count(x => x.ID == tagId) == 0)
                    {
                        Tag tag = new Tag();
                        tag.ID = tagId;
                        tag.Name = tags[i];
                        tag.Type = ComomConstants.productTag;
                        _TagRepository.Add(tag);
                    }

                    ProductTag productTag = new ProductTag();
                    productTag.ProductID = Product.ID;
                    productTag.TagID = tagId;
                    _ProductTagRepository.Add(productTag);
                }
            }
            return product;

        }

        public Product Delete(int id)
        {
            return _ProductRepository.Delete(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _ProductRepository.GetAll();
        }

        public IEnumerable<Product> GetAll(string keyWord)
        {
            if (!string.IsNullOrEmpty(keyWord))
                return _ProductRepository.GetMulti(x => x.Name.ToLower().Contains(keyWord.ToLower()) || x.Description.ToLower().Contains(keyWord.ToLower()));
            else
                return _ProductRepository.GetAll();
        }

        public Product GetById(int id)
        {
            return _ProductRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Product Product)
        {
            Product.Brand = null;
            Product.ProductCategory = null;

            _ProductRepository.Update(Product);
            if (!string.IsNullOrEmpty(Product.Tags))
            {
                string[] tags = Product.Tags.Split(',');
                for (var i = 0; i < tags.Length; i++)
                {
                    var tagId = StringHelper.ToUnsignString(tags[i]);
                    if (_TagRepository.Count(x => x.ID == tagId) == 0)
                    {
                        Tag tag = new Tag();
                        tag.ID = tagId;
                        tag.Name = tags[i];
                        tag.Type = ComomConstants.productTag;
                        _TagRepository.Add(tag);
                    }
                    _ProductTagRepository.DeleteMulti(x => x.ProductID == Product.ID);
                    ProductTag productTag = new ProductTag();
                    productTag.ProductID = Product.ID;
                    productTag.TagID = tagId;
                    _ProductTagRepository.Add(productTag);
                }

            }
        }

        public IEnumerable<Product> GetLastest(int top)
        {
            string[] includes = { "Brand" };
            return _ProductRepository.GetMulti(x => x.Status == true && x.HomeFlag == true, includes).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public IEnumerable<Product> GetTopSale(int top)
        {
            string[] includes = { "Brand" };
            return _ProductRepository.GetMulti(x => x.Status == true && x.PromotionPrice != null && x.PromotionPrice <= x.Price, includes).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public IEnumerable<Product> GetBestSeller(int top)
        {
            string[] includes = { "Brand" };
            return _ProductRepository.GetMulti(x => x.Status == true && x.HotFlag == true, includes).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public IEnumerable<Product> GetListProductByCategoryIdPaging(int categoryId, int page, int pageSize, string sort, out int totalRow)
        {
            var query = _ProductRepository.GetMulti(x => x.Status == true && x.CategoryID == categoryId);
            switch(sort)
            {
                case "popular":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;
                case "discount":
                    query = query.OrderByDescending(x => x.PromotionPrice.HasValue);
                    break;
                case "price":
                    query = query.OrderBy(x => x.Price);
                    break;
                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
            }
            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<string> GetListProductByName(string Name)
        {
            return _ProductRepository.GetMulti(x => x.Status && x.Name.Contains(Name)).Select(y => y.Name);
        }

        public IEnumerable<Product> Search(string keyWord, int page, int pageSize, string sort, out int totalRow)
        {
            var query = _ProductRepository.GetMulti(x => x.Status == true && x.Name.Contains(keyWord));
            switch (sort)
            {
                case "popular":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;
                case "discount":
                    query = query.OrderByDescending(x => x.PromotionPrice.HasValue);
                    break;
                case "price":
                    query = query.OrderBy(x => x.Price);
                    break;
                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
            }
            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<Product> GetRelatedProducts(int id, int top)
        {
            var product = _ProductRepository.GetSingleById(id);
            return _ProductRepository.GetMulti(x => x.Status && x.ID != id && x.CategoryID == product.CategoryID).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public IEnumerable<Tag> GetListTagByProductId(int id)
        {
            return _ProductTagRepository.GetMulti(x => x.ProductID == id, new string[] {"Tag"}).Select(y => y.Tag);
        }

        public void IncreaseView(int id)
        {
            var product = _ProductRepository.GetSingleById(id);
            if (product.ViewCount.HasValue)
                product.ViewCount += 1;
            else
                product.ViewCount = 1; 
        }

        public IEnumerable<Product> GetListTProductByTag(string id, int page, int pageSize, out int totalRow)
        {
            return _ProductRepository.GetListProductByTag(id, page, pageSize, out totalRow);
        }

        public Tag GetTag(string tagId)
        {
            return _TagRepository.GetSingleByCondition(x => x.ID == tagId);
        }

        public bool SellProduct(int productId, int quantity)
        {
            var product = _ProductRepository.GetSingleById(productId);

            if (product.Quantity < quantity)
                return false;

            product.Quantity -= quantity;
            return true;
        }
    }
}