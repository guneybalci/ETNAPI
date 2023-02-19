using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IOrderService
    {
        IDataResult<Order> GetById(int orderId);
        IDataResult<List<Order>> GetAll();
        IDataResult<List<Order>> Add(List<Order> orders);
        IResult Delete(Order order);
        IResult Update(Order order);

        IDataResult<List<Order>> GetByProductCode(int productCode);
    }
}
