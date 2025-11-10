using AcunmedyaSunum.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace AcunmedyaSunum.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly SqlConnection _sqlConnection;

    public OrderController(IConfiguration configuration)
    {
        _sqlConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        if (_sqlConnection.State != System.Data.ConnectionState.Open)
            _sqlConnection.Open();

        var command = new SqlCommand("SELECT * FROM Orders WHERE Id = @Id", _sqlConnection);
        command.Parameters.AddWithValue("@Id", id);
        SqlDataReader reader = null;

        try
        {
            reader = await command.ExecuteReaderAsync();
            if (!reader.HasRows)
                return NotFound();

            await reader.ReadAsync();
            var order = new
            {
                Id = reader["Id"],
                CustomerFirstName = reader["CustomerFirstName"],
                CustomerLastName = reader["CustomerLastName"],
                Email = reader["Email"],
                Address = reader["Address"],
                PhoneNumber = reader["PhoneNumber"],
                ProductName = reader["ProductName"],
                Barcode = reader["Barcode"],
                InvoiceAmount = reader["InvoiceAmount"],
                CreatedAt = reader["CreatedAt"]
            };

            return Ok(order);
        }
        catch (SqlException ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
        finally
        {
            reader?.Close();
            _sqlConnection.Close();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateOrderModel model)
    {
        if (model == null)
            return BadRequest("Invalid order data.");

        if (_sqlConnection.State != System.Data.ConnectionState.Open)
            _sqlConnection.Open();

        var command = new SqlCommand(@"
                INSERT INTO Orders 
                (CustomerFirstName, CustomerLastName, Email, Address, PhoneNumber, ProductName, Barcode, InvoiceAmount) 
                VALUES 
                (@CustomerFirstName, @CustomerLastName, @Email, @Address, @PhoneNumber, @ProductName, @Barcode, @InvoiceAmount)",
            _sqlConnection);

        command.Parameters.AddWithValue("@CustomerFirstName", model.CustomerFirstName);
        command.Parameters.AddWithValue("@CustomerLastName", model.CustomerLastName);
        command.Parameters.AddWithValue("@Email", model.Email);
        command.Parameters.AddWithValue("@Address", model.Address);
        command.Parameters.AddWithValue("@PhoneNumber", model.PhoneNumber);
        command.Parameters.AddWithValue("@ProductName", model.ProductName);
        command.Parameters.AddWithValue("@Barcode", model.Barcode);
        command.Parameters.AddWithValue("@InvoiceAmount", model.InvoiceAmount);

        try
        {
            await command.ExecuteNonQueryAsync();
            return CreatedAtAction(nameof(Post), new { model.Email }, model);
        }
        catch (SqlException ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
        finally
        {
            _sqlConnection.Close();
        }
    }
}
