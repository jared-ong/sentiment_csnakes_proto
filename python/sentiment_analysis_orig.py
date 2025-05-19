import torch
from transformers import DistilBertTokenizer, DistilBertForSequenceClassification

# Load the pre-trained model and tokenizer from Hugging Face
model_name = "distilbert-base-uncased-finetuned-sst-2-english"
tokenizer = DistilBertTokenizer.from_pretrained(model_name)
model = DistilBertForSequenceClassification.from_pretrained(model_name)

# Move model to GPU if available
device = torch.device("cuda" if torch.cuda.is_available() else "cpu")
model.to(device)
print(f"Using device: {device}")

def predict_sentiment(text):
    # Tokenize the input text
    inputs = tokenizer(text, return_tensors="pt", truncation=True, padding=True, max_length=512)
    
    # Move inputs to the same device as the model
    inputs = {key: val.to(device) for key, val in inputs.items()}
    
    # Run inference (no gradient computation for faster execution)
    with torch.no_grad():
        outputs = model(**inputs)
    
    # Get logits and apply softmax to convert to probabilities
    logits = outputs.logits
    probabilities = torch.softmax(logits, dim=-1)
    
    # Get predicted label (0 = negative, 1 = positive)
    predicted_label = torch.argmax(probabilities, dim=-1).item()
    label = "Positive" if predicted_label == 1 else "Negative"
    
    # Get confidence score
    confidence = probabilities[0][predicted_label].item()
    
    return label, confidence

def main():
    print("Sentiment Analysis Tool (type 'quit' to exit)")
    while True:
        # Get user input
        text = input("Enter a sentence: ")
        if text.lower() == "quit":
            break
        
        # Predict sentiment
        try:
            label, confidence = predict_sentiment(text)
            print(f"Sentiment: {label} (Confidence: {confidence:.2%})")
        except Exception as e:
            print(f"Error: {e}")

if __name__ == "__main__":
    main()