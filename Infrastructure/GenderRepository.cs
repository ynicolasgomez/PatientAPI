using Dapper;
using Microsoft.Data.SqlClient;
using PatientAPI.Models;
using System.Data;

namespace PatientAPI.Infrastructure;

public class GenderRepository
{
    private readonly string _connectionString;

    public GenderRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    private IDbConnection Connection => new SqlConnection(_connectionString);

    public async Task<IEnumerable<Gender>> GetAllAsync()
    {
        using var db = Connection;
        var sql = "SELECT Id, Name FROM Gender";
        return await db.QueryAsync<Gender>(sql);
    }
}
