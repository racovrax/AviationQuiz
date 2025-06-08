// -----------------------------------------------------------------------
// Autor: Chiriac Mihai
// Data: 2025-05-27
// Descriere: 
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AviatieQuiz.Core.Models;
using AviatieQuiz.Core;
using AviatieQuiz.Core.Models.JsonDtos;

namespace AviatieQuiz.Tests
{
    [TestClass]
    public class QuestionTests
    {
        [TestMethod]
        public void IsCorrect_ShouldReturnTrue_WhenOptionMatchesCorrectOptionId()
        {
            // Arrange
            var options = new List<AnswerOption>
            {
                new AnswerOption(1, "Option1"),
                new AnswerOption(2, "Option2")
            };
            var question = new Question(id: 1, text: "Sample?", options: options, correctOptionId: 2);

            // Act
            var result = question.IsCorrect(2);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsCorrect_ShouldReturnFalse_WhenOptionDoesNotMatch()
        {
            // Arrange
            var question = new Question(id: 2, text: "Sample?", options: new List<AnswerOption>(), correctOptionId: 5);

            // Act & Assert
            Assert.IsFalse(question.IsCorrect(1));
        }
    }

    [TestClass]
    public class QuestionResultTests
    {
        [TestMethod]
        public void Constructor_ShouldInitializeProperties_AndCalculateWasCorrect()
        {
            // Arrange
            var options = new List<AnswerOption> { new AnswerOption(0, "A") };
            var question = new Question(id: 3, text: "Qtext", options: options, correctOptionId: 0);

            // Act
            var result = new QuestionResult(question, selectedOptionId: 0);

            // Assert
            Assert.AreSame(question, result.OriginalQuestion);
            Assert.AreEqual(0, result.SelectedOptionId);
            Assert.IsTrue(result.WasCorrect);
            Assert.AreEqual("Qtext", result.QuestionText);
            Assert.AreEqual(0, result.CorrectOptionIdFromQuestion);
        }
    }

    [TestClass]
    public class DisciplineTests
    {
        [TestMethod]
        public void Constructor_ShouldInitializeName_AndEmptyQuestions()
        {
            // Arrange & Act
            var discipline = new Discipline(name: "TestDisc");

            // Assert
            Assert.AreEqual("TestDisc", discipline.Name);
            Assert.IsNotNull(discipline.Questions);
            Assert.AreEqual(0, discipline.Questions.Count);
            Assert.AreEqual(0, discipline.GetAllQuestions().Count);
        }

        [TestMethod]
        public void AddQuestion_ShouldAddToQuestionsCollection()
        {
            // Arrange
            var discipline = new Discipline("Disc");
            var question = new Question(id: 4, text: "T?", options: new List<AnswerOption>(), correctOptionId: 0);

            // Act
            discipline.AddQuestion(question);

            // Assert
            Assert.AreEqual(1, discipline.Questions.Count);
            Assert.AreSame(question, discipline.Questions[0]);
        }

        [TestMethod]
        public void GetRandomQuestions_ShouldReturnUpToRequestedCount()
        {
            // Arrange
            var discipline = new Discipline("Rand");
            for (int i = 0; i < 5; i++)
                discipline.AddQuestion(new Question(i, $"Q{i}", new List<AnswerOption>(), 0));

            // Act
            var subset = discipline.GetRandomQuestions(3);
            var all = discipline.GetRandomQuestions(10);

            // Assert
            Assert.AreEqual(3, subset.Count);
            Assert.AreEqual(5, all.Count);
        }
    }

    [TestClass]
    public class QuizSessionTests
    {
        private List<Question> CreateQuestions(int count)
        {
            var list = new List<Question>();
            for (int i = 0; i < count; i++)
                list.Add(new Question(i, $"Q{i}", new List<AnswerOption>(), correctOptionId: 0));
            return list;
        }

        [TestMethod]
        public void Constructor_ShouldInitializeStateCorrectly()
        {
            // Arrange & Act
            var questions = CreateQuestions(3);
            var session = new QuizSession(questions, disciplineName: "Disc");

            // Assert
            Assert.AreEqual("Disc", session.DisciplineName);
            Assert.AreEqual(0, session.CurrentQuestionIndex);
            Assert.AreEqual(3, session.TotalQuestions);
            Assert.IsFalse(session.IsQuizOver);
            Assert.IsNotNull(session.Results);
            Assert.AreEqual(0, session.Results.Count);
        }

        [TestMethod]
        public void GetCurrentQuestion_ReturnsNull_AfterQuizOver()
        {
            // Arrange
            var session = new QuizSession(CreateQuestions(1), "D");

            // Act
            var first = session.GetCurrentQuestion();
            session.GoToNextQuestion();
            var after = session.GetCurrentQuestion();

            // Assert
            Assert.IsNotNull(first);
            Assert.IsNull(after);
        }

        [TestMethod]
        public void AnswerCurrentQuestion_ShouldRecordResultOnlyOncePerQuestion()
        {
            // Arrange
            var session = new QuizSession(CreateQuestions(1), "D");

            // Act
            session.AnswerCurrentQuestion(0);
            session.AnswerCurrentQuestion(0);

            // Assert
            Assert.AreEqual(1, session.Results.Count);
        }

        [TestMethod]
        public void GoToNextQuestion_ShouldAdvanceIndex_AndFlagEnd()
        {
            // Arrange
            var session = new QuizSession(CreateQuestions(2), "D");

            // Act & Assert
            Assert.IsTrue(session.GoToNextQuestion());
            Assert.AreEqual(1, session.CurrentQuestionIndex);
            Assert.IsFalse(session.IsQuizOver);

            Assert.IsFalse(session.GoToNextQuestion());
            Assert.IsTrue(session.IsQuizOver);
        }

        [TestMethod]
        public void CalculateFinalScore_ShouldReturnCorrectFormat()
        {
            // Arrange
            var session = new QuizSession(CreateQuestions(2), "X");

            // Act
            session.AnswerCurrentQuestion(0); // correct
            session.GoToNextQuestion();
            session.AnswerCurrentQuestion(1); // incorrect
            var score = session.CalculateFinalScore();

            // Assert
            Assert.AreEqual("1/2", score);
        }

        [TestMethod]
        public void ResetSession_ShouldClearResults_AndResetIndex()
        {
            // Arrange
            var session = new QuizSession(CreateQuestions(2), "X");
            session.AnswerCurrentQuestion(0);
            session.GoToNextQuestion();

            // Act
            session.ResetSession();

            // Assert
            Assert.AreEqual(0, session.Results.Count);
            Assert.AreEqual(0, session.CurrentQuestionIndex);
            Assert.IsFalse(session.IsQuizOver);
        }
    }

    [TestClass]
    public class DataLoaderTests
    {
        private string _filePath;

        [TestInitialize]
        public void Setup()
        {
            _filePath = Path.GetTempFileName();
        }

        [TestMethod]
        public void LoadDisciplinesAndQuestions_ShouldReturnEmptyList_IfFileNotFound()
        {
            // Act
            var result = DataLoader.LoadDisciplinesAndQuestions("missing.json");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void LoadDisciplinesAndQuestions_ShouldParseValidJson()
        {
            // Arrange JSON matching JsonDtos
            var dtoList = new List<JsonDiscipline>
            {
                new JsonDiscipline
                {
                    Name = "Disc",
                    Questions = new List<JsonQuestion>
                    {
                        new JsonQuestion
                        {
                            Id = 100,
                            Text = "Txt",
                            CorrectOptionId = 1,
                            Options = new List<JsonAnswerOption>
                            {
                                new JsonAnswerOption { Id = 0, Text = "A" },
                                new JsonAnswerOption { Id = 1, Text = "B" }
                            }
                        }
                    }
                }
            };
            var content = Newtonsoft.Json.JsonConvert.SerializeObject(dtoList);
            File.WriteAllText(_filePath, content);

            // Act
            var disciplines = DataLoader.LoadDisciplinesAndQuestions(_filePath);

            // Assert
            Assert.AreEqual(1, disciplines.Count);
            var d = disciplines[0];
            Assert.AreEqual("Disc", d.Name);
            Assert.AreEqual(1, d.Questions.Count);
            var q = d.Questions[0];
            Assert.AreEqual(100, q.Id);
            Assert.AreEqual("Txt", q.Text);
            Assert.AreEqual(1, q.CorrectOptionId);
            Assert.AreEqual(2, q.Options.Count);
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (File.Exists(_filePath)) File.Delete(_filePath);
        }
    }
}