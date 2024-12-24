using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Common.Common;
using ProductsAPI.DataAccess.Repository;
using ProductsAPI.Models.Products;

namespace ProductsAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductDBManager _productDBManager;
        private readonly ILogger<ProductsController> _logger;
        public ProductsController(IProductDBManager productDBManager, ILogger<ProductsController> logger)
        {
            _productDBManager = productDBManager;
            _logger = logger;
        }

        [HttpGet("GetAllProducts")]
        public ActionResult<IEnumerable<Products>> GetAllProducts()
        {
            try
            {
                _logger.LogInformation("Calling GetAllProducts");
                var allproducts = _productDBManager.GetAll();

                return allproducts == null ? NotFound() : Ok(allproducts);

            }
            catch (Exception ex)
            {
                _logger.LogError("GetAllProducts catch Exception" + ex.ToString());
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetProductsByid")]
        [RequireQueryParameter("id")]
        public async Task<IActionResult> GetAllProductsByid([FromQuery] int id)
        {
            try
            {
                _logger.LogInformation("Calling GetProductsByid");
                if (id > 0)
                {
                    var product = _productDBManager.GetById(id);
                    return product == null ? NotFound() : Ok(product);
                }
                else
                {
                    return BadRequest("id is required");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("GetProductsByid catch Exception" + ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("InsertProducts")]
        public async Task<IActionResult> InsertProducts([FromBody] Products product)
        {
            try
            {
                _logger.LogInformation("Calling InsertProducts");
                if (product == null)
                {
                    _logger.LogError("No products is there to insert");
                    return BadRequest(SaveUpdateMessage.NoRecordsInsert);
                }
                else
                {
                    var createdProduct = _productDBManager.Add(product);
                    if (createdProduct != null)
                    {
                        if (createdProduct.Id > 0)
                        {
                            _logger.LogInformation("Product name with " + createdProduct.Name + " was inserted sucessfully");
                            return Ok(createdProduct.Name + " " + SaveUpdateMessage.Saved);
                        }
                        else
                        {
                            _logger.LogError("No products is there to insert");
                            return BadRequest(SaveUpdateMessage.NoRecordsInsert);
                        }
                    }
                    else
                    {
                        _logger.LogError("No products is there to insert");
                        return BadRequest(SaveUpdateMessage.NoRecordsInsert);
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("InsertProducts catch Exception" + ex.ToString());
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("UpdateProducts")]
        [RequireQueryParameter("id")]
        public async Task<IActionResult> UpdateProducts([FromBody] Products product)
        {
            try
            {
                _logger.LogInformation("Calling UpdateProducts");
                if (product == null) return BadRequest();
                var updatedProduct = _productDBManager.Update(product);
                if (updatedProduct != null)
                {
                    if (updatedProduct.Id > 0)
                    {
                        _logger.LogInformation("Product Id with " + updatedProduct.Id + " was updated sucessfully");

                        return Ok(updatedProduct.Name + " " + SaveUpdateMessage.Saved);
                    }
                    else
                    {
                        _logger.LogError("No products is there to update");
                        return BadRequest(SaveUpdateMessage.NoRecordsUpdate);
                    }
                }
                else
                {
                    _logger.LogError("No products is there to update");
                    return BadRequest(SaveUpdateMessage.NoRecordsUpdate);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("UpdateProducts catch Exception" + ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteProducts")]
        [RequireQueryParameter("id")]
        public IActionResult DeleteProducts([FromQuery] int id)
        {
            try
            {
                _logger.LogInformation("Calling DeleteProducts");

                _productDBManager.Delete(id);
                _logger.LogInformation("Product Id with " + id + " was deleted sucessfully");

                return Ok(SaveUpdateMessage.Deleted);

            }
            catch (Exception ex)
            {
                _logger.LogError("DeleteProducts catch Exception" + ex.ToString());
                return BadRequest(ex.Message);
            }
        }
    }
}
