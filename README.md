# Sentiment Analysis Web Application

This is a simple ASP.NET Core web application that performs sentiment analysis on user-provided text using a Python script integrated via CSnakes.

## Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) 8.0 or higher
- [Python](https://www.python.org/downloads/) 3.12
- Git (optional, for cloning the repository)

## Setup Instructions

1. **Clone the Repository** (if applicable):
   ```bash
   git clone https://github.com/your-repo/sentiment-webapp.git
   cd sentiment-webapp
   ```

2. **Set Up Python Virtual Environment**:
   - Navigate to the `python` directory:
     ```bash
     cd python
     ```
   - Create a virtual environment named `venv`:
     ```bash
     python -m venv venv
     ```
   - Activate the virtual environment:
     - On Windows:
       ```bash
       venv\Scripts\activate
       ```
     - On macOS/Linux:
       ```bash
       source venv/bin/activate
       ```
   - Install dependencies:
     ```bash
     pip install -r requirements.txt
     ```

3. **Set Up .NET Project**:
   - Navigate back to the project root:
     ```bash
     cd ..
     ```
   - Restore .NET dependencies:
     ```bash
     dotnet restore
     ```

4. **Run the Application**:
   - Build and run the application:
     ```bash
     dotnet run
     ```
   - Open a web browser and navigate to `https://localhost:5001` (or the specified port).

## Usage

- Enter a sentence in the text area and click "Analyze" to see the sentiment analysis results.

## Notes

- Ensure that the Python virtual environment is correctly configured.
- If using a GPU, verify that PyTorch detects it by checking the console output for "Using device: cuda".
- For any issues with Python dependencies, ensure that the virtual environment is activated and dependencies are installed correctly.