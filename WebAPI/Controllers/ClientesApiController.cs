using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using WebAPI.Models;
using WebAPI.Data;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/clientes")]
    public class ClientesApiController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public ClientesApiController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet, Route("")]
        public IEnumerable<Cliente> Get()
        {
            return _context.Clientes
                .Include(c => c.Endereco)
                .ToList();
        }

        [HttpGet, Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var cliente = _context.Clientes
                .Include(c => c.Endereco)
                .FirstOrDefault(c => c.Id == id);

            if (cliente == null) return NotFound();
            return Ok(cliente);
        }

        [HttpPost, Route("")]
        public IHttpActionResult Post(Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(
                    string.Join(" | ",
                        ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage)
                    )
                );
            }

            if (cliente.Endereco != null)
            {
                cliente.Endereco.Cliente = cliente;
            }

            _context.Clientes.Add(cliente);
            _context.SaveChanges();

            return Ok(cliente);
        }

        [HttpPut, Route("{id:int}")]
        public IHttpActionResult Put(int id, Cliente cliente)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != cliente.Id) return BadRequest();

            var existing = _context.Clientes
                .Include(c => c.Endereco)
                .FirstOrDefault(c => c.Id == id);

            if (existing == null) return NotFound();

            // CLIENTE
            existing.Nome = cliente.Nome;
            existing.CPF = cliente.CPF;
            existing.RG = cliente.RG;
            existing.DataExpedicao = cliente.DataExpedicao;
            existing.OrgaoExpedicao = cliente.OrgaoExpedicao;
            existing.UFExpedicao = cliente.UFExpedicao;
            existing.DataNascimento = cliente.DataNascimento;
            existing.Sexo = cliente.Sexo;
            existing.EstadoCivil = cliente.EstadoCivil;

            // ENDEREÇO
            if (existing.Endereco == null)
            {
                existing.Endereco = cliente.Endereco;
            }
            else
            {
                existing.Endereco.CEP = cliente.Endereco.CEP;
                existing.Endereco.Logradouro = cliente.Endereco.Logradouro;
                existing.Endereco.Numero = cliente.Endereco.Numero;
                existing.Endereco.Complemento = cliente.Endereco.Complemento;
                existing.Endereco.Bairro = cliente.Endereco.Bairro;
                existing.Endereco.Cidade = cliente.Endereco.Cidade;
                existing.Endereco.UF = cliente.Endereco.UF;
            }

            _context.SaveChanges();
            return Ok(existing);
        }

        [HttpDelete, Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            var cliente = _context.Clientes
                .Include(c => c.Endereco)
                .FirstOrDefault(c => c.Id == id);

            if (cliente == null) return NotFound();

            _context.Clientes.Remove(cliente);
            _context.SaveChanges();
            return Ok();
        }
    }
}