﻿namespace BlazorDapperProdutos.Data
{
    public class Produto
    {
        public int produtoid { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public string imagem { get; set; }
        public decimal preco { get; set; }
        public int estoque { get; set; }
    }
}
