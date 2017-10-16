using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;
using TeduShop.Common.ViewModels;

namespace TeduShop.Service
{
    public interface IOrderService
    {
        bool Create(Order order, List<OrderDetail> orderDetail);
        
    }

    public class OrderService : IOrderService
    {
        IOrderRepository _orderRepository;
        IOrderDetailRepository _orderDetailRepository;
        IUnitOfWork _unitOfWork;
        public OrderService(IOrderRepository orderRepository, IUnitOfWork unitOfWork, IOrderDetailRepository orderDetailRepository)
        {
            this._orderRepository = orderRepository;
            this._unitOfWork = unitOfWork;
            this._orderDetailRepository = orderDetailRepository;
        }

        public bool Create(Order order, List<OrderDetail> orderDetail)
        {
            try
            {
                _orderRepository.Add(order);
                _unitOfWork.Commit();

                foreach (var detail in orderDetail)
                {
                    detail.OrderID = order.ID;
                    _orderDetailRepository.Add(detail);
                }

                return true;

            }
            catch (Exception ex)
            {
                throw;
            }

        }

        
    }
}
