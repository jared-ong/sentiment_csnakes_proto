import torch
from transformers import DistilBertTokenizer, DistilBertForSequenceClassification
from typing import Tuple

# Load the pre-trained model and tokenizer from Hugging Face
model_name = "distilbert-base-uncased-finetuned-sst-2-english"
tokenizer = DistilBertTokenizer.from_pretrained(model_name)
model = DistilBertForSequenceClassification.from_pretrained(model_name)

# Move model to GPU if available
device = torch.device("cuda" if torch.cuda.is_available() else "cpu")
model.to(device)
print(f"Using device: {device}")

def predict_sentiment(text: str) -> Tuple[str, float]:
    """
    Predicts sentiment of input text.
    Args:
        text: Input sentence to analyze.
    Returns:
        Tuple of (sentiment label, confidence score).
    """
    # Tokenize the input text
    inputs = tokenizer(text, return_tensors="pt", truncation=True, padding=True, max_length=512)
    
    # Move inputs to the same device as the model
    inputs = {key: val.to(device) for key, val in inputs.items()}
    
    # Run inference
    with torch.no_grad():
        outputs = model(**inputs)
    
    # Get logits and apply softmax
    logits = outputs.logits
    probabilities = torch.softmax(logits, dim=-1)
    
    # Get predicted label
    predicted_label = torch.argmax(probabilities, dim=-1).item()
    label = "Positive" if predicted_label == 1 else "Negative"
    
    # Get confidence score
    confidence = probabilities[0][predicted_label].item()
    
    return label, confidence