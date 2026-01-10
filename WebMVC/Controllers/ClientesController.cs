using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebMVC.Models;
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

        public async Task<ActionResult> Index()
        {
            try
            {
                var clientes = await _apiService.GetClientesAsync();
                return View(clientes);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Erro ao acessar a API: " + ex.Message;
                return View();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(FormCollection form)
        {
            try
            {
                // Criando cliente manualmente a partir do formulário
                var cliente = new Cliente
                {
                    CPF = form["CPF"]?.Trim(),
                    Nome = form["Nome"]?.Trim(),
                    RG = form["RG"]?.Trim(),
                    DataExpedicao = DateTime.Parse(form["DataExpedicao"]),
                    OrgaoExpedicao = form["OrgaoExpedicao"]?.Trim(),
                    UFExpedicao = form["UFExpedicao"]?.Trim().ToUpper(),
                    DataNascimento = DateTime.Parse(form["DataNascimento"]),
                    Sexo = form["Sexo"]?.Trim(),
                    EstadoCivil = form["EstadoCivil"]?.Trim(),
                    Endereco = new Endereco
                    {
                        CEP = form["Endereco.CEP"]?.Trim(),
                        Logradouro = form["Endereco.Logradouro"]?.Trim(),
                        Numero = form["Endereco.Numero"]?.Trim(),
                        Complemento = form["Endereco.Complemento"]?.Trim(),
                        Bairro = form["Endereco.Bairro"]?.Trim(),
                        Cidade = form["Endereco.Cidade"]?.Trim(),
                        UF = form["Endereco.UF"]?.Trim().ToUpper()
                    }
                };

                await _apiService.CreateClienteAsync(cliente);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Erro ao criar cliente: " + ex.Message;
                var clientes = await _apiService.GetClientesAsync();
                return View("Index", clientes);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Cliente cliente)
        {
            try
            {
                if (cliente == null || cliente.Id == 0)
                {
                    ViewBag.Error = "Cliente inválido para edição.";
                    var clientes = await _apiService.GetClientesAsync();
                    return View("Index", clientes);
                }

                // Ajusta os trims igual no Create
                cliente.CPF = cliente.CPF?.Trim();
                cliente.RG = cliente.RG?.Trim();
                cliente.Nome = cliente.Nome?.Trim();
                cliente.OrgaoExpedicao = cliente.OrgaoExpedicao?.Trim();
                cliente.UFExpedicao = cliente.UFExpedicao?.Trim().ToUpper();
                cliente.EstadoCivil = cliente.EstadoCivil?.Trim();

                if (cliente.Endereco != null)
                {
                    cliente.Endereco.CEP = cliente.Endereco.CEP?.Trim();
                    cliente.Endereco.Logradouro = cliente.Endereco.Logradouro?.Trim();
                    cliente.Endereco.Numero = cliente.Endereco.Numero?.Trim();
                    cliente.Endereco.Complemento = cliente.Endereco.Complemento?.Trim();
                    cliente.Endereco.Bairro = cliente.Endereco.Bairro?.Trim();
                    cliente.Endereco.Cidade = cliente.Endereco.Cidade?.Trim();
                    cliente.Endereco.UF = cliente.Endereco.UF?.Trim().ToUpper();
                }

                await _apiService.UpdateClienteAsync(cliente);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Erro ao editar cliente: " + ex.Message;
                var clientes = await _apiService.GetClientesAsync();
                return View("Index", clientes);
            }
        }

    }
}
