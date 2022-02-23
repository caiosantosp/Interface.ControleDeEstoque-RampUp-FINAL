using ControleDeEstoqueProduto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ControleDeEstoqueProduto.Pages.Produtos
{
    public class DetalheModel : PageModel
    {
        [BindProperty]
        public Produto produtos { get; set; }

        string baseUrl = "https://controledeestoquev3-beta.herokuapp.com/";

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/Produtos/"+id);
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    produtos = JsonConvert.DeserializeObject<Produto>(result);
                    return Page();
                }
                else
                {
                    return RedirectToPage("../Error");
                }
            }
        }
    }
}
