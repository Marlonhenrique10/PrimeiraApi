using WebApi.Model;

namespace WebApi.Infraestrutura
{
    public class InfoApiRepositorio : IinfoApiRepositorio
    {
        private readonly ConnectionContext _context = new ConnectionContext();

        public void Add(InfoApi infoApi)
        {
            _context.infoApis.Add(infoApi); // Add valor a minha tabela
            _context.SaveChanges(); // Salvando a informação
        }

        public List<InfoApi> Get(int pageNumber, int pageQuantity)
        {
            return _context.infoApis.Skip(pageNumber * pageQuantity).Take(pageQuantity).ToList(); // Retornando um array com todas as informações do db
        }

        public InfoApi? Get(int id)
        {
            return _context.infoApis.Find(id); // Retornando o usuário que ele encontrar
        }
    }
}
