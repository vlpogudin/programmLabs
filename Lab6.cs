using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq.Expressions;

namespace lab6
{
    internal class Program
    {
        /// <summary>
        /// Вывод основного меню на экран
        /// </summary>
        static void PrintMainMenu()
        {
            Console.WriteLine("=-=-= ГЛАВНОЕ МЕНЮ =-=-=");
            Console.WriteLine("1. Ввести строку вручную");
            Console.WriteLine("2. Ввести строку из заранее сформированного массива");
            Console.WriteLine("0. Завершить работу");
            Console.WriteLine("\nВыберите пункт меню:");
        }

        /// <summary>
        /// Вывод меню ввода строки
        /// </summary>
        static void PrintGetMenu()
        {
            Console.WriteLine("=-=-= ВВОД СТРОКИ =-=-=");
            Console.WriteLine("Введите строку:");
        }

        /// <summary>
        /// Вывод меню выбора способа обработки строки
        /// </summary>
        static void PrintInputMenu()
        {
            Console.WriteLine("=-=-= МЕНЮ ВВОДА =-=-=");
            Console.WriteLine("1. Обработка строки первым вариантом");
            Console.WriteLine("2. Обработка строки вторым вариантом (нормализация вывода)");
            Console.WriteLine("3. Выход в главное меню");
            Console.WriteLine("\nВыберите пункт меню: ");
        }

        /// <summary>
        /// Вывод меню выхода
        /// </summary>
        static void PrintExitMenu()
        {
            Console.WriteLine("Вы уверены, что хотите завершить работу?");
            Console.WriteLine("1. Да");
            Console.WriteLine("2. Нет");
            Console.WriteLine("\nВыберите пункт меню:");
        }

        /// <summary>
        /// Проверка корректности введённого числа
        /// </summary>
        /// <returns>Введённое пользователем число</returns>
        static int GetInt()
        {
            int intNumber; // объявление переменной, отвечающей за введенный номер
            bool isConvert; // объявление переменной, отвечающей за возможность конвертации веденного номера
            do // цикл проверки корректного ввода пункта меню
            {
                isConvert = int.TryParse(Console.ReadLine(), out intNumber); // проверка введённой строки на принадлежность к целочисленному типу
                if (!isConvert) // введённое число нецелочисленное
                    Console.Write("Неправильно введено число либо выходит из диапазона!\nПопробуйте ещё раз: ");
            } while (!isConvert);
            return intNumber;
        }

        /// <summary>
        /// Первый способ обработки строки (без нормализации вывода)
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="answer"></param>
        static void FirstMethod(string inputString, int answer = 1)
        {
            Regex rx = new Regex(@"[^.!?]+[.!?]"); /* поиск последовательности символов, к-я не содержит "!.?", за к-й следует "!.?" (поиск предложений в строке)
                                                    "[^.!?]" - любой символ кроме "!.?"
                                                    "+" - повтор символов м/б >1 раза
                                                    "[.!?]" - поиск символов "!.?" - конец предложения */
            MatchCollection sentences = rx.Matches(inputString); // найденные совпадения регулярного выражения
            if (sentences.Count >= 2) // кол-во предложений больше двух
            {
                string lastSentence = sentences[^1].ToString().TrimStart();
                string newString = lastSentence; // начинаем строку с последнего предложения
                for (int i = 1; i < sentences.Count - 1; i++) // цикл заполнения строки всеми средними предложениями
                {
                    newString += sentences[i];
                }
                newString += " " + sentences[0].ToString(); // заканчиваем строку первым предложением
                Console.Clear();
                Console.WriteLine($"Исходная строка:\n{inputString}\n\nРезультат:\n{newString}\n");
            }
            else // кол-во предложений менее двух
            {
                Console.Clear();
                Console.WriteLine("В строке должно быть хотя бы 2 предложения!\n");
                InputString(answer); // повторный вызов ввода строки
            }
        }

        /// <summary>
        /// Второй способ обработки строки (нормализация вывода)
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="answer"></param>
        static void SecondMethod(string inputString, int answer = 2)
        {
            string exitString = inputString; // инициализация переменной для выходного сообщения
            inputString = inputString.Replace("\t", " "); // убираем все знаки табуляции
            Regex rx = new Regex(@"([:;,.""\*!?\s])\1+"); /* поиск в строке символов ":;,.""\*!?пробел", повторяющихся >1 раза
                                                            "[:;,.""\*!?\s]" - поиск символов в строке
                                                            "\1+" -
                                                            символы ранее в скобках м/б >1 раза */
            string newInputString = rx.Replace(inputString, "$1"); // все повторяющиеся элементы заменяем на значение первой группы символов ([:;,.""\*!?\s])
            rx = new Regex(@"(?<=[.!?])\s+"); /* поиск пробелов в строке, к-е идут после ".!?"
                                                "?<=[.!?]" - поиск пробела, к-ому предшествуют ".!?"
                                                "\s+" - пробел повторяется >1 раза */
            string[] sentences = rx.Split(newInputString); // разделение строки по регулярному выражению
            if (sentences.Length >= 2) // кол-во предложений больше двух
            {
                bool isFirst = true; // инициализаия переменной, отвечающей за первое предложение в строке
                string temp = sentences[0].ToString().Trim(); // далее - смена местами первого и последнего предложений
                sentences[0] = sentences[^1];
                sentences[^1] = temp;
                string newString = ""; // инициализация новой строки
                foreach (string sentence in sentences) // цикл прохода по всем предложением с формированием новой строки
                {
                    if (isFirst) // предложение - первое
                    {
                        inputString = sentence; // далее - замена первой буквы предложения на прописную
                        inputString = inputString.ToLower();
                        string firstLetter = inputString[0].ToString().ToUpper();
                        inputString = inputString.Remove(0, 1).Insert(0, firstLetter);
                        newString += inputString; // корректную строку добавляем в начало
                        isFirst = false;
                    }
                    else // предложение - не первое
                    {
                        inputString = sentence; // далее - замена первой буквы предложения на прописную
                        inputString = inputString.ToLower();
                        string firstLetter = inputString[0].ToString().ToUpper();
                        inputString = inputString.Remove(0, 1).Insert(0, firstLetter);
                        newString += " " + inputString.Trim(); // корректную строку добавляем в предложение через пробел
                    }

                }
                Console.Clear();
                Console.WriteLine($"Исходная строка:\n{exitString}\n\nРезультат:\n{newString}\n");
            }
            else // кол-во предложений меньше двух
            {
                Console.Clear();
                Console.WriteLine("В строке должно быть хотя бы 2 предложения!\n");
                InputString(answer); // повторный вызов ввода предложения
            }
        }

        /// <summary>
        /// Выбор введённого пользователем пункта меню
        /// </summary>
        /// <param name="answer"></param>
        static void InputString(int answer)
        {

            bool success = false; // инициализация переменной, отвечающая за успешное выполнение одного из блоков switch
            while (!success) // цикл ввода новой строки до того момента, пока она не будет корректна
            {
                try // обработка ошибки ввода некорректной строки
                {
                    PrintGetMenu();
                    string inputString = Console.ReadLine();
                    switch (answer)
                    {
                        case 1:
                            {
                                CorrectSentences(inputString, answer);
                                success = true;
                                break;
                            }
                        case 2:
                            {
                                CorrectSentences(inputString, answer);
                                success = true;
                                break;
                            }
                    }
                }
                catch (System.IndexOutOfRangeException)
                {
                    Console.Clear();
                    Console.WriteLine("Введена некорректная строка. Повторите ввод!\n");
                }
            }
        }

        /// <summary>
        /// Выбор пункта меню (массив сформированных строк)
        /// </summary>
        /// <param name="answer"></param>
        /// <param name="inputString"></param>
        /// <param name="arrString"></param>
        static void StringArr(int answer, string inputString, string[] arrString)
        {
            switch (answer)
            {
                case 1:
                    {
                        CorrectSentencesArr(inputString, answer, arrString);
                        break;
                    }
                case 2:
                    {
                        CorrectSentencesArr(inputString, answer, arrString);
                        break;
                    }
            }
        }

        /// <summary>
        /// Получение номера строки в сформированном массиве
        /// </summary>
        /// <param name="arrString"></param>
        /// <returns>Строка из массива под введённым номером</returns>
        static string GetNumber(string[] arrString)
        {
            string inputString = ""; // инициализация переменной, отвечающей за строку массива
            bool success = false; // инициализация переменной, отвечающей за успешное выполнение одного из блоков switch
            while (!success)
            {
                try // обработка ошибки ввода номера, находящегося за пределами длины массива
                {
                    Console.WriteLine("Введите номер строки в массиве:");
                    int number = GetInt() - 1;
                    inputString = arrString[number].ToString();
                    success = true;

                }
                catch (System.IndexOutOfRangeException)
                {
                    Console.WriteLine("Элемента под таким номером не существует!\n");
                }
            }
            return inputString;
        }

        /// <summary>
        /// Проверка корректного ввода строки
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="answer"></param>
        static void CorrectSentences(string inputString, int answer)
        {
            bool isValid = CheckSentenceFormat(inputString); // корректный ввод формата предложения
            bool isNumeric = CheckCountNumber(inputString); // в предложении - не только числа и их больше двух

            if (isValid && isNumeric)
            {
                if (answer == 1)
                    FirstMethod(inputString);
                else if (answer == 2)
                    SecondMethod(inputString);
            }
            else
            {
                Console.WriteLine("Некоторые предложения содержат ошибки формата ввода. Повторите ввод!\n");
                InputString(answer); // повторный ввод строки
            }
        }

        /// <summary>
        /// Проверка корректности строки из массива
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="answer"></param>
        /// <param name="arrString"></param>
        static void CorrectSentencesArr(string inputString, int answer, string[] arrString)
        {
            bool isValid = CheckSentenceFormat(inputString);
            bool isNumeric = CheckCountNumber(inputString);

            if (isValid && isNumeric)
            {
                if (answer == 1)
                    FirstMethod(inputString);
                else if (answer == 2)
                    SecondMethod(inputString);
            }
            else
            {
                Console.WriteLine($"Некоторые предложения строки \"{inputString}\" содержат ошибки формата ввода. Повторите ввод!\n");
                inputString = GetNumber(arrString);
                StringArr(answer, inputString, arrString); // повторный ввод номера
            }
        }

        /// <summary>
        /// Проверка заполнения предложений исключительно числами
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns>Строка исключительно из чисел/нет</returns>
        static bool CheckCountNumber(string inputString)
        {
            Regex rx = new Regex(@"(?<=[.!?])\s+");
            string[] sentences = rx.Split(inputString.Trim());
            foreach (string sentence in sentences) // проход по всем предложениям
            {
                string[] words = sentence.Split(new char[] { '!', '?', '.', ' ' }); // разбиваем предложение на слова
                Array.Resize(ref words, words.Length - 1); // последний пробел удаляем
                int countNum = 0; // инициализация переменной, отвечающей за число чисел в предложении
                foreach (string word in words) // проход по всем словам
                {
                    bool isNumeric = int.TryParse(word, out _);
                    if (isNumeric) // слово - число
                        countNum++;
                }
                if (countNum == words.Length && countNum >= 2) // все слова в предложении - числа, и их больше двух
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Проверка корректного формата ввода строки
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns>Строка корректна/нет</returns>
        static bool CheckSentenceFormat(string inputString)
        {
            Regex rx = new Regex(@"(?<=[.!?])\s+");
            string[] sentences = rx.Split(inputString.Trim());

            int count = 0; // инициализация переменной, отвечающей за счётчик прописных букв
            for (int i = 0; i < sentences.Length; i++) // проход по всем предложениям
            {
                sentences[i] = sentences[i].ToString().Replace("!", " ").Replace(".", " ").Replace("?", " "); // оставляем пробелы
                string[] words = sentences[i].ToString().Trim().Split(' '); // массив слов по пробелам
                bool isNumeric = int.TryParse(words[0], out _);
                if (!isNumeric) // если это не число
                {
                    try
                    {
                        char firstLetter = sentences[i].ToString().Trim().ToCharArray()[0]; // первый символ слова
                        bool isUpper = char.IsUpper(firstLetter); // первый символ - прописная
                        if (isUpper)
                            count += 1;
                    }
                    catch (System.IndexOutOfRangeException)
                    {
                        return false;
                    }
                }
                else
                    count++;
            }
            if (count == sentences.Length) // если заглавных столько же, сколько и предложений
                return true;
            else
                return false;
        }
        static void Main(string[] args)
        {
            int answer;
            do
            {
                string[] arrString = File.ReadAllLines(@"../../../test.txt");
                PrintMainMenu();
                answer = GetInt();
                Console.Clear();
                switch (answer)
                {
                    case 1:
                        {
                            do
                            {
                                PrintInputMenu();
                                answer = GetInt();
                                Console.Clear();
                                switch (answer)
                                {
                                    case 1:
                                        {
                                            Console.Clear();
                                            InputString(answer);
                                            break;
                                        }
                                    case 2:
                                        {
                                            Console.Clear();
                                            InputString(answer);
                                            break;
                                        }
                                    case 3:
                                        {
                                            Console.Clear();
                                            break;
                                        }
                                    default:
                                        {
                                            Console.WriteLine("Пункта под таким номером нет. Попробуйте ещё раз!\n");
                                            break;
                                        }
                                }
                            } while (answer != 3);
                            break;
                        }
                    case 2:
                        {
                            do
                            {
                                PrintInputMenu();
                                answer = GetInt();
                                Console.Clear();
                                switch (answer)
                                {
                                    case 1:
                                        {
                                            Console.Clear();
                                            string inputString = GetNumber(arrString);
                                            StringArr(answer, inputString, arrString);
                                            break;
                                        }
                                    case 2:
                                        {
                                            Console.Clear();
                                            string inputString = GetNumber(arrString);
                                            StringArr(answer, inputString, arrString);
                                            break;
                                        }
                                    case 3:
                                        {
                                            Console.Clear();
                                            break;
                                        }
                                    default:
                                        {
                                            Console.WriteLine("Пункта под таким номером нет. Попробуйте ещё раз!\n");
                                            break;
                                        }
                                }
                            } while (answer != 3);
                            break;
                        }
                    case 0:
                        {
                            do
                            {
                                PrintExitMenu();
                                answer = GetInt();
                                Console.Clear();
                                switch (answer)
                                {
                                    case 1:
                                        {
                                            Console.Clear();
                                            Console.WriteLine("Программа завершена!");
                                            System.Environment.Exit(0);
                                            break;
                                        }
                                    case 2:
                                        {
                                            Console.Clear();
                                            break;
                                        }
                                    default:
                                        {
                                            Console.Clear();
                                            Console.WriteLine("Пункта под таким номером нет. Попробуйте ещё раз!\n");
                                            break;
                                        }
                                }
                            } while (answer != 2);
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Пункта под таким номером нет. Попробуйте ещё раз!\n");
                            break;
                        }
                }
            } while (answer != 0);
        }
    }
}
