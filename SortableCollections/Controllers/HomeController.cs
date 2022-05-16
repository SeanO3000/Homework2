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
            var contacts = new[]
            {
                new Contact{Id = 1, Name="dave", City="Seattle", State="WA", Phone="123"},
                new Contact{Id = 2, Name="mike", City="Spokane", State="WA", Phone="234"},
                new Contact{Id = 3, Name="lisa", City="San Jose", State="CA", Phone="345"},
                new Contact{Id = 4, Name="cathy", City="Dallas", State="TX", Phone="456"},
            };

            ViewData["IdSortParam"] = sortOrder == "Id";
            ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParam"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (sortOrder != null)
            {
                switch (sortOrder.ToLower())
                {
                    case "id":
                        {
                            contacts = contacts.OrderByDescending(c => c.Id);
                            break;
                        }
                    case "name":
                        {
                            contacts = contacts.OrderBy(c => c.Name);
                            break;
                        }
                    case "city":
                        {
                            contacts = contacts.OrderBy(c => c.City);
                            break;
                        }
                    case "state":
                        {
                            contacts = contacts.OrderBy(c => c.State);
                            break;
                        }
                    case "phone":
                        {
                            contacts = contacts.OrderBy(c => c.Phone);
                            break;
                        }
                }
            }

            return View(await contacts.AsNotracking().ToListAsync());
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