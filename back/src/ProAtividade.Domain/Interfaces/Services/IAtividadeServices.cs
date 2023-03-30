using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProAtividade.Domain.Entities;

namespace ProAtividade.Domain.Interfaces.Services
{
    public interface IAtividadeServices
    {
        Task<AtividadeModel> AdicionarAtividade(AtividadeModel model);
        Task<AtividadeModel> AtualizarAtividade(AtividadeModel model);

        Task<bool> DeletarAtividade(int AtividadeId); 
        Task<bool> ConcluirAtividade(AtividadeModel model); 

        Task<AtividadeModel[]> PegarTodasAtividadesAsync();
        Task<AtividadeModel> PegarAtividadePorIdAsync(int AtividadeId);
    }
}