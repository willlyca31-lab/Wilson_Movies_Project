
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace Wilson_Movies_Project.Controllers;

public class HelloWorldController : Controller
{
    // GET: /HelloWorld/
    // This is the default action method.
    // GET: /HelloWorld/
// This action renders the Index.cshtml view.
    public IActionResult Index()
    {
        return View();
    }

    // GET: /HelloWorld/Welcome/
    // This action method displays a welcome message.
    public string Welcome()
    {
        return "This is the Welcome action method...";
    }
}
