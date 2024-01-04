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

        public List<InfoApi> Get()
        {
            return _context.infoApis.ToList();
        }
    }
}
