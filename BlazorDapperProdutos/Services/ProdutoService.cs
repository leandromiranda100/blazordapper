using BlazorDapperProdutos.Data;
using Dapper;
using System.Data;

namespace BlazorDapperProdutos.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IDapperService _dapperDAL;

        public ProdutoService(IDapperService dapperDAL)
        {
            _dapperDAL = dapperDAL;
        }

        public Task<int> Create(Produto produto)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("nome", produto.nome, DbType.String);
            dbPara.Add("descricao", produto.descricao, DbType.String);
            dbPara.Add("imagem", produto.imagem, DbType.String);
            dbPara.Add("preco", produto.preco, DbType.Decimal);
            dbPara.Add("estoque", produto.estoque, DbType.Int32);

            var produtoid = Task.FromResult(_dapperDAL.Insert<int>("DBO.[SP_Novo_Produto]", dbPara, commandType: CommandType.StoredProcedure));

            return produtoid;
        }

        public Task<int> Delete(int id)
        {
            var deleteProduto = Task.FromResult(_dapperDAL.Execute($"DELETE [produtos]" + $" WHERE produtoid= {id}", null, commandType: CommandType.Text));

            return deleteProduto;
        }

        public Task<Produto> GetById(int id)
        {
            var produto = Task.FromResult(_dapperDAL.Get<Produto>($"SELECT * FROM [produtos]" + $" WHERE produtoid= {id}", null, commandType: CommandType.Text));

            return produto;
        }

        public Task<List<Produto>> ListAll()
        {
            var produtos = Task.FromResult(_dapperDAL.GetAll<Produto>($"SELECT * FROM [produtos]", null, commandType: CommandType.Text));

            return produtos;
        }

        public Task<int> Update(Produto produto)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("produtoid", produto.produtoid, DbType.Int32);
            dbPara.Add("nome", produto.nome, DbType.String);
            dbPara.Add("descricao", produto.descricao, DbType.String);
            dbPara.Add("imagem", produto.imagem, DbType.String);
            dbPara.Add("preco", produto.preco, DbType.Decimal);
            dbPara.Add("estoque", produto.estoque, DbType.Int32);

            var updateProduto = Task.FromResult(_dapperDAL.Update<int>("DBO.[SP_Atualiza_Produto]", dbPara, commandType: CommandType.StoredProcedure));

            return updateProduto;
        }
    }
}
