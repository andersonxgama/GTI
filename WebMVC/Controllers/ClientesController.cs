using System.Threading.Tasks;
using System.Web.Mvc;
using WebMVC.Services;

namespace WebMVC.Controllers
{
    public class ClientesController : Controller
    {
        private readonly ClientesApiService _apiService;

        public ClientesController()
        {
            _apiService = new ClientesApiService();
        }

        // GET: Clientes
        public async Task<ActionResult> Index()
        {
            try
            {
                var clientes = await _apiService.GetClientesAsync();
                return View(clientes);
            }
            catch (System.Exception ex)
            {
                ViewBag.Error = "Erro ao acessar a API: " + ex.Message;
                return View();
            }
        }
    }
}
