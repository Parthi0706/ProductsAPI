using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Common.Common;
using ProductsAPI.DataAccess.Repository;
using ProductsAPI.Models.Products;

namespace ProductsAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductDBManager _productDBManager; // Dependency for managing product data
        private readonly ILogger<ProductsController> _logger; // Logger for logging information and errors

        // Constructor to initialize the dependencies through dependency injection
        public ProductsController(IProductDBManager productDBManager, ILogger<ProductsController> logger)
        {
            _productDBManager = productDBManager;
            _logger = logger;
        }

        /// <summary>
        /// Gets all the products.
        /// </summary>
        /// <returns>Returns a list of all products or a 404 if no products are found.</returns>
        [HttpGet("GetAllProducts")]
        public ActionResult<IEnumerable<Products>> GetAllProducts()
        {
            try
            {
                _logger.LogInformation("Calling GetAllProducts");

                // Fetch all products from the database
                var allproducts = _productDBManager.GetAll();

                // Return the result as 200 (OK) or 404 (Not Found) based on data availability
                return allproducts == null ? NotFound("Product not found.") : Ok(allproducts);
            }
            catch (Exception ex)
            {
                // Log the exception and return a 400 (Bad Request)
                _logger.LogError("GetAllProducts catch Exception: " + ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Gets a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <returns>Returns the product if found, or appropriate status code.</returns>
        [HttpGet("GetProductsByid")]
        [RequireQueryParameter("id")]
        public ActionResult<Products> GetAllProductsByid([FromQuery] int id)
        {
            try
            {
                _logger.LogInformation("Calling GetProductsByid");

                if (id > 0)
                {
                    // Fetch product by ID
                    var product = _productDBManager.GetById(id);

                    // Return the product as 200 (OK) or 404 (Not Found)
                    return product == null ? NotFound("Product not found") : Ok(product);
                }
                else
                {
                    // Return a 400 (Bad Request) if the ID is not valid
                    _logger.LogError("Id should be not be Zero");
                    return BadRequest("Id should be not be Zero");
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return a 400 (Bad Request)
                _logger.LogError("GetProductsByid catch Exception: " + ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Inserts a new product into the database.
        /// </summary>
        /// <param name="product">The product details to be inserted.</param>
        /// <returns>Returns a success message or an error.</returns>
        [HttpPost("InsertProducts")]
        public async Task<IActionResult> InsertProducts([FromBody] Products product)
        {
            try
            {
                _logger.LogInformation("Calling InsertProducts");

                if (product == null)
                {
                    _logger.LogError("No product provided to insert");
                    return BadRequest(SaveUpdateMessage.NoRecordsInsert); // Return error if product is null
                }

                // Add the product to the database
                var createdProduct = _productDBManager.Add(product);

                if (createdProduct != null && createdProduct.Id > 0)
                {
                    _logger.LogInformation($"Product {createdProduct.Name} inserted successfully");
                    return Ok($"{createdProduct.Name} {SaveUpdateMessage.Saved}"); // Return success
                }

                _logger.LogError("No product was inserted");
                return BadRequest(SaveUpdateMessage.NoRecordsInsert);
            }
            catch (Exception ex)
            {
                // Log the exception and return a 400 (Bad Request)
                _logger.LogError("InsertProducts catch Exception: " + ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="product">The product details to be updated.</param>
        /// <returns>Returns a success message or an error.</returns>
        [HttpPut("UpdateProducts")]       
        public async Task<IActionResult> UpdateProducts([FromBody] Products product)
        {
            try
            {
                _logger.LogInformation("Calling UpdateProducts");

                if (product == null)
                {
                    _logger.LogError("No product provided to update");
                    return BadRequest(SaveUpdateMessage.NoRecordsUpdate); // Return error if product is null
                }
                else
                {
                    if (product.Id > 0)
                    {

                        // Update the product in the database
                        var updatedProduct = _productDBManager.Update(product);

                        if (updatedProduct != null && updatedProduct.Id > 0)
                        {
                            _logger.LogInformation($"Product with ID {updatedProduct.Id} updated successfully");
                            return Ok($"{updatedProduct.Name} was {SaveUpdateMessage.Saved}"); // Return success
                        }

                        _logger.LogError("No product was updated");
                        return BadRequest(SaveUpdateMessage.NoRecordsUpdate);
                    }
                    else
                    {
                        // Return a 400 (Bad Request) if the ID is not valid
                        _logger.LogError("Id should be not be Zero");
                        return BadRequest("Id should be not be Zero");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return a 400 (Bad Request)
                _logger.LogError("UpdateProducts catch Exception: " + ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to be deleted.</param>
        /// <returns>Returns a success message or an error.</returns>
        [HttpDelete("DeleteProducts")]
        [RequireQueryParameter("id")]
        public IActionResult DeleteProducts([FromQuery] int id)
        {
            try
            {
                if (id > 0)
                {
                    _logger.LogInformation("Calling DeleteProducts");

                    // Delete the product from the database
                    _productDBManager.Delete(id);

                    _logger.LogInformation($"Product with ID {id} deleted successfully");
                    return Ok($"Product was  {SaveUpdateMessage.Deleted}"); // Return success
                }
                else
                {
                    // Return a 400 (Bad Request) if the ID is not valid
                    _logger.LogError("Id should be not be Zero");
                    return BadRequest("Id should be not be Zero");
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return a 400 (Bad Request)
                _logger.LogError("DeleteProducts catch Exception: " + ex.ToString());
                return BadRequest(ex.Message);
            }
        }
    }

}
