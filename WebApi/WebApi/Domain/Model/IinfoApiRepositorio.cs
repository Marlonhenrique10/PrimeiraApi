using WebApi.Domain.DTOs;

namespace WebApi.Domain.Model
{
    public interface IinfoApiRepositorio
    {
        void Add(InfoApi infoApi); // Método do funcionario pode add

        void Delete(InfoApi infoApi); // Método para deletar a informação

        void Update(InfoApi infoApi); // Método para atualizar a informação

        List<InfoApiDTO> Get(int pageNumber, int pageQuantity); // Método do funcionario pode obter os valores

        InfoApi? Get(int id); // Retornando apenas 1 usuário
    }
}
