using InvestimentoApi.Repositories;
using InvestimentoApi.DTOs;
using InvestimentoApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InvestimentoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtivosController : ControllerBase
    {
        private readonly IRepository<Ativo> _ativoRepository;

        public AtivosController(IRepository<Ativo> ativoRepository)
        {
            _ativoRepository = ativoRepository;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                IEnumerable<Ativo> classes = await _ativoRepository.FindAll();
                return Ok(classes);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao acessar o banco de dados");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                Ativo ativo = await _ativoRepository.FindByID(id);
                if(ativo is null)
                {
                    return NotFound();
                }
                return Ok(ativo);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao acessar o banco de dados");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AtivoDTO ativoDTO)
        {
            try
            {
                Ativo ativo = new Ativo(ativoDTO.Codigo, ativoDTO.Descricao, ativoDTO.ClasseId);
                await _ativoRepository.Add(ativo);
                return Ok($"{ativo.Descricao} criado");
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao acessar o banco de dados");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AtivoDTO ativoDTO)
        {
            try
            {
                Ativo ativo = await _ativoRepository.FindByID(id);
                if (ativo is null)
                {
                    return NotFound();
                }
                ativo.Codigo = ativoDTO.Codigo;
                ativo.Descricao = ativoDTO.Descricao;
                ativo.Classe.Id = ativoDTO.ClasseId;
                await _ativoRepository.Update(ativo);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro no banco de dados");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Ativo ativo = await _ativoRepository.FindByID(id);
                if(ativo is null)
                {
                    return NotFound();
                }
                await _ativoRepository.Remove(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao acessar o banco de dados");
            }
        }
    }
}
