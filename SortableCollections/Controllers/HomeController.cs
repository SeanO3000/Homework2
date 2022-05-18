using Microsoft.AspNetCore.Mvc;
using SortableCollections.Models;
using System.Diagnostics;
using System.Linq;

namespace SortableCollections.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["Id"] = string.IsNullOrEmpty(sortOrder) ? "id" : "";
            ViewData["Name"] = string.IsNullOrEmpty(sortOrder) ? "name" : "";
            ViewData["City"] = string.IsNullOrEmpty(sortOrder) ? "city" : "";
            ViewData["State"] = string.IsNullOrEmpty(sortOrder) ? "state" : "";
            ViewData["Phone"] = string.IsNullOrEmpty(sortOrder) ? "phone" : "";

            var contacts = new[]
            {
                new Contact{Id = 1, Name="dave", City="Seattle", State="WA", Phone="123"},
                new Contact{Id = 2, Name="mike", City="Spokane", State="WA", Phone="234"},
                new Contact{Id = 3, Name="lisa", City="San Jose", State="CA", Phone="345"},
                new Contact{Id = 4, Name="cathy", City="Dallas", State="TX", Phone="456"},
            };

            if (sortOrder != null)
            {
                switch (sortOrder.ToLower())
                {
                    case "id":
                        {
                            contacts = contacts.OrderByDescending(c => c.Id).ToArray();
                            break;
                        }
                    case "name":
                        {
                            contacts = contacts.OrderBy(c => c.Name).ToArray();
                            break;
                        }
                    case "city":
                        {
                            contacts = contacts.OrderBy(c => c.City).ToArray();
                            break;
                        }
                    case "state":
                        {
                            contacts = contacts.OrderBy(c => c.State).ToArray();
                            break;
                        }
                    case "phone":
                        {
                            contacts = contacts.OrderBy(c => c.Phone).ToArray();
                            break;
                        }
                }
            }

            return View(contacts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}