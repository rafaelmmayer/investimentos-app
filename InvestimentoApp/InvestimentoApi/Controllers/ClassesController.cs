using InvestimentoApi.Repositories;
using InvestimentoApi.DTOs;
using InvestimentoApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvestimentoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassesController : Controller
    {
        private readonly IRepository<Classe> _classeRepository;

        public ClassesController(IRepository<Classe> classeRepository)
        {
            _classeRepository = classeRepository;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {            
            try
            {
                IEnumerable<Classe> classes = await _classeRepository.FindAll();
                return Ok(classes);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao acessar o banco de dados");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {            
            try
            {
                Classe classe = await _classeRepository.FindByID(id);
                if (classe == null)
                {
                    return NotFound();
                }
                return Ok(classe);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao acessar o banco de dados");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClasseDTO classeDTO)
        {
            try
            {
                Classe classe = new Classe() { Descricao = classeDTO.Descricao };
                await _classeRepository.Add(classe);
                return Ok($"{classe.Descricao} criado");
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao acessar o banco de dados");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ClasseDTO classeDTO)
        {
            try
            {
                Classe classe = await _classeRepository.FindByID(id);
                if(classe is null)
                {
                    return NotFound();
                }
                classe.Descricao = classeDTO.Descricao;
                await _classeRepository.Update(classe);
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
                var classe = await _classeRepository.FindByID(id);

                if (classe == null)
                    return NotFound();

                await _classeRepository.Remove(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao acessar o banco de dados");
            }
        }
    }
}
