using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModuloAPI.Entities;

namespace ModuloAPI.Context
{
    public class AgendaContext : DbContext //fazer herança da classe DbContext
    {
        public AgendaContext(DbContextOptions<AgendaContext> options) : base(options)/* Construtor de conexão, 
                                                                                        passando para classe DbContext*/
        {
            
        }
        //Entidade = classe no programa e tambem tabela no banco de dados
        public DbSet<Contato> Contatos { get; set; }//propriedade que refere a entidade(classe) no caso Contato
        public DbSet<Usuario> Usuarios { get; set; }
    }
}