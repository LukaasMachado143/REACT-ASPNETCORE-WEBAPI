namespace ProAtividade.Domain.Entities
{
    public class AtividadeModel
    {
        
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataConclusao { get; set; }
        public PrioridadeEnum Prioridade { get; set; }
        
        
        public AtividadeModel() 
        {
            DataCriacao = DateTime.Now;
            DataConclusao = null;
        } 
        public AtividadeModel(int id, string titulo, string descricao) : this() 
        {
            this.Id = id;
            this.Titulo = titulo;
            this.Descricao = descricao;
   
        }

        public void Concluir(){
            if (DataConclusao == null)
                DataConclusao = DateTime.Now;
            else    
                throw new Exception($"Atividade já concluída em: {DataConclusao?.ToString("dd/MM/yyyy hh:mm")}");
        }
    }
}