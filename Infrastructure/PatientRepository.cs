using Dapper;
using Microsoft.Data.SqlClient;
using PatientAPI.Models;
using System.Data;

namespace PatientAPI.Infrastructure;

public class PatientRepository
{
    private readonly string _connectionString;

    public PatientRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    private IDbConnection Connection => new SqlConnection(_connectionString);

    public async Task<IEnumerable<Patient>> GetAllAsync()
    {
        using var db = Connection;
        return await db.QueryAsync<Patient>("EXEC GetAllPatients");
    }

    public async Task<Patient?> GetByIdAsync(int id)
    {
        using var db = Connection;
        return await db.QueryFirstOrDefaultAsync<Patient>("SELECT * FROM Patient WHERE Id = @Id", new { Id = id });
    }

    public async Task AddAsync(Patient patient)
    {
        using var db = Connection;
        var sql = @"INSERT INTO Patient (FirstName, LastName, DocumentNumber, DocumentTypeId, GenderId) 
                    VALUES (@FirstName, @LastName, @DocumentNumber, @DocumentTypeId, @GenderId)";
        await db.ExecuteAsync(sql, patient);
    }

    public async Task UpdateAsync(Patient patient)
    {
        using var db = Connection;
        var sql = @"UPDATE Patient SET FirstName = @FirstName, LastName = @LastName, DocumentNumber = @DocumentNumber,
                    DocumentTypeId = @DocumentTypeId, GenderId = @GenderId WHERE Id = @Id";
        await db.ExecuteAsync(sql, patient);
    }

    public async Task DeleteAsync(int id)
    {
        using var db = Connection;
        await db.ExecuteAsync("DELETE FROM Patient WHERE Id = @Id", new { Id = id });
    }
    public async Task<IEnumerable<Patient>> SearchAsync(string filter)
    {
        using var db = Connection;
        return await db.QueryAsync<Patient>(
            "SearchPatients",
            new { Filter = filter },
            commandType: CommandType.StoredProcedure
        );
    }

}
