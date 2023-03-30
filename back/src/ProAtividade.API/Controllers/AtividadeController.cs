using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProAtividade.Data.Context;
using ProAtividade.Domain.Entities;
using ProAtividade.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace ProAtividade.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AtividadeController : ControllerBase
    {
        private readonly IAtividadeServices _atividadeServices;

        public AtividadeController(IAtividadeServices atividadeServices)
        {
            _atividadeServices = atividadeServices;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var atividades = await _atividadeServices.PegarTodasAtividadesAsync();
                if(atividades == null) return NoContent();
                
                return Ok(atividades);
            }
            catch (System.Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError,
                                    $"Erro ao tentar recuperar as atividades. Erro: {ex.Message}");
            }   

        }

        [HttpGet("{id}")]
        public async Task <IActionResult> Get(int id)
        {
            try
            {
                var atividade = await _atividadeServices.PegarAtividadePorIdAsync(id);
                if(atividade == null) return NoContent();
                
                return Ok(atividade);
            }
            catch (System.Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError,$"Erro ao tentar recuperar as atividade com ID: ${id}. Erro: {ex.Message}");
            }   
        } 

        [HttpPost]
        public async Task <IActionResult> Post(AtividadeModel dadosNovaAtividade)
        {

            try
            {
                var atividadeAdicionada = await _atividadeServices.AdicionarAtividade(dadosNovaAtividade);
                if(atividadeAdicionada == null) return StatusCode(StatusCodes.Status501NotImplemented,$"Erro ao inserir nova atividade.");
                return Ok(atividadeAdicionada);
            }
            catch (System.Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError,$"Erro ao inserir nova atividade. Erro: {ex.Message}");
            }   
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, AtividadeModel dadosAtividade)
        {
            try
            {
                if(dadosAtividade.Id != id) return StatusCode(StatusCodes.Status409Conflict,"Você está tentando alterar a atividade errada");
                
                var atividadeAtualizada = await _atividadeServices.AtualizarAtividade(dadosAtividade);

                if(atividadeAtualizada == null) return NoContent();

                return Ok(atividadeAtualizada);
            }
            catch (System.Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError,$"Erro ao atualizar a atividade com ID: ${id}. Erro: {ex.Message}");
            }   

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id){

            try
            {
                var atividadeSelecionada = await _atividadeServices.PegarAtividadePorIdAsync(id);
                if(atividadeSelecionada == null) return NoContent();
                if(id != atividadeSelecionada.Id) return StatusCode(StatusCodes.Status406NotAcceptable,"Atividade inexistente, impossível de deletar");
                
                if(await _atividadeServices.DeletarAtividade(id))
                {
                    return Ok(new {message = "Atividade Deletada"});
                }
                else
                {
                    return BadRequest("Ocorreu algum problema nào específico ao tentar deletar a atividade.");
                }

                
            }
            catch (System.Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError,$"Erro ao deletar a atividade com ID: ${id}. Erro: {ex.Message}");
            }
        }
    }
}