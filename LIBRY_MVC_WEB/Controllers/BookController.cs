using LIBRY_MVC_WEB.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Mime;
using System.Text;

namespace LIBRY_MVC_WEB.Controllers
{
    public class BookController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly HttpClient httpClient;

        public BookController(IHttpClientFactory httpClientFactory) {
           this.httpClientFactory = httpClientFactory;
            httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri("https://localhost:7172/api/");
            
        }
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] string? sortby = null, bool isacsending = true,string? filteron = null,  string? filterquery = null)
        {
			List<BookDTO> books = new List<BookDTO>();
			try
			{
				var queryParams = new Dictionary<string, string>
				{
					{ "sortby", sortby },
					{ "isacsending", isacsending.ToString() },
					{ "filteron", filteron },
					{ "filterquery", filterquery },
					{ "pageNumber", "1" },
					{ "pageSize", "100" }
				};

				var query = string.Join("&", queryParams
					.Where(kvp => kvp.Value != null)
					.Select(kvp => $"{kvp.Key}={kvp.Value}"));

				var response = await httpClient.GetAsync($"Book/GetAll?{query}");

				response.EnsureSuccessStatusCode(); // Ensure we have a successful response

				var responseContent = await response.Content.ReadAsStringAsync();
				books = JsonConvert.DeserializeObject<List<BookDTO>>(responseContent);
			}
			catch (Exception ex)
			{
				// Log the exception
				Console.WriteLine(ex.Message);
			}

			return View(books);
		}
		[HttpPost]
		public async Task<IActionResult>AddBook(AddBookDTO addBook)
		{
			try
			{
				var client = httpClientFactory.CreateClient();
				var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7172/api/Book/AddBook")
				{
					Content = new StringContent(JsonConvert.SerializeObject(addBook), Encoding.UTF8, "application/json")
				};

				var response = await client.SendAsync(request);
				response.EnsureSuccessStatusCode();

				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return View("Error");
			}
		}

    }
}
