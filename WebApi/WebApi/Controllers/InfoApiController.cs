using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Aplication.ViewModel;
using WebApi.Domain.DTOs;
using WebApi.Domain.Model;
using WebApi.Infrastructure.Repositorys;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/infoApi")]
    public class InfoApiController : ControllerBase
    {
        private readonly IinfoApiRepository _infoApiRepository;
        private readonly ILogger<InfoApiController> _logger;
        private readonly IMapper _mapper;

        public InfoApiController(IinfoApiRepository infoApiRepository, ILogger<InfoApiController> logger, IMapper mapper)
        {
            _infoApiRepository = infoApiRepository ?? throw new ArgumentNullException(nameof(infoApiRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [Authorize]
        [HttpPost]
        public IActionResult InsertPhoto([FromForm]InfoApiViewModel infApiView)
        {

            var filePath = Path.Combine("Storage", infApiView.Photo.FileName);

            using Stream fileStream = new FileStream(filePath, FileMode.Create);
            infApiView.Photo.CopyTo(fileStream);

            var infoApi = new InfoApi(infApiView.Name, infApiView.Age, filePath);

            _infoApiRepository.InsertPhoto(infoApi);

            return Ok();
        }

        [Authorize]
        [HttpPost]
        [Route("{id}/download")]
        public IActionResult DownloadPhoto(int id)
        {
            var infoApi = _infoApiRepository.Get(id);

            if (infoApi == null)
            {
                return NotFound();
            }

            try
            {
                var dataByte = System.IO.File.ReadAllBytes(infoApi.photo);

                return File(dataByte, "image/");
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Erro ao ler a imagem: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult Get(int pageNumber, int pageQuantity)
        {
            try
            {
                var infoApiss = _infoApiRepository.Get(pageNumber, pageQuantity);

                return Ok(infoApiss);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao exebir dados");
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Search(int id)
        {
            try
            {
                var idUsuario = _infoApiRepository.Get(id);

                var infoApiDTOS = _mapper.Map<InfoApiDTO>(idUsuario);

                return Ok(infoApiDTOS);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao exebir dados");
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var infApi = _infoApiRepository.Get(id);

                if (infApi == null)
                {
                    return Ok(new {messagem = $"Esse id {id} não existe na nossa base de dados!"});
                }

                _infoApiRepository.Delete(infApi);

                return Ok(new { message = "Item deletado com sucesso!" });
            }
            catch (Exception ex) 
            {
                return StatusCode(500, $"Erro ao deletar o funcionário: {ex.Message}");
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromForm] InfoApiViewModel infApiView)
        {
            try
            {
                var existeImg = _infoApiRepository.Get(id);

                if (existeImg == null)
                {
                    return Ok(new {message = $"Esse item {id} não existe na nossa base de dados"});
                }

                existeImg.name = infApiView.Name;
                existeImg.age = infApiView.Age;

                if (infApiView.Photo != null)
                {
                    if (!string.IsNullOrEmpty(existeImg.photo))
                    {
                        System.IO.File.Delete(existeImg.photo);
                    }

                    var filePath = Path.Combine("Storage", infApiView.Photo.FileName);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        infApiView.Photo.CopyTo(fileStream);
                    }

                    existeImg.photo = filePath;
                }

                _infoApiRepository.Update(existeImg);

                return Ok(new {message = "Item atualizado com sucesso!"});
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar os dados: {ex.Message}");
            }
        }
    }
}
