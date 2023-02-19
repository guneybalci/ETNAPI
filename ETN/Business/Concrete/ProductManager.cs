using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Logging;
using Core.Aspects;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore.Internal;
using Remotion.Linq.Parsing;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }


        //[SecuredOperation("product.add")]
        //Fluent Validation
        //[ValidationAspect(typeof(ProductValidator))]
        public IDataResult<List<Product>> Add(List<Product> products)
        {
            foreach (var item in products)
            {
                var result = _productDal.GetAll(p => p.ProductCode == item.ProductCode).Any();
                if (result)
                    return new ErrorDataResult<List<Product>>(Messages.ProductNameAlreadyExists);
                _productDal.Add(item);
            }

            return new SuccessDataResult<List<Product>>(products, Messages.ProductAdded);
        }

        //[ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new Result(true, Messages.ProductUptaded);
        }

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult(Messages.ProductDeleted);
        }

        //[LogAspect(typeof(DatabaseLogger))]
        public IDataResult<List<Product>> GetAll()
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductsListed);

        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.Id == productId));
        }

     
    }
}
