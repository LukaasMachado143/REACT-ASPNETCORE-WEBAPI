using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProAtividade.Domain.Entities;

namespace ProAtividade.Domain.Interfaces.Repositories
{
    public interface IAtividadeRepo : IGeralRepo
    {
        Task<AtividadeModel[]> PegarTodasAsync();
        Task<AtividadeModel> PegarPorIdAsync(int id);
        Task<AtividadeModel> PegarPorTituloAsync(string? titulo);


    }
}