using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebMVC.Models;

namespace WebMVC.Services
{
    public class ClientesApiService
    {
        private readonly string baseUrl = "http://localhost:51456/api/clientes";

        public async Task<List<Cliente>> GetClientesAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("");
                if (response.IsSuccessStatusCode)
                {
                    var clientes = await response.Content.ReadAsAsync<List<Cliente>>();
                    return clientes;
                }
                else
                {
                    throw new Exception("Erro ao acessar a API: " + response.StatusCode);
                }
            }
        }

        public async Task<Cliente> CreateClienteAsync(Cliente cliente)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsJsonAsync("", cliente);

                if (response.IsSuccessStatusCode)
                {
                    // Lê o cliente criado com endereço de volta da API
                    var createdCliente = await response.Content.ReadAsAsync<Cliente>();
                    return createdCliente;
                }
                else
                {
                    throw new Exception("Erro ao criar cliente: " + response.StatusCode);
                }
            }
        }

        public async Task<Cliente> UpdateClienteAsync(Cliente cliente)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PutAsJsonAsync(cliente.Id.ToString(), cliente);

                if (response.IsSuccessStatusCode)
                {
                    var updatedCliente = await response.Content.ReadAsAsync<Cliente>();
                    return updatedCliente;
                }
                else
                {
                    throw new Exception("Erro ao atualizar cliente: " + response.StatusCode);
                }
            }
        }

    }
}