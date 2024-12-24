using ProductsAPI.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAPI.DataAccess.Repository
{
    public interface IProductDBManager
    {
        IEnumerable<Products> GetAll();
        Task<Products> GetById(int id);
        Products Add(Products product);
        Products Update(Products product);
        void Delete(int id);
    }
}
