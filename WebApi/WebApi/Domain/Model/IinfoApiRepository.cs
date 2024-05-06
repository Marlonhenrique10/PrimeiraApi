using WebApi.Domain.DTOs;

namespace WebApi.Domain.Model
{
    public interface IinfoApiRepository
    {
        void InsertPhoto(InfoApi infoApi);

        void Delete(InfoApi infoApi);

        void Update(InfoApi infoApi);

        List<InfoApiDTO> Get(int pageNumber, int pageQuantity);

        InfoApi? Get(int id);
    }
}
