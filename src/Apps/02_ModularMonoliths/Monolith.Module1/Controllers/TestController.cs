using Microsoft.AspNetCore.Mvc;

namespace Monolith.Module1.Controllers
{
    [Route("[module]/[controller]")]
    internal class TestController : Controller
    {
        [HttpGet]
        public ActionResult<string> Index()
        {
            const string foo = "Hello World from TestController in Module 2";
            return foo;
        }
    }
}