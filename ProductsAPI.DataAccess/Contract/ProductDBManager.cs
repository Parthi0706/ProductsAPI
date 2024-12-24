using Microsoft.Extensions.Logging;
using ProductsAPI.Common.DBContext;
using ProductsAPI.DataAccess.Repository;
using ProductsAPI.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAPI.DataAccess.Contract
{
    public class ProductDBManager : IProductDBManager
    {
        private readonly ProductDbContext _context;
        private readonly ILogger<ProductDBManager> _logger;
        public ProductDBManager(ProductDbContext context, ILogger<ProductDBManager> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Products> GetAll() => _context.Products.ToList();

        public async Task<Products> GetById(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    // Log warning or throw a custom exception
                    _logger.LogWarning($"Product with Id {id} not found.");
                    throw new KeyNotFoundException($"Product with Id {id} not found.");
                }
                return product;
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogWarning("An error occurred while retrieving the product. " + ex);
                throw new Exception("An error occurred while retrieving the product.", ex);
            }
        }

        public Products Add(Products product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

        public Products Update(Products product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
            return product;
        }

        public void Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }
    }
}
