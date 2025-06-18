using Dapper; 
using Microsoft.Data.SqlClient;
using PatientAPI.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

public class ExamRepository
{
    private readonly string _connectionString;

    public ExamRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    private IDbConnection Connection => new SqlConnection(_connectionString);

    public async Task<IEnumerable<Exam>> GetAllAsync()
    {
        using var db = Connection;
        var sql = "SELECT Id, Nombre, Descripcion FROM Examen";
        return await db.QueryAsync<Exam>(sql);
    }

    public async Task AddAsync(Exam exam)
    {
        using var db = Connection;
        var sql = @"INSERT INTO Examen (Nombre, Descripcion) 
                    VALUES (@Nombre, @Descripcion)";
        await db.ExecuteAsync(sql, exam);
    }
}
