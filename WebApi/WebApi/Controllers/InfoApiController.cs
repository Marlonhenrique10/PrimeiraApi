using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Aplication.ViewModel;
using WebApi.Domain.DTOs;
using WebApi.Domain.Model;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/infoApi")] // Criando a minha rota padrão
    public class InfoApiController : ControllerBase
    {
        private readonly IinfoApiRepositorio _infoApiRepositorio;
        private readonly ILogger<InfoApiController> _logger;
        private readonly IMapper _mapper;

        public InfoApiController(IinfoApiRepositorio infoApiRepositorio, ILogger<InfoApiController> logger, IMapper mapper)
        {
            _infoApiRepositorio = infoApiRepositorio ?? throw new ArgumentNullException(nameof(infoApiRepositorio));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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

        [HttpGet]
        [Route("{id}")]
        public IActionResult Search(int id)
        {
            try
            {
                var idUsuario = _infoApiRepositorio.Get(id);

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
                var infApi = _infoApiRepositorio.Get(id);

                if (infApi == null)
                {
                    return Ok(new {messagem = $"Esse id {id} não existe na nossa base de dados!"});
                }

                _infoApiRepositorio.Delete(infApi);

                return Ok(new { message = "Item deletado com sucesso!" });
            }
            catch (Exception ex) 
            {
                return StatusCode(500, $"Erro ao deletar o funcionário: {ex.Message}");
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult AtualizarDados(int id, [FromForm] InfoApiViewModel infApiView)
        {
            try
            {
                var existeImg = _infoApiRepositorio.Get(id);

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

                _infoApiRepositorio.Update(existeImg);

                return Ok(new {message = "Item atualizado com sucesso!"});
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar os dados: {ex.Message}");
            }
        }
    }
}
