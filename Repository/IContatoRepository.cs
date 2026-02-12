using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModuloAPI.Entities;

namespace ModuloAPI.Repository
{
    public interface IContatoRepository
    {
        Task<List<Contato>> ListarContatos();
        Task<List<Contato>> ObterPorNome(string nome);
        Task<Contato> ObterPorId(int id);
        Task CriarContato(Contato contato);
        Task AtualizarContato(Contato contato);
        Task DeletarContato(Contato contato);
    }
}