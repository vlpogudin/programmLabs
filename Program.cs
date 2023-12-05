namespace lab4
{
    internal class Program
    {

        /// <summary>
        /// Функция нахождения опорного числа
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="minIndex"></param>
        /// <param name="maxIndex"></param>
        /// <returns>Найденное опорное число</returns>
        static int FindPivot(int[] arr, int minIndex, int maxIndex) // функция нахождения опорного числа
        {
            int pivot = minIndex - 1; // инициализация переменной, отвечающей за опорное число
            int temp; // объявление временной переменной
            for (int i = minIndex; i < maxIndex; i++) // цикл постановки опорного числа на верное место
            {
                if (arr[i] < arr[maxIndex]) // постановка опорного числа на верное место
                {
                    pivot++;
                    temp = arr[pivot];
                    arr[pivot] = arr[i];
                    arr[i] = temp;
                }
            }

            pivot++; // выравнивание индекса
            temp = arr[pivot];
            arr[pivot] = arr[maxIndex];
            arr[maxIndex] = temp;

            return pivot;
        }

        /// <summary>
        /// Функция быстрой сортировки массива
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="minIndex"></param>
        /// <param name="maxIndex"></param>
        /// <returns>Отсортированный массив</returns>
        static int[] QuickSort(int[] arr, int minIndex, int maxIndex) // функция быстрой сортировки массива
        {
            if (minIndex >= maxIndex)
                return arr;

            int pivot = FindPivot(arr, minIndex, maxIndex); // нахождение опорного числа
            QuickSort(arr, minIndex, pivot - 1);
            QuickSort(arr, pivot + 1, maxIndex);

            return arr;
        }
        static void Main(string[] args)
        {
            int answer; // объявление переменной, отвечающей за выбор пункта меню
            bool isConvertAnsw; // объявление переменной, отвечающей за корректный ответ выбора пункта меню
            int length; // объявление переменной, отвечающей за длину массива
            int[] arr = new int[0]; // создание массива 
            do
            {
                // вывод меню в консоль
                Console.WriteLine("1. Ввести элементы массива");
                Console.WriteLine("2. Сформировать массив с помощью ДСЧ");
                Console.WriteLine("3. Напечатать массив");
                Console.WriteLine("4. Удалить из массива N элементов, начиная с номера K");
                Console.WriteLine("5. Добавить элемент с номером K");
                Console.WriteLine("6. Поменять местами элементы с четными и нечетными номерами");
                Console.WriteLine("7. Поиск элемента, равного среднему арифметическому элементов массива");
                Console.WriteLine("8. Сортировка массива простым обменом");
                Console.WriteLine("9. Поиск элемента (бинарный поиск)");
                Console.WriteLine("10. Очистить консоль");
                Console.WriteLine("11. Выход");
                Console.WriteLine("\nВыберите пункт меню:");

                do // цикл проверки корректного ввода пункта меню
                {
                    isConvertAnsw = int.TryParse(Console.ReadLine(), out answer); // проверка введённой строки на принадлежность к целочисленному типу
                    if (!isConvertAnsw) // введённое число нецелочисленное
                        Console.WriteLine("Неправильно введено число либо выходит из диапазона!\nПопробуйте ещё раз\n");
                } while (!isConvertAnsw);

                switch (answer) // вветвления для выбранных пунктов меню
                {
                    case 1: // блок ручного ввода элементов в массив
                        {
                            bool isConvert; // объявление переменной, отвечающей за корректное преобразование введённой строки к типу
                            do // цикл  проверки корректного ввода количества элементов
                            {
                                Console.WriteLine("Введите количество элементов массива:");
                                isConvert = int.TryParse(Console.ReadLine(), out length);
                                if (!isConvert || length < 0 || length > 2147483591)
                                    Console.WriteLine("Неправильно введено число либо выходит из диапазона!\nПопробуйте ещё раз\n");
                            } while (!isConvert || length < 0 || length > 2147483591);

                            arr = new int[length]; // изменение длины массива с заданным числом от пользователя
                            int element; // объявление переменной, отвечающей за элемент массива
                            for (int i = 0; i < length; i++) // цикл ручного ввода элементов массива пользователем
                            {
                                do // цикл проверки корректного ввода элемента массива
                                {
                                    Console.WriteLine($"Введите элемент массива {i+1}:");
                                    isConvert = int.TryParse(Console.ReadLine(), out element);
                                    if (!isConvert)
                                        Console.WriteLine("Неправильно введено число либо выходит из диапазона!\nПопробуйте ещё раз\n");
                                } while (!isConvert);

                                arr[i] = element; // присваивание элементу массива введённого пользоваталем значения
                            }
                            Console.WriteLine("Массив сформирован\n");
                            break;
                        }

                    case 2: // блок создания рандомных значений для элементов массива
                        {
                            bool isConvert;
                            do
                            {
                                Console.WriteLine("Введите количество элементов массива");
                                isConvert = int.TryParse(Console.ReadLine(), out length);
                                if (!isConvert || length < 0)
                                    Console.WriteLine("Неправильно введено число!\nПопробуйте ещё раз");
                            } while (!isConvert || length < 0);

                            Random rnd = new Random(); // инициализация переменной, отвечающей за рандомное значение элемента массива
                            arr = new int[length]; 
                            for (int i = 0; i < length; i++) // цикл присваивания рандомных значений элементам массива
                                arr[i] = rnd.Next(-100, 100); // присваивание элементу массива рандомного значения
                            Console.WriteLine("Массив сформирован\n");
                            break;  
                        }

                    case 3: // блок вывода массива
                        {
                            if (arr.Length == 0) // проверка массива на пустоту
                                Console.WriteLine("Массив пуст\n");
                            else
                                Console.WriteLine("[{0}]\n", string.Join(", ", arr)); // вывод массива
                            break;
                        }

                    case 4: // блок удаления элементов из массива
                        {
                            if (arr.Length == 0) // проверка массива на пустоту
                                Console.WriteLine("Массив пуст, невозможно удалить элементы\n");
                            else
                            {
                                bool isConvert;
                                int position, count; // объявление переменных, отвечающих за номер элемента и счётчик
                                do // цикл проверки корректного ввода номера элемента
                                {
                                    Console.WriteLine("Введите номер элемента:");
                                    isConvert = int.TryParse(Console.ReadLine(), out position);
                                    if (!isConvert)
                                        Console.WriteLine("Неправильно введено число либо выходит из диапазона!\nПопробуйте ещё раз\n");
                                    else if (position <= 0 || position > arr.Length) // введённый номер за пределами массива
                                        Console.WriteLine($"Число должно находиться в промежутке от 1 до {arr.Length}");
                                } while (!isConvert || position <= 0 || position > arr.Length);

                                do // цикл проверки корректного ввода количества элементов
                                {
                                    Console.WriteLine("Введите количество элементов:");
                                    isConvert = int.TryParse(Console.ReadLine(), out count);
                                    if (!isConvert)
                                        Console.WriteLine("Неправильно введено число либо выходит из диапазона!\nПопробуйте ещё раз\n");
                                    else if (position + count > arr.Length + 1 || count < 0) // количество за пределами возможных значений
                                        Console.WriteLine($"Число должно находиться в промежутке от 0 до {arr.Length - position + 1}");
                                } while (!isConvert || position + count > arr.Length + 1 || count < 0);

                                int[] newArr = new int[arr.Length - count]; /* создание нового массива с длиной, равной количеству
                                                                            неудаляемых элементов */ 
                                for (int i = 0; i < position - 1; i++) /* цикл присваивания значений элементов старого массива для нового
                                                                       до элемента с заданной позицией*/ 
                                    newArr[i] = arr[i];
                                for (int i = 0; i < newArr.Length - position + 1; i++) // цикл присваивания оставшихся в массиве элементов
                                    newArr[position - 1 + i] = arr[position - 1 + count + i];
                                arr = newArr; // ссылка нового массива на старый
                                Console.WriteLine("Удаление элемента(ов) завершено\n");
                            }
                            break;
                        }

                    case 5: // блок добавления элементов в массив
                        {
                            
                            bool isConvert;
                            int position, element;
                            do
                            {
                                Console.WriteLine("Введите номер элемента: ");
                                isConvert = int.TryParse(Console.ReadLine(), out position);
                                if (!isConvert)
                                    Console.WriteLine("Неправильно введено число либо выходит из диапазона!\nПопробуйте ещё раз");
                                else if (position <= 0 || position > arr.Length + 1)
                                    Console.WriteLine($"Число должно находиться в промежутке от 1 до {arr.Length + 1}");
                            } while (!isConvert || position <= 0 || position > arr.Length + 1);

                            do
                            {
                                Console.WriteLine("Введите элемент: ");
                                isConvert = int.TryParse(Console.ReadLine(), out element);
                                if (!isConvert)
                                    Console.WriteLine("Неправильно введено число либо выходит из диапазона!\nПопробуйте ещё раз");
                            } while (!isConvert);

                            if (arr.Length + 1 > 2147483591)
                                Console.WriteLine("Достигнута максимальная длина массива\nНевозможно добавить элемент\n");
                            else
                            {
                                int[] newArr = new int[arr.Length + 1]; // создание нового массива с длинной, большей старого массива на 1
                                for (int i = 0; i < position - 1; i++) /* цикл присваивания значений элементов старого массива для нового
                                                                    до числа с заданной позицией */
                                    newArr[i] = arr[i];
                                newArr[position - 1] = element; // присваивание элементу массива введённого пользователем значения
                                for (int i = position; i < arr.Length + 1; i++) /* цикл присваивания значений элементов старого массива для нового
                                                                            после числа с заданной позицией */
                                    newArr[i] = arr[i - 1];
                                arr = newArr;

                                Console.WriteLine("Элемент добавлен в массив\n");
                            }
                            break;
                        }

                    case 6: // блок смены элементов по позициям
                        {
                            if (arr.Length == 0)
                                Console.WriteLine("Массив пуст, невозможно поменять элементы\n");
                            else
                            {
                                int i = 0; // инициализация переменной, отвечающей за чётные номера элементов
                                while (i < arr.Length - 1) // цикл смены элементов
                                {
                                    int temp; // объявление временной переменной
                                    temp = arr[i]; // присваивание переменной значения элемента под чётным номером
                                    arr[i] = arr[i + 1]; // присваивание элементу под четным номером значения элемента под нечётным номером
                                    arr[i + 1] = temp; // присваивание элементу под нечётным номером значения элемента под чётным номером
                                    i += 2; // следующий чётный номер
                                }
                                Console.WriteLine("Элементы поменяны местами\n");
                            }
                            break;
                        }

                    case 7: // блок поиска элемента, равного среднему арифметическому
                        {
                            if (arr.Length == 0)
                                Console.WriteLine("Массив пуст, невозможно осуществить поиск\n");
                            else
                            {
                                int k = 0; // инициализация переменной, отвечающей за счётчик
                                int sumArr = 0; // инициализация переменной, отвечающей за сумму элементов массива
                                foreach (int element in arr) // цикл подсчёта суммы элементов массива
                                    sumArr += element;
                                double averageValue = (double)sumArr / arr.Length; // инициализация переменной, отвечающей за среднее арифметическое

                                foreach (int element in arr) // цикл сравнения элементов массива со средним арифметическим
                                {
                                    k++;
                                    if (element == (double)averageValue)
                                    {
                                        Console.WriteLine($"Элемент, равный среднему арифметическому элементов массива: {element}");
                                        Console.WriteLine($"Количество сравнений: {k}\n");
                                        break;
                                    }
                                }
                                if (k == arr.Length && k != 1)
                                    Console.WriteLine("Нет такого элемента в массиве\n");
                            }
                            break;
                        }

                    case 8:  // блок сортировки массива простым обменом
                        {
                            if (arr.Length == 0)
                                Console.WriteLine("Массив пуст, невозможно отсортировать\n");
                            else
                            {
                                int temp; // объявление временной переменной
                                for (int i = 0; i < arr.Length; i++) // цикл прохода по всем элементам
                                {
                                    for (int j = arr.Length - 1; j > i; j--) // цикл сравнения соседних чисел
                                    {
                                        if (arr[j] < arr[j - 1]) // сортировка по возрастанию
                                        {
                                            temp = arr[j];
                                            arr[j] = arr[j - 1];
                                            arr[j - 1] = temp;
                                        }
                                    }
                                }
                                Console.WriteLine("Массив отсортирован\n");
                            }
                            break;
                        }

                    case 9: // блок бинарного поиска элемента массива
                        {
                            if (arr.Length == 0) 
                                Console.WriteLine("Массив пуст, невозможно осуществить поиск\n");
                            else
                            {
                                arr = QuickSort(arr, 0, arr.Length - 1); // быстрая сортировка массива

                                bool isConvert;
                                int inputElement; // объявление переменной, отвечающей за введённый элемент
                                do // цикл проверки корректного ввода элемента массива
                                {
                                    Console.WriteLine("Введите целочисленный элемент:");
                                    isConvert = int.TryParse(Console.ReadLine(), out inputElement);
                                    if (!isConvert)
                                        Console.WriteLine("Неправильно введено число\n");
                                } while (!isConvert);

                                int maxNumberComparisons = (int)Math.Ceiling(Math.Log2(arr.Length)) + 1; // инициализация переменной, отвечающей за наибольшее возможное число сравнений
                                int lowerBound = 0; // инициализация переменной, отвечающей за нижнюю границу
                                int upperBound = arr.Length - 1; // инициализация переменной, отвечающей за верхнюю границу
                                int numberComparisons = 1; // инициализация переменной, отвечающей за количество сравнений
                                int centerPos = (lowerBound + upperBound) / 2; // инициализация переменной, отвечающей за центральное значение
                                while (arr[centerPos] != inputElement && numberComparisons <= maxNumberComparisons - 1) // цикл изменения границ в зависимости от значений введённого числа
                                {
                                    if (inputElement < arr[centerPos])
                                        upperBound = centerPos - 1;
                                    else if (inputElement > arr[centerPos])
                                        lowerBound = centerPos + 1;
                                    centerPos = (lowerBound + upperBound) / 2;
                                    numberComparisons++;
                                }
                                if (numberComparisons == maxNumberComparisons)
                                    Console.WriteLine("Нет такого элемента в массиве\n");
                                else
                                    Console.WriteLine($"Элемент найден\nКоличиство сравнений: {numberComparisons}\n");
                            }
                            break;
                        }

                    case 10: // блок очистки консоли
                        {
                            Console.Clear(); // очистка консоли
                            Console.WriteLine("Консоль очищена\n");
                            break;
                        }

                    case 11: // блок завершения работы программы
                        {
                            Console.WriteLine("Работа завершена");
                            break;
                        }

                    default: // блок ввода некорректного пункта меню
                        {
                            Console.WriteLine("Неправильно введён пункт меню\n");
                            break;
                        }
                }
            } while (answer != 11);
        }
    }
}
