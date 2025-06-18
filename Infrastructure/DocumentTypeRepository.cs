using Dapper;
using Microsoft.Data.SqlClient;
using PatientAPI.Models;
using System.Data;

namespace PatientAPI.Infrastructure.DocumentType
{
    public class DocumentTypeRepository
    {
        private readonly string _connectionString;

        public DocumentTypeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection Connection => new SqlConnection(_connectionString);

        public async Task<List<Models.DocumentType>> GetAllAsync()
        {
            using var db = Connection;
            var sql = "SELECT Id, Name FROM DocumentType";
            var result = await db.QueryAsync<Models.DocumentType>(sql);
            return result.ToList();
        }
    }
}
