using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<List<Product>> GetAll();

        // Tek bir ürünle ilgili bilgiler
        IDataResult<Product> GetById(int productId);

        IDataResult<List<Product>> Add(List<Product> products);

        //IResult Add(Product product);

        IResult Update(Product product);

        IResult Delete(Product product);

        //IDataResult<List<ProductDetailDto>> GetProductDetails();
      
    }
}
