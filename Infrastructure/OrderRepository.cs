using Dapper;
using Microsoft.Data.SqlClient;
using PatientAPI.Models;
using System.Data;

namespace PatientAPI.Infrastructure;

public class OrderRepository
{
    private readonly string _connectionString;

    public OrderRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    private IDbConnection Connection => new SqlConnection(_connectionString);

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        using var db = Connection;
        var sql = "SELECT Id, PatientId, FechaOrden FROM Orden";
        return await db.QueryAsync<Order>(sql);
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
        using var db = Connection;
        var sql = "SELECT Id, PatientId, FechaOrden FROM Orden WHERE Id = @Id";
        return await db.QueryFirstOrDefaultAsync<Order>(sql, new { Id = id });
    }

    public async Task AddAsync(Order order)
    {
        using var db = Connection;
        var sql = @"INSERT INTO Orden (PatientId, FechaOrden) 
                    VALUES (@PatientId, @FechaOrden)";
        await db.ExecuteAsync(sql, order);
    }

    public async Task UpdateAsync(Order order)
    {
        using var db = Connection;
        var sql = @"UPDATE Orden 
                    SET PatientId = @PatientId, FechaOrden = @FechaOrden 
                    WHERE Id = @Id";
        await db.ExecuteAsync(sql, order);
    }

    public async Task DeleteAsync(int id)
    {
        using var db = Connection;
        var sql = "DELETE FROM Orden WHERE Id = @Id";
        await db.ExecuteAsync(sql, new { Id = id });
    }

    public async Task<IEnumerable<Order>> GetTodayOrdersAsync()
    {
        using var db = Connection;
        var sql = @"SELECT Id, PatientId, FechaOrden 
                    FROM Orden 
                    WHERE CAST(FechaOrden AS DATE) = CAST(GETDATE() AS DATE)";
        return await db.QueryAsync<Order>(sql);
    }

    public async Task<IEnumerable<dynamic>> GetOrderCountPerPatientAsync()
    {
        using var db = Connection;
        var sql = @"SELECT PatientId, COUNT(*) AS TotalOrdenes 
                    FROM Orden 
                    GROUP BY PatientId";
        return await db.QueryAsync(sql);
    }
}
