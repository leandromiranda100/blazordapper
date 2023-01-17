using BlazorDapperProdutos.Data;

namespace BlazorDapperProdutos.Services
{
    public interface IProdutoService
    {
        Task<int> Create(Produto produto);
        Task<int> Update(Produto produto);
        Task<int> Delete(int id);
        Task<Produto> GetById(int id);
        Task<List<Produto>> ListAll();
    }
}
