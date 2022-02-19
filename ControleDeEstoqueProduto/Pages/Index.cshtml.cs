using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleDeEstoqueProduto.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace ControleDeEstoqueProduto.Pages
{
    public class IndexModel : PageModel
    {
        public List<Produto> Produtos { get; set; }

        public string TotalEstoques { get; set; } 

        public string TotalVendas { get; set; }

        string UrlGlobal = "https://controldeestoquev2.herokuapp.com/";

        public async Task OnGetAsync()
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(UrlGlobal);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                HttpResponseMessage response = await client.GetAsync(UrlGlobal+"api/Produtos/");
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    Produtos = JsonConvert.DeserializeObject<List<Produto>>(result);
                }
                else
                {
                    Console.WriteLine("Erro");
                }

                
            }
            await GetTotalEstoque();
            await GetTotalVendas();
        } 
        public async Task GetTotalEstoque()
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(UrlGlobal);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(UrlGlobal + "api/Produtos/QuantidadeTotalDoEstoque/");


                var resultTotal = response.Content.ReadAsStringAsync().Result;
                TotalEstoques = resultTotal;
            }

        }
        public async Task GetTotalVendas()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(UrlGlobal);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/Produtos/TotalDeVendasDosProdutos");

                var result = response.Content.ReadAsStringAsync().Result;
                TotalVendas = result;
            }
        }
    }
}
