using SQLite;

namespace MauiAppMinhasCompras.Models
{
    public class Produto
    {

        // variavel usada para guardar o valor da descrição por trás
        string _descricao;

        [PrimaryKey, AutoIncrement]
        
        // valores do banco de dados do aplicativo
        public int Id { get; set; }
        
        // quando o usuario tentar definir descrição como nulo apareça mensagem na tela para preencher de forma correta
        // o aplicativo não permite avançar se a descrição for nula
        public string Descricao { 
            get => _descricao;
            set
            {
               if(value == null)
                {
                    throw new Exception("Por favor preencha a descrição");
                }
                _descricao = value;
            }
                }
        public double 
            Quantidade { get; set; }
        public double Preco {  get; set; }

        public double Total { get => Quantidade * Preco; }


    }
}
