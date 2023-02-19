using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ProductOrderManager : IProductOrderService
    {
        IProductOrderDal _productOrderDal;

        public ProductOrderManager(IProductOrderDal productOrderDal)
        {
            _productOrderDal = productOrderDal;
        }

        public IDataResult<ProductOrderDto> GetProductDetails()
        {
            return new SuccessDataResult<ProductOrderDto>(_productOrderDal.GetProductOrderDetails());
        }
    }
}
