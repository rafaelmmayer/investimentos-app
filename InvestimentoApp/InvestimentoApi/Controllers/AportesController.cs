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
    public class AportesController : ControllerBase
    {
        private readonly IRepository<Aporte> _aporteRepository;

        public AportesController(IRepository<Aporte> ativoRepository)
        {
            _aporteRepository = ativoRepository;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                IEnumerable<Aporte> classes = await _aporteRepository.FindAll();
                return Ok(classes);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro no banco de dados");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                Aporte aporte = await _aporteRepository.FindByID(id);
                if(aporte is null)
                {
                    return NotFound();
                }
                return Ok(aporte);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro no banco de dados");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AporteDTO aporteDTO)
        {
            try
            {
                Aporte aporte = new Aporte(aporteDTO.Quantidade, aporteDTO.PrecoEnvio, aporteDTO.AtivoId);
                await _aporteRepository.Add(aporte);
                return Ok($"Aporte criado");
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro no banco de dados");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AporteDTO aporteDTO)
        {
            try
            {
                Aporte aporte = await _aporteRepository.FindByID(id);
                if (aporte is null)
                {
                    return NotFound();
                }
                aporte.Quantidade = aporteDTO.Quantidade;
                aporte.PrecoEnvio = aporteDTO.PrecoEnvio;
                aporte.Ativo.Id = aporteDTO.AtivoId;
                await _aporteRepository.Update(aporte);
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
                Aporte aporte = await _aporteRepository.FindByID(id);
                if(aporte is null)
                {
                    return NotFound();
                }
                await _aporteRepository.Remove(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro no banco de dados");
            }
        }
    }
}
