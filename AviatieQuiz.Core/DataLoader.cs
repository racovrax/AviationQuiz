// -----------------------------------------------------------------------
// Autor: Agape Ioan
// Data: 2025-05-27
// Descriere: 
// -----------------------------------------------------------------------



using AviatieQuiz.Core.Models;
using AviatieQuiz.Core.Models.JsonDtos;
using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AviatieQuiz.Core
{
    public static class DataLoader
    {
        public static List<Discipline> LoadDisciplinesAndQuestions(string filePath)
        {
            var domainDisciplines = new List<Discipline>();
            if (!File.Exists(filePath))
            {
                Debug.WriteLine($"Fisierul nu a fost gasit la {filePath}");
                return domainDisciplines;
            }

            try
            {
                string jsonString = File.ReadAllText(filePath);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var loadedJsonDisciplines = JsonSerializer.Deserialize<List<JsonDiscipline>>(jsonString, options);

                if (loadedJsonDisciplines != null)
                {
                    foreach (var jsonDisc in loadedJsonDisciplines)
                    {
                        var discipline = new Discipline(jsonDisc.Name);
                        if (jsonDisc.Questions != null)
                        {
                            foreach (var jsonQ in jsonDisc.Questions)
                            {
                                if (jsonQ.Options != null)
                                {
                                    var answerOptions = jsonQ.Options
                                        .Select(opt => new AnswerOption(opt.Id, opt.Text))
                                        .ToList();

                                    var question = new Question(
                                        jsonQ.Id,
                                        jsonQ.Text,
                                        answerOptions,
                                        jsonQ.CorrectOptionId
                                    );
                                    discipline.AddQuestion(question);
                                }
                            }
                        }
                        domainDisciplines.Add(discipline);
                    }
                }
            }
            catch (JsonException jsonEx)
            {
                Debug.WriteLine($"Eroare la parsarea JSON: {jsonEx.Message}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Eroare generala la incarcarea datelor: {ex.Message}");
            }
            return domainDisciplines;
        }
    }



}