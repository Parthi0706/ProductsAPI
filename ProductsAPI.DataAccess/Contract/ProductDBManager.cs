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
        private readonly ProductDbContext _context; // Database context for interacting with the product database
        private readonly ILogger<ProductDBManager> _logger; // Logger for logging information and errors

        // Constructor to initialize dependencies via dependency injection
        public ProductDBManager(ProductDbContext context, ILogger<ProductDBManager> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all products from the database.
        /// </summary>
        /// <returns>An enumerable list of all products.</returns>
        public IEnumerable<Products> GetAll() => _context.Products.ToList();

        /// <summary>
        /// Retrieves a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>The product if found, or throws an exception if not.</returns>
        public async Task<Products> GetById(int id)
        {
            try
            {
                // Find the product by its ID
                var product = await _context.Products.FindAsync(id);

                // If product is not found, log a warning and throw a KeyNotFoundException
                if (product == null)
                {
                    _logger.LogWarning($"Product with Id {id} not found.");
                    throw new KeyNotFoundException($"Product with Id {id} not found.");
                }

                // Return the found product
                return product;
            }
            catch (Exception ex)
            {
                // Log the exception and rethrow a generic exception with additional details
                _logger.LogError("An error occurred while retrieving the product. " + ex);
                throw new Exception("An error occurred while retrieving the product.", ex);
            }
        }

        /// <summary>
        /// Adds a new product to the database.
        /// </summary>
        /// <param name="product">The product to add.</param>
        /// <returns>The added product with its assigned ID.</returns>
        public Products Add(Products product)
        {
            // Add the product to the database
            _context.Products.Add(product);

            // Save changes to persist the new product
            _context.SaveChanges();

            // Return the added product
            return product;
        }

        /// <summary>
        /// Updates an existing product in the database.
        /// </summary>
        /// <param name="product">The product with updated details.</param>
        /// <returns>The updated product.</returns>
        public Products Update(Products product)
        {
            // Update the product in the database
            _context.Products.Update(product);

            // Save changes to persist the update
            _context.SaveChanges();

            // Return the updated product
            return product;
        }

        /// <summary>
        /// Deletes a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        public void Delete(int id)
        {
            // Find the product by its ID
            var product = _context.Products.Find(id);

            // If the product exists, remove it from the database
            if (product != null)
            {
                _context.Products.Remove(product);

                // Save changes to persist the deletion
                _context.SaveChanges();
            }
        }
    }

}
