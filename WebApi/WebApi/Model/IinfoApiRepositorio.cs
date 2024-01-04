namespace WebApi.Model
{
    public interface IinfoApiRepositorio
    {
        void Add(InfoApi infoApi); // Método do funcionario pode add

        List<InfoApi> Get(); // Método do funcionario pode obter os valores
    }
}
