using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Aplication.ViewModel;
using WebApi.Domain.Model;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/infoApi")] // Criando a minha rota padrão
    public class InfoApiController : ControllerBase
    {
        private readonly IinfoApiRepositorio _infoApiRepositorio;
        private readonly ILogger<InfoApiController> _logger;

        public InfoApiController(IinfoApiRepositorio infoApiRepositorio, ILogger<InfoApiController> logger)
        {
            _infoApiRepositorio = infoApiRepositorio ?? throw new ArgumentNullException(nameof(infoApiRepositorio));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add([FromForm]InfoApiViewModel infApiView)
        {

            var filePath = Path.Combine("Storage", infApiView.Photo.FileName); // Caminho do arquivo da foto

            // Estou salvando o arquivo dentro da memória e depois colocando na pasta que eu quero
            using Stream fileStream = new FileStream(filePath, FileMode.Create);
            infApiView.Photo.CopyTo(fileStream);

            var infoApi = new InfoApi(infApiView.Name, infApiView.Age, filePath);

            _infoApiRepositorio.Add(infoApi);

            return Ok();
        }

        [Authorize]
        [HttpPost]
        [Route("{id}/download")]
        public IActionResult DownloadPhoto(int id)
        {
            var infoApi = _infoApiRepositorio.Get(id); // Pegando os dados da imagem com o id do usuário

            if (infoApi == null)
            {
                return NotFound();
            }

            try
            {
                var dataByte = System.IO.File.ReadAllBytes(infoApi.photo);

                return File(dataByte, "image/"); // Retornando para o front-end a minha imagem e passando o tipo que estarei passando
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
                var infoApiss = _infoApiRepositorio.Get(pageNumber, pageQuantity);

                return Ok(infoApiss);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao exebir dados");
            }
        }
    }
}
