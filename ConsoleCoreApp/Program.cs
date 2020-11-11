using Challenge;
using Challenge.DataContracts;
using System;
using Task = System.Threading.Tasks.Task;

namespace ConsoleCoreApp
{
    // Это рекомендуемый вариант приложения.
    // Данное приложение можно запускать под Windows, Linux, Mac.
    // Для запуска приложения необходимо скачать и установить подходящую версию .NET Core.
    // Скачать можно тут: https://dotnet.microsoft.com/download/dotnet-core
    // Какая версия .NET Core нужна можно посмотреть в свойствах проекта.
    class Program
    {
        static async Task Main(string[] args)
        {
            const string teamSecret = "gUMEt0Qm/FgKeVCaKEoWjk5acFvsjUOS"; // Вставь сюда ключ команды
            if (string.IsNullOrEmpty(teamSecret))
            {
                Console.WriteLine("Задай секрет своей команды, чтобы можно было делать запросы от ее имени");
                return;
            }
            var challengeClient = new ChallengeClient(teamSecret);

            const string challengeId = "projects-course";
            Console.WriteLine($"Нажми ВВОД, чтобы получить информацию о соревновании {challengeId}");

            Console.WriteLine("Ожидание...");
            var challenge = await challengeClient.GetChallengeAsync(challengeId);
            Console.WriteLine(challenge.Description);
            Console.WriteLine();
            Console.WriteLine("----------------");
            Console.WriteLine();

            string taskType = "";

            var utcNow = DateTime.UtcNow;
            string currentRound = null;
            foreach (var round in challenge.Rounds)
            {
                if (round.StartTimestamp < utcNow && utcNow < round.EndTimestamp)
                    currentRound = round.Id;
            }

            Console.WriteLine($"Нажми ВВОД, чтобы получить первые 50 взятых командой задач типа {taskType} в раунде {currentRound}");

            Console.WriteLine("Ожидание...");
            var firstTasks = await challengeClient.GetTasksAsync(currentRound, taskType, TaskStatus.Pending, 0, 50);
            for (int i = 0; i < firstTasks.Count; i++)
            {
                var task = firstTasks[i];
                Console.WriteLine($"  Задание {i + 1}, статус {task.Status}");
                Console.WriteLine($"  Формулировка: {task.UserHint}");
                Console.WriteLine($"                {task.Question}");
                Console.WriteLine();
            }
            Console.WriteLine("----------------");
            Console.WriteLine();

            for (int j = 0; j < 600; ++j)
            {
                /*Console.WriteLine($"Нажми ВВОД, чтобы получить задачу типа {taskType} в раунде {currentRound}");

                Console.WriteLine("Ожидание...");*/
                var newTask = await challengeClient.AskNewTaskAsync(currentRound, taskType);
                /* Console.WriteLine($"  Новое задание, статус {newTask.Status}");
                 Console.WriteLine($"  Формулировка: {newTask.UserHint}");
                 Console.WriteLine($"                {newTask.Question}");
                 Console.WriteLine();
                 Console.WriteLine("----------------");
                 Console.WriteLine();*/

                string type = newTask.TypeId;
                string answer = "";

                if (type == "math")
                {
                    answer = MySolutions.SloveMath(newTask.Question);
                }
                else
                    if (type == "determinant")
                {
                    answer = MySolutions.Determinant(newTask.Question);
                }
                else
                    if (type == "polynomial-root")
                {
                    continue;
                    //answer = MyPrograms.Polynomial_root(type);
                }
                else
                    if (type == "moment")
                {
                    answer = MySolutions.Moment(newTask.Question);
                }
                else
                    if (type == "cypher")
                {
                    continue;
                    //answer = MyPrograms.Cypher(newTask.Question);
                }
                else
                    if (type == "shape")
                {
                    answer = MySolutions.Shape(newTask.Question);
                }
                if (type == "statistics")
                {
                    answer = MySolutions.Statistics(newTask.Question);
                }

                if (answer == "KEKS")
                    continue;
                /* Console.WriteLine($"Нажми ВВОД, чтобы ответить на полученную задачу самым правильным ответом: {answer}");

             Console.WriteLine("Ожидание...");*/
                var updatedTask = await challengeClient.CheckTaskAnswerAsync(newTask.Id, answer);
                /*Console.WriteLine($"  Новое задание, статус {updatedTask.Status}");
                Console.WriteLine($"  Формулировка:  {updatedTask.UserHint}");
                Console.WriteLine($"                 {updatedTask.Question}");
                Console.WriteLine($"  Ответ команды: {updatedTask.TeamAnswer}");
                Console.WriteLine();*/
                 if (updatedTask.Status == TaskStatus.Success)
                     Console.WriteLine($"Ура! Ответ угадан!");
                 else if (updatedTask.Status == TaskStatus.Failed)
                 {
                     Console.WriteLine($"Похоже ответ не подошел и задачу больше сдать нельзя...");
                     throw new Exception("Задача не правильна решена");
                 }
            }
            Console.WriteLine();
            Console.WriteLine("----------------");
            Console.WriteLine();

            Console.WriteLine($"Нажми ВВОД, чтобы завершить работу программы");

        }
    }
}
