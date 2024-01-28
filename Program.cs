`csharp
using System;
using System.Collections.Generic;

namespace DailyPlanner
{
    class Program
    {
        static Dictionary<DateTime, List<Note>> notes = new Dictionary<DateTime, List<Note>>();

        static void Main(string[] args)
        {
            // Создаем несколько заметок
            DateTime date1 = new DateTime(2022, 1, 6);
            Note note1 = new Note("Заметка 1", "Описание заметки 1", date1, new DateTime(2022, 1, 10));
            AddNoteToDictionary(note1);

            DateTime date2 = new DateTime(2022, 1, 8);
            Note note2 = new Note("Заметка 2", "Описание заметки 2", date2, new DateTime(2022, 1, 12));
            AddNoteToDictionary(note2);

            DateTime date3 = new DateTime(2022, 1, 13);
            Note note3 = new Note("Заметка 3", "Описание заметки 3", date3, new DateTime(2022, 1, 15));
            AddNoteToDictionary(note3);

            // Устанавливаем текущую дату на первую заметку
            DateTime currentDate = date1;

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Ежедневник на {currentDate.ToShortDateString()}\\n");

                if (notes.ContainsKey(currentDate))
                {
                    List<Note> notesForDate = notes[currentDate];
                    for (int i = 0; i < notesForDate.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {notesForDate[i].Title}");
                    }
                }
                else
                {
                    Console.WriteLine("На эту дату нет заметок");
                }

                Console.WriteLine("\\nВыберите заметку или переключитесь на другую дату:");

                // Отображаем стрелочное меню для переключения между датами
                string[] menuOptions = new string[] { "<", ">", "Подробнее", "Выход" };
                int selectedOptionIndex = ShowArrowMenu(menuOptions);

                switch (selectedOptionIndex)
                {
                    case 0:
                        currentDate = currentDate.AddDays(-1);
                        break;
                    case 1:
                        currentDate = currentDate.AddDays(1);
                        break;
                    case 2:
                        ShowNoteDetails(currentDate);
                        break;
                    case 3:
                        Console.WriteLine("До свидания!");
                        return;
                }
            }
        }

        static void AddNoteToDictionary(Note note)
        {
            if (notes.ContainsKey(note.Date))
            {
                notes[note.Date].Add(note);
            }
            else
            {
                List<Note> notesForDate = new List<Note>();
                notesForDate.Add(note);
                notes.Add(note.Date, notesForDate);
            }
        }

        static int ShowArrowMenu(string[] options)
        {
            int selectedIndex = 0;

            while (true)
            {
                Console.CursorVisible = false;
                for (int i = 0; i < options.Length; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                    }
                    Console.Write($" {options[i]} ");
                    Console.ResetColor();
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    selectedIndex--;
                    if (selectedIndex < 0)
                    {
                        selectedIndex = options.Length - 1;
                    }
                }
                else if (keyInf
o.Key == ConsoleKey.RightArrow)
                {
                    selectedIndex++;
                    if (selectedIndex >= options.Length)
                    {
                        selectedIndex = 0;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    Console.CursorVisible = true;
                    return selectedIndex;
                }
            }
        }

        static void ShowNoteDetails(DateTime date)
        {
            if (notes.ContainsKey(date))
            {
                List<Note> notesForDate = notes[date];
                Console.Clear();
                Console.WriteLine($"Заметки на {date.ToShortDateString()}:\\n");
                for (int i = 0; i < notesForDate.Count; i++)
                {
                    Console.WriteLine($"Заголовок: {notesForDate[i].Title}");
                    Console.WriteLine($"Описание: {notesForDate[i].Description}");
                    Console.WriteLine($"Дата создания: {notesForDate[i].Date.ToShortDateString()}");
                    Console.WriteLine($"Дата выполнения: {notesForDate[i].DueDate.ToShortDateString()}");
                    Console.WriteLine();
                }

                Console.WriteLine("Нажмите Enter для возврата в главное меню");
                Console.ReadLine();
            }
        }
    }

    class Note
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }

        public Note(string title, string description, DateTime date, DateTime dueDate)
        {
            Title = title;
            Description = description;
            Date = date;
            DueDate = dueDate;
        }
    }
}