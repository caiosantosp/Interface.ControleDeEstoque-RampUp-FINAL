namespace ControleDeEstoqueProduto.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public string Tamanho { get; set; }
        public string Marca { get; set; }
        public string Composicao { get; set; }
        public string Genero { get; set; }
        public string Cor { get; set; }

        public string UrlImagem { get; set; }
        public int TotalDeVendas { get; set; }
        public int TotalDeCompras { get; set; }

    }
}
