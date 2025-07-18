using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ModuloAPI.Context;
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
        [HttpGet]
        public IActionResult ListContatos()
        {
            List<Contato> ListaContatos = new List<Contato>();
            ListaContatos = _context.Contatos.ToList();
            return Ok(ListaContatos);
        } 
        
        [HttpPost]
        public IActionResult Create(Contato contato)//CREATE
        {
            _context.Add(contato);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterPorId), new {id = contato.Id}, contato);//retornar id e informções de contato criado
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)//SELECT
        {
            var contato = _context.Contatos.Find(id);
            if (contato == null)
            {
                return NotFound();
            }
            return Ok(contato);
        }

        [HttpGet("ObterPorNome")]
        public IActionResult ObterPorNome(string nome)//SELECT
        {
            var contatos = _context.Contatos.Where(x => x.Nome.Contains(nome));//usando LINQ para buscar contatos
            if (contatos == null)
            {
                return NotFound();
            }
            return Ok(contatos);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarContato(int id, Contato contato)//UPDATE
        {
            var contatoBanco = _context.Contatos.Find(id);
            if(contatoBanco == null)
            {
                return NotFound();
            }
            contatoBanco.Nome = contato.Nome;
            contatoBanco.Telefone = contato.Telefone;
            contatoBanco.Ativo = contato.Ativo;

            _context.Contatos.Update(contatoBanco);
            _context.SaveChanges();
            return Ok(contatoBanco);
        }

        [HttpDelete]
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