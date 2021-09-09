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
    public class AporteRepository : IRepository<Aporte>
    {
        private readonly DapperContext _context;

        public AporteRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task Add(Aporte item)
        {
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                await dbConnection.ExecuteAsync(
                    "insert into Aportes (Quantidade, PrecoEnvio, AtivoId) values (@Quantidade, @PrecoEnvio, @AtivoId)",
                    new { item.Quantidade, item.PrecoEnvio, AtivoId = item.Ativo.Id });
            }
        }

        public async Task<IEnumerable<Aporte>> FindAll()
        {
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                return await dbConnection.QueryAsync<Aporte, Ativo, Classe, Aporte>(
                    "select * from Aportes " +
                    "inner join Ativos on Aportes.AtivoId = Ativos.Id " +
                    "inner join Classes on Ativos.ClasseId = Classes.Id",
                    map: (aporte, ativo, classe) =>
                    {
                        ativo.Classe = classe; 
                        aporte.Ativo = ativo;
                        return aporte;
                    },
                    splitOn: "AtivoId,ClasseId"
                    );
            }
        }

        public async Task<Aporte> FindByID(int id)
        {
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                IEnumerable<Aporte> aportes = await dbConnection.QueryAsync<Aporte, Ativo, Classe, Aporte>(
                    "select * from Aportes " +
                    "inner join Ativos on Aportes.AtivoId = Ativos.Id " +
                    "inner join Classes on Ativos.ClasseId = Classes.Id " +
                    "where Aportes.Id = @id",
                    map: (aporte, ativo, classe) =>
                    {
                        ativo.Classe = classe;
                        aporte.Ativo = ativo;
                        return aporte;
                    },
                    splitOn: "AtivoId,ClasseId",
                    param: new { id }
                    );

                return aportes.FirstOrDefault();
            }
        }

        public async Task Remove(int id)
        {
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                await dbConnection.ExecuteAsync("delete from Aportes where Id = @id", new { id });
            }
        }

        public async Task Update(Aporte aporte)
        {
            using (IDbConnection dbConnection = _context.CreateConnection())
            {
                await dbConnection.ExecuteAsync(
                    "update Aportes set " +
                    "Quantidade=@Quantidade, PrecoEnvio=@PrecoEnvio, AtivoId=@AtivoId " +
                    "where Aportes.Id=@Id"
                    , new { aporte.Quantidade, aporte.PrecoEnvio, AtivoId = aporte.Ativo.Id, aporte.Id });
            }
        }
    }
}
