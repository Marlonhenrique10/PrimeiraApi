namespace WebApi.Model
{
    public interface IinfoApiRepositorio
    {
        void Add(InfoApi infoApi); // Método do funcionario pode add

        List<InfoApi> Get(int pageNumber, int pageQuantity); // Método do funcionario pode obter os valores

        InfoApi? Get(int id); // Retornando apenas 1 usuário
    }
}
