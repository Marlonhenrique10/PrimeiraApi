using WebApi.Domain.DTOs;
using WebApi.Domain.Model;

namespace WebApi.Infraestrutura.Repositorios
{
    public class InfoApiRepositorio : IinfoApiRepositorio
    {
        private readonly ConnectionContext _context = new ConnectionContext();

        public void Add(InfoApi infoApi)
        {
            _context.infoApis.Add(infoApi); // Add valor a minha tabela
            _context.SaveChanges(); // Salvando a informação
        }

        public List<InfoApiDTO> Get(int pageNumber, int pageQuantity)
        {
            return _context.infoApis.Skip(pageNumber * pageQuantity) // Calculando a quantidade de pagina * a quantidade de item na tabela
                .Take(pageQuantity) // Pega a quantidade de item na tabela
                .Select(b => 
                new InfoApiDTO()
                {
                    Id = b.id,
                    Name = b.name,
                    Photo = b.photo
                })
                .ToList(); // Retornando um array com todas as informações do db
        }

        public InfoApi? Get(int id)
        {
            return _context.infoApis.Find(id); // Retornando o usuário que ele encontrar
        }

        public void Delete(InfoApi infoApi)
        {
            _context.infoApis.Remove(infoApi);
            _context.SaveChanges();
        }

        public void Update(InfoApi infoApi)
        {
            _context.infoApis.Update(infoApi);
            _context.SaveChanges();
        }
    }
}
