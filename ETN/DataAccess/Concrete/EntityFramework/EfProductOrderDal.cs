using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductOrderDal : EfEntityRepositoryBase<ProductOrderDto, EtnContext>, IProductOrderDal
    {
        public ProductOrderDto GetProductOrderDetails()
        {
            using (EtnContext context = new EtnContext())
            {
                var model = new ProductOrderDto();

                model.Products = context.Products.Include(i => i.ProductOrders).ThenInclude(i => i.Order).ToList();

                model.Orders = context.Orders.Include(i => i.ProductOrders).ThenInclude(i => i.Product).ToList();

                return model;
            }
        }
    }
}
