using Microsoft.AspNetCore.Mvc;
using SentimentWebApp.Models;
using SentimentWebApp.PythonIntegration;

namespace SentimentWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly SentimentAnalysis _sentimentAnalysisService;

        public HomeController(SentimentAnalysis sentimentAnalysisService)
        {
            _sentimentAnalysisService = sentimentAnalysisService;
        }

        public IActionResult Index()
        {
            return View(new SentimentModel());
        }

        [HttpPost]
        public IActionResult Analyze(SentimentModel model)
        {
            if (string.IsNullOrEmpty(model.InputText))
            {
                ModelState.AddModelError("InputText", "Please enter a sentence.");
                return View("Index", model);
            }

            var (label, confidence) = _sentimentAnalysisService.PredictSentiment(model.InputText);
            model.Sentiment = label;
            model.Confidence = confidence;

            return View("Index", model);
        }
    }
}