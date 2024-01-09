using Microsoft.AspNetCore.Mvc;
using WebApi.Model;
using WebApi.ViewModel;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/infoApi")] // Criando a minha rota padrão
    public class InfoApiController : ControllerBase
    {
        private readonly IinfoApiRepositorio _infoApiRepositorio;

        public InfoApiController(IinfoApiRepositorio infoApiRepositorio)
        {
            _infoApiRepositorio = infoApiRepositorio ?? throw new ArgumentNullException(nameof(infoApiRepositorio));
        }

        [HttpPost]
        public IActionResult Add(InfoApiViewModel infApiView)
        {
            var infoApi = new InfoApi(infApiView.Name, infApiView.Age, null);

            _infoApiRepositorio.Add(infoApi);

            return Ok();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var infoApiss = _infoApiRepositorio.Get();

            return Ok(infoApiss);
        }
    }
}
