using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProAtividade.Domain.Entities;
using ProAtividade.Domain.Interfaces.Repositories;
using ProAtividade.Domain.Interfaces.Services;

namespace ProAtividade.Domain.Services
{
    public class AtividadeService : IAtividadeServices
    {
        private readonly IAtividadeRepo _atividadeRepo;

        public AtividadeService(IAtividadeRepo atividadeRepo)
        {
            _atividadeRepo = atividadeRepo;
        }

        public async Task<AtividadeModel?> AdicionarAtividade(AtividadeModel model)
        {
            if (await _atividadeRepo.PegarPorTituloAsync(model.Titulo) != null)
            {
                throw new Exception("Já existe uma atividade com esse título !");
            }
            else if (await _atividadeRepo.PegarPorIdAsync(model.Id) == null)
            {
                _atividadeRepo.Adicionar(model);
                if (await _atividadeRepo.SalvarMudancasAsync())
                    return model;
            }

            return null;
        }

        public async Task<AtividadeModel> AtualizarAtividade(AtividadeModel model)
        {
            if (model.DataConclusao != null)
            {
                throw new Exception("Não pode alterar essa atividade, pois está concluída!");
            }
            else if (await _atividadeRepo.PegarPorIdAsync(model.Id) != null)
            {
                _atividadeRepo.Atualizar(model);
                if (await _atividadeRepo.SalvarMudancasAsync())
                    return model;
            }

            return null;

        }

        public async Task<bool> ConcluirAtividade(AtividadeModel model)
        {
            if (model != null) 
            {
                model.Concluir();
                _atividadeRepo.Atualizar<AtividadeModel>(model);
                return await _atividadeRepo.SalvarMudancasAsync();
            }
            return false;
        }

        public async Task<bool> DeletarAtividade(int atividadeId)
        {
            var atividadeEncontrada = await _atividadeRepo.PegarPorIdAsync(atividadeId);
            if (atividadeEncontrada == null)
            {
                throw new Exception("Não pode excluir uma atividade inexistente");
            }

            _atividadeRepo.Deletar(atividadeEncontrada);
            return await _atividadeRepo.SalvarMudancasAsync();
        }

        public async Task<AtividadeModel> PegarAtividadePorIdAsync(int atividadeId)
        {
            try
            {
                var atividadeEncontrada = await _atividadeRepo.PegarPorIdAsync(atividadeId);
                if (atividadeEncontrada == null) return null;

                return atividadeEncontrada;
            }
            catch (System.Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<AtividadeModel[]> PegarTodasAtividadesAsync()
        {
            try
            {
                var atividades = await _atividadeRepo.PegarTodasAsync();
                if (atividades == null) return null;

                return atividades;
            }
            catch (System.Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }
    }
}