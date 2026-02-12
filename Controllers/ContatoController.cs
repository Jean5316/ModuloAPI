using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ModuloAPI.Context;
using ModuloAPI.DTOs;
using ModuloAPI.Entities;

namespace ModuloAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContatoController : ControllerBase
    {
        //Conexão com banco 
        private readonly AgendaContext _context;
        public ContatoController(AgendaContext context)//Construtor do context conexão com banco de dados via injeção de dependencia
        {
            _context = context;
        }

        //ENDPOINTS
        [HttpGet("ListarContatos")]
        public ActionResult ListContatos()
        {
            var contatos = _context.Contatos.ToList();
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
        public ActionResult CriarContato(ContatoCriar dto)//CREATE
        {
            var contato = new Contato
            {
                Nome = dto.Nome,
                Telefone = dto.Telefone,
                Ativo = dto.Ativo
            };

            _context.Add(contato);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterPorId), new {id = contato.Id}, contato);//retornar id e informções de contato criado
        }

        [HttpGet("ObterPorId/{id}")]
        public ActionResult ObterPorId(int id)//SELECT
        {
            var contato = _context.Contatos.Find(id);
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
        public ActionResult ObterPorNome(string nome)//SELECT
        {
            var contatos = _context.Contatos.Where(x => x.Nome.Contains(nome));//usando LINQ para buscar contatos
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
        public IActionResult AtualizarContato(int id, ContatoCriar dto)//UPDATE
        {
            var contatoBanco = _context.Contatos.Find(id);
            if(contatoBanco == null)
            {
                return NotFound();
            }
            contatoBanco.Nome = dto.Nome;
            contatoBanco.Telefone = dto.Telefone;
            contatoBanco.Ativo = dto.Ativo;

            _context.Contatos.Update(contatoBanco);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("DeletarContato/{id}")]
        public IActionResult DeletarContato(int id)//DELETE
        {
            var contatoBanco = _context.Contatos.Find(id);
            if(contatoBanco == null)
            {
                return NotFound();
            }

            _context.Contatos.Remove(contatoBanco);
            _context.SaveChanges();
            return NoContent();
        }

        
        
        
    }
}