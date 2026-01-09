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
    }
}
