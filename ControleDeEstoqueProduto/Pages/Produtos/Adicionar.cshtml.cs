using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using ControleDeEstoqueProduto.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ControleDeEstoqueProduto.Pages.Produtos
{
    public class PrivacyModel : PageModel
    {
        [BindProperty]
        public Produto Produto { get; set; }

        string baseUrl = "https://controledeestoquev3-beta.herokuapp.com/";

        public async Task<IActionResult> OnPostAsync() {

            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsJsonAsync("api/Produtos/", Produto);

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
