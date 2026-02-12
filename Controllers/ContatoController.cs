using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ModuloAPI.Context;
using ModuloAPI.DTOs;
using ModuloAPI.Entities;
using ModuloAPI.Repository;
using ModuloAPI.Services;

namespace ModuloAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]//Autenticação para acessar os endpoints
    public class ContatoController : ControllerBase
    {
        //Conexão com banco 
        private readonly IContatoRepository _repository;
        public ContatoController(IContatoRepository repository)//Construtor do context conexão com banco de dados via injeção de dependencia
        {
            _repository = repository;
        }

        //ENDPOINTS
        [HttpGet("ListarContatos")]
        public async Task<ActionResult> ListContatos()
        {
            var contatos = await _repository.ListarContatos();
            var response = contatos.Select(c => new ContatoResponse
            {
                Id = c.Id,
                Nome = c.Nome,
                Telefone = c.Telefone,
                Ativo = c.Ativo
            });
            return Ok(response);
        } 
        
        [HttpPost("CriarNovoContato")]
        public async Task<ActionResult> CriarContato(ContatoCriar dto)//CREATE
        {
            var contato = new Contato
            {
                Nome = dto.Nome,
                Telefone = dto.Telefone,
                Ativo = dto.Ativo
            };

            await  _repository.CriarContato(contato);
            return CreatedAtAction(nameof(ObterPorId), new {id = contato.Id}, contato);//retornar id e informções de contato criado
        }

        [HttpGet("ObterPorId/{id}")]
        public async Task<ActionResult> ObterPorId(int id)//SELECT
        {
            var contato = await _repository.ObterPorId(id);
            if (contato == null)
            {
                return NotFound();
            }

            var response = new ContatoResponse
            {
                Id = contato.Id,
                Nome = contato.Nome,
                Telefone = contato.Telefone,
                Ativo = contato.Ativo
            };
            return Ok(response);
        }

        [HttpGet("ObterPorNome")]
        public async Task<ActionResult> ObterPorNome(string nome)//SELECT
        {
            var contatos = await _repository.ObterPorNome(nome);
            if (contatos == null)
            {
                return NotFound();
            }
            
            var response = contatos.Select(c => new ContatoResponse
            {
                Id = c.Id,
                Nome = c.Nome,
                Telefone = c.Telefone,
                Ativo = c.Ativo
            });
            return Ok(response);
        }

        [HttpPut("AtualizarContato/{id}")]
        public async Task<IActionResult> AtualizarContato(int id, ContatoCriar dto)//UPDATE
        {
            var contatoBanco = await _repository.ObterPorId(id);
            if(contatoBanco == null)
            {
                return NotFound();
            }
            contatoBanco.Nome = dto.Nome;
            contatoBanco.Telefone = dto.Telefone;
            contatoBanco.Ativo = dto.Ativo;

            await _repository.AtualizarContato(contatoBanco);
            return NoContent();
        }

        [HttpDelete("DeletarContato/{id}")]
        public async Task<IActionResult> DeletarContato(int id)//DELETE
        {
            var contatoBanco = await _repository.ObterPorId(id);
            if(contatoBanco == null)
            {
                return NotFound();
            }

            await _repository.DeletarContato(contatoBanco);
            return NoContent();
        }

        
        
        
    }
}