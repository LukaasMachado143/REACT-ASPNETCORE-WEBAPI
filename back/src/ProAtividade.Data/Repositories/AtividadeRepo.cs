using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAtividade.Data.Context;
using ProAtividade.Domain.Entities;
using ProAtividade.Domain.Interfaces.Repositories;

namespace ProAtividade.Data.Repositories
{
    public class AtividadeRepo : GeralRepo, IAtividadeRepo
    {
        private readonly DataContext _context;

        public AtividadeRepo(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<AtividadeModel> PegarPorIdAsync(int id)
        {
            IQueryable<AtividadeModel> query = _context.Atividades;
            query = query.AsNoTracking()
                         .OrderBy(ativ => ativ.Id);
            return await query.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<AtividadeModel> PegarPorTituloAsync(string? titulo)
        {
            IQueryable<AtividadeModel> query = _context.Atividades;
            query = query.AsNoTracking()
                         .OrderBy(ativ => ativ.Titulo);
            return await query.FirstOrDefaultAsync(a => a.Titulo == titulo);
        }

        public async Task<AtividadeModel[]> PegarTodasAsync()
        {
            IQueryable<AtividadeModel> query = _context.Atividades;
            query = query.AsNoTracking()
                         .OrderBy(ativ => ativ.Id);
            return await query.ToArrayAsync();
        }
    }
}