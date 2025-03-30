using Microsoft.AspNetCore.Mvc;

namespace DevExpert.Marketplace.App.Controllers;

public class ProductController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}