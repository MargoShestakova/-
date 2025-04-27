using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SimptomChek
{
    public partial class MainWindow : Window
    {
        private List<DiseaseCategory> _diseases;
        private UserInfoControl _userInfoControl;
        private SymptomsControl _symptomsControl;
        private QuestionsControl _questionsControl;
        private ResultsControl _resultsControl;
        private List<Disease> _foundDiseases;
        private int _currentQuestionIndex = 0;
        private Disease _currentDisease;

        public MainWindow()
        {
            InitializeComponent();
            LoadDiseases("Diseases.json");

            _userInfoControl = new UserInfoControl();
            _symptomsControl = new SymptomsControl();
            _questionsControl = new QuestionsControl();
            _resultsControl = new ResultsControl();

            MainContentControl.Content = _userInfoControl;
        }
        private void LoadDiseases(string filename)
        {
            try
            {
                // Используй относительный путь:
                string jsonString = File.ReadAllText("Diseases.json");

                // Дальше код остается без изменений:
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                _diseases = JsonSerializer.Deserialize<List<DiseaseCategory>>(jsonString, options);

                if (_diseases == null)
                {
                    MessageBox.Show("Ошибка: не удалось десериализовать данные из Diseases.json");
                }
                else
                {
                    Console.WriteLine($"Успешно загружено {_diseases.Count} категорий болезней"); // Отладочное сообщение
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке Diseases.json: {ex.Message}");
            }
        }

        private async void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainContentControl.Content == _userInfoControl)
            {
                // Получение данных о пользователе
                string gender = _userInfoControl.GetGender();
                string ageText = _userInfoControl.GetAge();

                // Проверка возраста
                if (!int.TryParse(ageText, out int age))
                {
                    MessageBox.Show("Пожалуйста, введите корректный возраст.");
                    return;
                }

                // Переход к вводу симптомов
                MainContentControl.Content = _symptomsControl;
            }
            else if (MainContentControl.Content == _symptomsControl)
            {
                // Получение симптомов
                string symptomsText = _symptomsControl.GetSymptoms();
                string[] symptoms = symptomsText.ToLower().Split(',').Select(s => s.Trim()).ToArray();

                Console.WriteLine($"Введенные симптомы: {string.Join(", ", symptoms)}"); // Отладочное сообщение

                // Поиск болезней
                string gender = _userInfoControl.GetGender();
                int age = int.Parse(_userInfoControl.GetAge());
                _foundDiseases = FindDiseasesBySymptoms(symptoms, age, gender);

                if (_foundDiseases.Count == 0)
                {
                    MessageBox.Show("Совпадений не найдено. Пожалуйста, уточните симптомы.");
                    MainContentControl.Content = _userInfoControl; // Возврат к вводу данных
                    return;
                }

                Console.WriteLine($"Найдено {_foundDiseases.Count} болезней"); // Отладочное сообщение

                // Запуск вопросов
                _currentDisease = _foundDiseases[0]; // Начинаем с первой болезни
                _currentQuestionIndex = 0;
                ShowQuestion();
            }
            else if (MainContentControl.Content == _questionsControl)
            {
                // Обработка ответа на вопрос
                bool answer = (_questionsControl.Tag as string) == "Yes"; // Получаем ответ из Tag

                if (answer && _currentDisease.Questions[_currentQuestionIndex - 1].Worsens)
                {
                    // Здесь можно добавить логику учета "ухудшающих" ответов
                    // Например, увеличить счетчик ухудшающих ответов для болезни
                }

                // Переход к следующему вопросу или к результатам
                if (_currentQuestionIndex < _currentDisease.Questions.Length)
                {
                    ShowQuestion();
                }
                else
                {
                    // Все вопросы заданы, выводим результаты
                    DisplayResults();
                }
            }
        }

        private List<Disease> FindDiseasesBySymptoms(string[] symptoms, int age, string gender)
        {
            List<Disease> foundDiseases = new List<Disease>();

            if (_diseases == null)
            {
                Console.WriteLine("Ошибка: _diseases is null");
                return foundDiseases;
            }

            Console.WriteLine($"Поиск болезней по симптомам: {string.Join(", ", symptoms)}, возраст: {age}, пол: {gender}"); // Отладочное сообщение

            foreach (var category in _diseases)
            {
                foreach (var disease in category.Diseases)
                {
                    Console.WriteLine($"Проверка болезни: {disease.Name}, симптомы: {string.Join(", ", disease.Symptoms)}, пол: {disease.Gender}"); // Отладочное сообщение

                    // Фильтр по полу
                    if (gender != "Не выбрано" && !string.Equals(disease.Gender, gender, StringComparison.OrdinalIgnoreCase) && !string.Equals(disease.Gender, "all", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine($"Болезнь {disease.Name} пропущена из-за пола"); // Отладочное сообщение
                        continue;
                    }

                    // Поиск совпадений по симптомам
                    int commonSymptoms = symptoms.Count(s => disease.Symptoms.Contains(s));
                    Console.WriteLine($"Совпадений симптомов для болезни {disease.Name}: {commonSymptoms}"); // Отладочное сообщение

                    if (commonSymptoms > 0)
                    {
                        foundDiseases.Add(disease);
                        Console.WriteLine($"Найдена болезнь: {disease.Name}, совпадений: {commonSymptoms}");
                    }
                }
            }

            return foundDiseases;
        }

        private void ShowQuestion()
        {
            if (_currentDisease == null || _currentDisease.Questions == null || _currentDisease.Questions.Length == 0)
            {
                Console.WriteLine("Ошибка: нет вопросов для текущей болезни");
                DisplayResults(); // Если нет вопросов, сразу выводим результаты
                return;
            }

            if (_currentQuestionIndex < _currentDisease.Questions.Length)
            {
                Question currentQuestion = _currentDisease.Questions[_currentQuestionIndex];
                _questionsControl.QuestionText = currentQuestion.Text;

                // Отписываемся от старых обработчиков, чтобы избежать дублирования
                _questionsControl.YesClick -= QuestionButton_Click;
                _questionsControl.NoClick -= QuestionButton_Click;

                // Подписываемся на новые обработчики
                _questionsControl.YesClick += QuestionButton_Click;
                _questionsControl.NoClick += QuestionButton_Click;

                MainContentControl.Content = _questionsControl;
                _questionsControl.Tag = null; // Сброс Tag
                _currentQuestionIndex++;

                Console.WriteLine($"Отображен вопрос: {currentQuestion.Text}"); // Отладочное сообщение
            }
            else
            {
                DisplayResults();
            }
        }

        private void QuestionButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                _questionsControl.Tag = button.Content.ToString(); // Сохраняем ответ в Tag
                NextButton_Click(sender, e); // Переходим к следующему этапу
            }
        }

        private void DisplayResults()
        {
            _resultsControl.ResultsText = "Возможные болезни:\n";
            foreach (var disease in _foundDiseases)
            {
                _resultsControl.ResultsText += $"- {disease.Name}\n";
            }
            MainContentControl.Content = _resultsControl;
        }
    }
}