using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModuloAPI.Context;
using ModuloAPI.Entities;
using ModuloAPI.Repository;

namespace ModuloAPI.Services
{
    public class ContatoRepository : IContatoRepository
    {

        private readonly AgendaContext _context;

        public ContatoRepository(AgendaContext context)
        {
            _context = context;
        }

        public async Task AtualizarContato(Contato contato)
        {
            _context.Contatos.Update(contato);
            await _context.SaveChangesAsync();
        }

        public async Task CriarContato(Contato contato)
        {
            _context.Contatos.Add(contato);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarContato(Contato contato)
        {
            _context.Contatos.Remove(contato);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Contato>> ListarContatos()
        {
            return await _context.Contatos.ToListAsync();
        }

        public async Task<Contato> ObterPorId(int id)
        {
            return await _context.Contatos.FindAsync(id);
        }

        public async Task<List<Contato>> ObterPorNome(string nome)
        {
           return await _context.Contatos.Where(c => c.Nome.Contains(nome)).ToListAsync();
        }
    }
}