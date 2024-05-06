using WebApi.Domain.DTOs;
using WebApi.Domain.Model;
using WebApi.infrastructure;

namespace WebApi.Infrastructure.Repositorys
{
    public class InfoApiRepository : IinfoApiRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();

        public void InsertPhoto(InfoApi infoApi)
        {
            _context.infoApis.Add(infoApi);
            _context.SaveChanges();
        }

        public List<InfoApiDTO> Get(int pageNumber, int pageQuantity)
        {
            return _context.infoApis.Skip(pageNumber * pageQuantity)
                .Take(pageQuantity)
                .Select(b => 
                new InfoApiDTO()
                {
                    Id = b.id,
                    Name = b.name,
                    Photo = b.photo
                })
                .ToList();
        }

        public InfoApi? Get(int id)
        {
            return _context.infoApis.Find(id);
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
