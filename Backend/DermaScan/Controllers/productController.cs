using DermaScan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;

namespace DermaScan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class productController : ControllerBase
    {

        [HttpPost]
        public ActionResult SaveProductData([FromBody] productRequest productRequest)
        {
            if (productRequest == null)
                return BadRequest("Request is null");

            if (string.IsNullOrEmpty(productRequest.ProductType) ||
                string.IsNullOrEmpty(productRequest.ProductName))
                return BadRequest("Missing required fields");

            try
            {
                using (var connection = new SqlConnection("Server=DESKTOP-E8PH0L7;Database=dermascan;Integrated Security=True;TrustServerCertificate=True"))
                using (var command = new SqlCommand("sp_SaveInventoryData", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    var productIdParam = new SqlParameter("@ProductID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(productIdParam);
                    command.Parameters.AddWithValue("@ProductType", productRequest.ProductType);
                    command.Parameters.AddWithValue("@ProductName", productRequest.ProductName);
                    command.Parameters.AddWithValue("@ExpireDate", productRequest.ExpireDate);

                    connection.Open();
                    command.ExecuteNonQuery();

                    if (productIdParam.Value != DBNull.Value)
                    {
                        int newProductId = (int)productIdParam.Value;

                        // Return ALL product details including the new ID
                        return Ok(new
                        {
                            productId = newProductId,
                            productType = productRequest.ProductType,
                            productName = productRequest.ProductName,
                            expireDate = productRequest.ExpireDate
                        });
                    }
                    return BadRequest("Failed to save product data.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet]
        public ActionResult GetProductData()
        {
            using (SqlConnection connection = new SqlConnection
            {
                ConnectionString = "Server=DESKTOP-E8PH0L7;Database=dermascan;Integrated Security=True;TrustServerCertificate=True"
            })
            {
                using (SqlCommand command = new SqlCommand
                {
                    CommandText = "sp_GetInventoryData",
                    CommandType = CommandType.StoredProcedure,
                    Connection = connection
                })
                {
                    connection.Open();

                    List<InventoryDto> respon = new List<InventoryDto>();

                    using (SqlDataReader sqlDataReader = command.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            InventoryDto inventoryDto = new InventoryDto();
                            inventoryDto.ProductId = Convert.ToInt32(sqlDataReader["ProductID"]);
                            inventoryDto.ProductType = Convert.ToString(sqlDataReader["ProductType"]);
                            inventoryDto.ProductName = Convert.ToString(sqlDataReader["ProductName"]);
                            inventoryDto.ExpireDate = DateOnly.FromDateTime(Convert.ToDateTime(sqlDataReader["ExpireDate"]));

                            respon.Add(inventoryDto);
                        }
                    }

                    connection.Close();
                    return Ok(JsonConvert.SerializeObject(respon));
                }
            }
        }

        [HttpDelete]
        public ActionResult DeleteProductData(int productId)
        {
            using (SqlConnection connection = new SqlConnection
            {
                ConnectionString = "Server=DESKTOP-E8PH0L7;Database=dermascan;Integrated Security=True;TrustServerCertificate=True"
            })
            {
                using (SqlCommand command = new SqlCommand
                {
                    CommandText = "sp_DeleteInventoryDetails",
                    CommandType = CommandType.StoredProcedure,
                    Connection = connection
                })
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@ProductID", productId);

                    command.ExecuteNonQuery();

                    connection.Close();
                    return Ok();
                }
            }
        }
    }
}
