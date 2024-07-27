using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WordsWebApplication.Models;
using WordsWebApplication.Services;

namespace WordsWebApplication.Controllers
{
    public class ConvertController : Controller
    {
        private readonly ILogger<ConvertController> logger;

        private readonly INumberToWordsConvertService numberToWordsConvertService;

        public ConvertController(INumberToWordsConvertService numberToWordsConvertService, ILogger<ConvertController> logger)
        {
            this.numberToWordsConvertService = numberToWordsConvertService;
            this.logger = logger;
        }

        public IActionResult Index()
        {
            logger.LogInformation("Index page visited");
            return View();
        }

        [HttpPost]
        public IActionResult Convert(string number)
        {
            logger.LogInformation("Convert method called with number: {Number}", number);
            try
            {
                string result = numberToWordsConvertService.Convert(number);
                logger.LogInformation("Conversion successful: {Result}", result);
                return Json(new { result });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error converting number to words for number: {Number}", number);
                return StatusCode(500, "Internal Server Error");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            logger.LogError("Error page visited, Request ID: {RequestId}", requestId);
            return View(new ErrorViewModel { RequestId = requestId });
        }
    }
}
