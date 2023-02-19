using Business.Abstract;
using Business.Constants;
using Business.Enums;
using Business.ValidationRules.FluentValidation;
using Castle.Core.Resource;
using Core.Aspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Extensions;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using Newtonsoft.Json.Linq;
using Remotion.Linq.Clauses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private IOrderDal _orderDal;
        private IProductOrderService _productOrderService;
        private IProductService _productService;
        public OrderManager(IOrderDal orderDal, IProductOrderService productOrderService, IProductService productService)
        {
            _orderDal = orderDal;
            _productOrderService = productOrderService;
            _productService = productService;
        }


        [LogAspect(typeof(FileLogger))]
        //[SecuredOperation("customer")]
        [ValidationAspect(typeof(OrderValidator))]
        public IDataResult<List<Order>> Add(List<Order> orders)
        {
            IResult result = BusinessRules.Run(CheckIfProductCodesExists(orders), CheckIfCustomerOrderNoExists(orders));
            if (result != null)
                return (IDataResult<List<Order>>)result;

            OrderStatus SiparisAlindi = OrderStatus.SiparisAlindi;
            string enumDescription = SiparisAlindi.GetEnumDescription();

            //ProductOrderDto productOrderDto = _productOrderService.GetProductDetails().Data;
            foreach (var item in orders)
            {
                item.Status = enumDescription;
                _orderDal.Add(item);
            }

            return new SuccessDataResult<List<Order>>(orders.Select(x => new Order { Id = x.Id, OrderNo = x.OrderNo, Status = x.Status }).ToList(), Messages.OrderAdded);

        }

        [LogAspect(typeof(FileLogger))]
        //[SecuredOperation("customer")]
        public IResult Delete(Order order)
        {
            _orderDal.Delete(order);
            return new SuccessResult(Messages.OrderDeleted);
        }

        [LogAspect(typeof(FileLogger))]
        //[SecuredOperation("customer")]
        public IDataResult<Order> GetById(int orderId)
        {
            return new SuccessDataResult<Order>(_orderDal.Get(o => o.Id == orderId));
        }

        [LogAspect(typeof(FileLogger))]
        //[SecuredOperation("customer")]
        public IDataResult<List<Order>> GetAll()
        {
            return new SuccessDataResult<List<Order>>(_orderDal.GetAll());
        }


        [LogAspect(typeof(FileLogger))]
        //[SecuredOperation("customer")]
        public IResult Update(Order order)
        {
            _orderDal.Update(order);
            return new SuccessResult(Messages.OrderUpdated);
        }

        private IResult CheckIfCustomerOrderNoExists(List<Order> orders)
        {
            foreach (var item in orders)
            {
                var result = _orderDal.GetAll(o => o.OrderNo == item.OrderNo).Any();
                if (result)
                {
                    return new ErrorResult(Messages.ProductNameAlreadyExists);
                }
            }

            return new SuccessResult();
        }

        private IResult CheckIfProductCodesExists(List<Order> orders)
        {
            List<Order> checkedOrders = new List<Order>();
            Random rnd = new Random();
            foreach (var item in orders)
            {
                var result = _orderDal.GetAll(o => o.ProductCode == item.ProductCode).Any();
                if (result == false)
                    checkedOrders.Add(item);
            }

            foreach (var item in checkedOrders)
            {
                List<Product> products = new List<Product>{new Product { ProductCode = item.ProductCode, ProductName="Ürün"+Convert.ToString(rnd.Next())}};
                _productService.Add(products);
            }

            return new SuccessResult();
        }

        public IDataResult<List<Order>> GetByProductCode(int productCode)
        {
            return new SuccessDataResult<List<Order>>(_orderDal.GetAll(o => o.ProductCode == productCode), Messages.ProductsListed);
        }
    }
}
