using Dapper;
using InvestimentoApi.Context;
using InvestimentoApi.Models;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace InvestimentoApi.Repositories
{
    public class ClasseRepository : IRepository<Classe>
    {
        private readonly DapperContext _context;

        public ClasseRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task Add(Classe classe)
        {
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                await dbConnection.ExecuteAsync("insert into Classes (Descricao) values (@Descricao)", classe);
            }
        }

        public async Task<IEnumerable<Classe>> FindAll()
        {
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                return await dbConnection.QueryAsync<Classe>("select * from Classes");
            }
        }

        public async Task<Classe> FindByID(int id)
        {
            using (var connection = _context.CreateConnection())
            {
                Classe classe = await connection.QuerySingleOrDefaultAsync<Classe>("select * from Classes where Id = @id", new { id });
                return classe;
            }
        }

        public async Task Remove(int id)
        {
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                await dbConnection.ExecuteAsync("delete from Classes where Id = @id", new { id });
            }
        }

        public async Task Update(Classe classe)
        {
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                await dbConnection.ExecuteAsync("update Classes set Descricao=@Descricao where Classes.Id=@Id", new { classe.Descricao, classe.Id });
            }
        }
    }
}
