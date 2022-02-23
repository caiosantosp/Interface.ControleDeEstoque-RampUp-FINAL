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
    public class DeletarModel : PageModel
    {
        
            [BindProperty]
            public Produto produtos { get; set; }

            string baseUrl = "https://apicontroledeestoquev3beta.herokuapp.com/";
            public async Task<IActionResult> OnGetAsync(int? id)
            {
                if (id == null)
                {
                return RedirectToPage("../Error");
            }

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
                }

                return Page();
            }

            public async Task<IActionResult> OnPostAsync(int? id)
            {
                if (id == null)
                {
                    return RedirectToPage("../Error");
                }


                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.DeleteAsync("api/Produtos/" + produtos.Id);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToPage("./Estoque");
                    }
                    else
                    {
                        return RedirectToPage("../Error");
                    }
                }
            }
        
    }
}
