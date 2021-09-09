using Dapper;
using InvestimentoApi.Context;
using InvestimentoApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace InvestimentoApi.Repositories
{
    public class AtivoRepository : IRepository<Ativo>
    {
        private readonly DapperContext _context;

        public AtivoRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task Add(Ativo ativo)
        {
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                await dbConnection.ExecuteAsync(
                    "insert into Ativos (Codigo, Descricao, ClasseId) values (@Codigo, @Descricao, @ClasseId)", 
                    new { ativo.Codigo, ativo.Descricao, ClasseId = ativo.Classe.Id }); 
            }
        }

        public async Task<IEnumerable<Ativo>> FindAll()
        {
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                return await dbConnection.QueryAsync<Ativo, Classe, Ativo>(
                    "select * from Ativos " +
                    "inner join Classes on Ativos.ClasseId = Classes.Id",
                    map: (ativo, classe) =>
                    {
                        ativo.Classe = classe;
                        return ativo;
                    },
                    splitOn: "ClasseId"
                    );
            }
        }

        public async Task<Ativo> FindByID(int id)
        {
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                IEnumerable<Ativo> ativos = await dbConnection.QueryAsync<Ativo, Classe, Ativo>(
                    "select * from Ativos " +
                    "inner join Classes on Ativos.ClasseId = Classes.Id " +
                    "where Ativos.Id = @id",
                    map: (ativo, classe) =>
                    {
                        ativo.Classe = classe;
                        return ativo;
                    },
                    splitOn: "ClasseId",
                    param: new { id }
                    );

                return ativos.FirstOrDefault();
            }
        }

        public async Task Remove(int id)
        {
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                await dbConnection.ExecuteAsync("delete from Ativos where Id=@id", new { id });
            }
        }

        public async Task Update(Ativo ativo)
        {
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                await dbConnection.ExecuteAsync(
                    "update Ativos set " +
                    "Codigo=@Codigo, Descricao=@Descricao, ClasseId=@ClasseId " +
                    "where Ativos.Id=@Id"
                    , new { ativo.Codigo, ativo.Descricao, ClasseId = ativo.Classe.Id, ativo.Id });
            }
        }
    }
}
