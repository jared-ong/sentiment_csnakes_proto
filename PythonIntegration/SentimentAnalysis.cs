using CSnakes.Runtime;
using System;
using System.IO;

namespace SentimentWebApp.PythonIntegration
{
    public class SentimentAnalysis
    {
        private readonly IPythonEnvironment _pythonEnvironment;

        public SentimentAnalysis(IPythonEnvironment pythonEnvironment)
        {
            _pythonEnvironment = pythonEnvironment;
        }

        public (string Label, double Confidence) PredictSentiment(string text)
        {
            var module = _pythonEnvironment.SentimentAnalysis();
            var result = module.PredictSentiment(text);
            return (result.Item1, result.Item2);
        }
    }
}