# Aviation Quiz

A modular C# application that delivers an interactive, JSON-driven quiz experience centered on aeronautical knowledge. The system is composed of a Core library that defines data models and quiz logic, an application layer providing a console-based user interface, and comprehensive unit tests to ensure correctness and maintainability.

## Features

- **Dynamic Question Loading**  
  Questions and answer options are stored in `quiz.json` and loaded at runtime.  
- **User Management**  
  User profiles and scores are persisted in `users.json` to track progress over multiple sessions.  
- **Real-Time Scoring**  
  Immediate feedback on each answer, with a running total displayed at quiz end.  
- **Compiled Help File**  
  An integrated CHM (`help.chm`) offers guidance on application usage and command syntax.  
- **Test-Driven Development**  
  NUnit test projects verify core logic (`AviationQuiz.Core.Tests`) and application flows (`AviatieQuiz.App.Tests`).

## Architecture

1. **AviationQuiz.Core**  
   Contains domain models (`Question`, `User`, `QuizService`) and business rules for question selection, scoring and data persistence.  
2. **AviatieQuiz.App**  
   Console application that handles user input, displays questions, and interfaces with the Core library.  
3. **AviationQuizSolution**  
   Visual Studio solution aggregating all projects.  
4. **Test Projects**  
   - `QuestionTests` validates individual question behavior.  
   - `AviatieQuiz.App.Tests` verifies end-to-end quiz scenarios.

## Installation

1. **Prerequisites**  
   - [.NET SDK 6.0 or later](https://dotnet.microsoft.com/download)  
   - Git client  
2. **Clone the repository**  
   ```bash
   git clone https://github.com/racovrax/AviationQuiz.git
   cd AviationQuiz
3. **Data files**
Ensure that quiz.json and users.json remain alongside the executable so that questions and user data load correctly.
