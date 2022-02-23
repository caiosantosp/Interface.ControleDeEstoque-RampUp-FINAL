using ControleDeEstoqueProduto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;


namespace ControleDeEstoqueProduto.Pages.Produtos
{
   
    public class EditarModel : PageModel
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
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/Produtos/" + id);

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    produtos = JsonConvert.DeserializeObject<Produto>(result);
                    
                }
                else
                {
                    return RedirectToPage("../Error");
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PutAsJsonAsync("/api/Produtos/" + produtos.Id, produtos);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("/Produtos/Estoque");
                }
                else
                {
                    return Page();
                }

            }
        }
    }
}
