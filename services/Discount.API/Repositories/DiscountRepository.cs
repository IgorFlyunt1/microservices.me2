using Dapper;
using Discount.API.DbContext;
using Discount.API.Entities;
using Npgsql;

namespace Discount.API.Repositories;

public class DiscountRepository : IDiscountRepository
{
    private readonly DapperContext _dapperContext;

    public DiscountRepository(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<Coupon> GetDiscount(string productName)
    {
        await using var connection = _dapperContext.CreateNpgSqlConnection();

        var query = $"SELECT * FROM Coupon WHERE ProductName = @ProductName";

        return await connection.QueryFirstOrDefaultAsync<Coupon>(query, new {ProductName = productName});
    }

    public async Task<bool> CreateDiscount(Coupon coupon)
    {
        await using var connection = _dapperContext.CreateNpgSqlConnection();

        var affected = await connection.ExecuteAsync(
            "INSERT INTO Coupon (ProductName, Description, Amount) VALUES(@ProductName, @Description, @Amount)",
            new {coupon.ProductName, coupon.Description, coupon.Amount});

        return await Task.FromResult(affected > 0);
    }

    public async Task<bool> UpdateDiscount(Coupon coupon)
    {
        await using var connection = _dapperContext.CreateNpgSqlConnection();

        var affected = await connection.ExecuteAsync(
            "UPDATE Coupon SET ProductName = @ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id",
            new {coupon.ProductName, coupon.Description, coupon.Amount, coupon.Id});

        return await Task.FromResult(affected > 0);
    }

    public async Task<bool> DeleteDiscount(string productName)
    {
        await using var connection = _dapperContext.CreateNpgSqlConnection();

        var affected = await connection.ExecuteAsync("DELETE FROM Coupon WHERE ProductName = @ProductName",
            new {ProductName = productName});

        return await Task.FromResult(affected > 0);
    }
}