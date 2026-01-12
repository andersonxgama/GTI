using System;
using System.Linq;
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
            var clientes = await _apiService.GetClientesAsync();
            return View(clientes);
        }

        [HttpPost]
        public async Task<ActionResult> Create(FormCollection form)
        {
            try
            {
                var cliente = RetornarCliente(form);

                if (cliente.Endereco != null)
                    cliente.Endereco.Cliente = cliente;

                await _apiService.CreateClienteAsync(cliente);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            var clientes = await _apiService.GetClientesAsync();
            var cliente = clientes.FirstOrDefault(c => c.Id == id);

            if (cliente == null)
                return RedirectToAction("Index");

            return View(cliente);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(FormCollection form)
        {
            try
            {
                var cliente = RetornarCliente(form);
                cliente.Id = int.Parse(form["Id"]);
                cliente.Endereco.ClienteId = cliente.Id;

                await _apiService.UpdateClienteAsync(cliente);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        private Cliente RetornarCliente(FormCollection form)
        {
            DateTime dataExpedicao = DateTime.Now;
            DateTime dataNascimento = DateTime.Now;

            DateTime.TryParse(form["DataExpedicao"], out dataExpedicao);
            DateTime.TryParse(form["DataNascimento"], out dataNascimento);

            Endereco endereco = null;

            if (!string.IsNullOrWhiteSpace(form["Endereco.CEP"]))
            {
                endereco = new Endereco
                {
                    CEP = form["Endereco.CEP"],
                    Logradouro = form["Endereco.Logradouro"],
                    Numero = form["Endereco.Numero"],
                    Complemento = form["Endereco.Complemento"],
                    Bairro = form["Endereco.Bairro"],
                    Cidade = form["Endereco.Cidade"],
                    UF = form["Endereco.UF"]
                };
            }

            return new Cliente
            {
                CPF = form["CPF"],
                Nome = form["Nome"],
                RG = form["RG"],
                DataExpedicao = dataExpedicao,
                OrgaoExpedicao = form["OrgaoExpedicao"],
                UFExpedicao = form["UFExpedicao"],
                DataNascimento = dataNascimento,
                Sexo = form["Sexo"],
                EstadoCivil = form["EstadoCivil"],
                Endereco = endereco
            };
        }

    }
}