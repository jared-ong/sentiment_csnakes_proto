namespace SentimentWebApp.Models
{
    public class SentimentModel
    {
        public string InputText { get; set; } = string.Empty;
        public string Sentiment { get; set; } = string.Empty;
        public double Confidence { get; set; } = 0.0;
    }
}