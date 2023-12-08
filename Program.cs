namespace lab5
{
    internal class Program
    {
        static void PrintMainMenu()
        {
            Console.WriteLine("=-=-= ГЛАВНОЕ МЕНЮ =-=-=");
            Console.WriteLine("1. Работа с одномерными массивами");
            Console.WriteLine("2. Работа с двумерными массивами");
            Console.WriteLine("3. Работа с рваными массивами");
            Console.WriteLine("0. Завершить работу");
            Console.WriteLine("\nВыберите пункт меню:");
        }
        static void PrintDimMenu()
        {
            Console.WriteLine("=-=-= ОДНОМЕРНЫЕ МАССИВЫ =-=-=");
            Console.WriteLine("1. Создать массив");
            Console.WriteLine("2. Напечатать массив");
            Console.WriteLine("3. Удалить элемент равный среднему арифметическому элементов массива");
            Console.WriteLine("4. Выйти в главное меню");
            Console.WriteLine("\nВыберите пункт меню:");
        }
        static void PrintTDimMenu()
        {
            Console.WriteLine("=-=-= ДВУМЕРНЫЕ МАССИВЫ =-=-=");
            Console.WriteLine("1. Создать массив");
            Console.WriteLine("2. Напечатать массив");
            Console.WriteLine("3. Добавить столбец в конец матрицы");
            Console.WriteLine("4. Выйти в главное меню");
            Console.WriteLine("\nВыберите пункт меню:");
        }
        static void PrintJagMenu()
        {
            Console.WriteLine("=-=-= РВАНЫЕ МАССИВЫ =-=-=");
            Console.WriteLine("1. Создать массив");
            Console.WriteLine("2. Напечатать массив");
            Console.WriteLine("3. Удалить все строки, в которых встречается заданное число K");
            Console.WriteLine("4. Выйти в главное меню");
            Console.WriteLine("\nВыберите пункт меню:");
        }
        static void PrintArr(int[] dimArr)
        {
            if (dimArr.Length == 0) // проверка массива на пустоту
                Console.WriteLine("Массив пуст\n");
            else
                Console.WriteLine("[{0}]\n", string.Join(", ", dimArr)); // вывод массива
        }
        static void PrintArr(int[,] tDimArr)
        {
            if (tDimArr.Length == 0) // проверка массива на пустоту
                Console.WriteLine("Массив пуст\n");
            else
                for (int i = 0; i < tDimArr.GetLength(0); i++)
                {
                    for (int j = 0; j < tDimArr.GetLength(1); j++)
                    {
                        Console.Write($"{tDimArr[i,j],4}");
                    }
                    Console.WriteLine("\n");
                }
        }
        static void PrintArr(int[][] jagArr)
        {
            if (jagArr.Length == 0) // проверка массива на пустоту
                Console.WriteLine("Массив пуст\n");
            else
                for (int i = 0; i < jagArr.Length; i++)
                {
                    for (int j = 0; j < jagArr[i].Length; j++)
                    {
                        Console.Write($"{jagArr[i][j], 4}");
                    }
                    Console.WriteLine("\n");
                }
        }
        static int ArrSize(string msg)
        {
            int size = -1;
            Console.Write(msg);
            while (size < 0)
            {
                size = GetInt();
                if (size < 0)
                {
                    Console.Write("Количество может быть только неотрицательным целым числом!\nПовторите ввод: ");
                }
            }
            return size;
        }
        static int[] CreateArr(int[] dimArr)
        {
            dimArr = new int[ArrSize("Введите количество элементов массива: ")];
            return dimArr;
        }
        static int[] CreateArr(int[] dimArr, int size)
        {
            dimArr = new int[size];
            return dimArr;
        }
        static int[,] CreateArr(int[,] tDimArr)
        {
            tDimArr = new int[ArrSize("Введите количество строк массива: "), ArrSize("Введите количество столбцов массива: ")];
            return tDimArr;
        }
        static int[][] CreateArr(int[][] jagArr)
        {
            jagArr = new int[ArrSize("Введите количество строк массива: ")][];
            return jagArr;
        }
        static int[] DeleteElement(ref int[] dimArr)
        {
            if (dimArr.Length == 0)
            {
                Console.Clear();
                Console.WriteLine("Удалить невозможно, так как массив пуст\n");
            }
            else
            {
                int k = 0; // инициализация переменной, отвечающей за счётчик
                double averageValue = (double)dimArr.Sum() / dimArr.Length; // инициализация переменной, отвечающей за среднее арифметическое

                foreach (int element in dimArr) // цикл сравнения элементов массива со средним арифметическим
                {
                    if (element == (double)averageValue)
                        k++;
                }
                if (k == 0)
                    Console.WriteLine("Невозможно удалить, так как нет такого элемента в массиве\n");
                else if (k == 1)
                {
                    int[] newDimArr = new int[dimArr.Length - 1];
                    int index = 0;
                    for (int i = 0; i < dimArr.Length; i++)
                    {
                        if (dimArr[i] != averageValue)
                        {
                            newDimArr[index] = dimArr[i];
                            index++;
                        }
                    }
                    dimArr = newDimArr;
                    Console.WriteLine($"Удаление элемента {averageValue} завершено\n");
                }
                else
                {
                    Console.WriteLine("В массиве найдено несколько таких элементов");
                    Console.WriteLine("1. Удалить первый попавшийся элемент");
                    Console.WriteLine("2. Удалить все подходящие элементы");
                    Console.Write("\nВыберите подходящее действие: ");
                    int answer = GetInt();
                    switch (answer)
                    {
                        case 1:
                            {
                                int[] newDimArr = new int[dimArr.Length - 1];
                                int index = 0, temp = 0;
                                for (int i = 0; i < dimArr.Length; i++)
                                {
                                    if (dimArr[i] != averageValue || temp > 0)
                                    {
                                        newDimArr[index] = dimArr[i];
                                        index++;
                                    }
                                    else
                                        temp++;
                                }
                                dimArr = newDimArr;
                                Console.Clear();
                                Console.WriteLine($"Удаление первого попавшегося элемента {averageValue} завершено\n");
                                break;
                            }

                        case 2:
                            {
                                int[] newDimArr = new int[dimArr.Length - k];
                                int index = 0;
                                for (int i = 0; i < dimArr.Length; i++)
                                {
                                    if (dimArr[i] != averageValue)
                                    {
                                        newDimArr[index] = dimArr[i];
                                        index++;
                                    }
                                }
                                dimArr = newDimArr;
                                Console.Clear();
                                Console.WriteLine($"Удаление всех элементов {averageValue} завершено\n");
                                break;
                            }
                    }
                }
            }
            return dimArr;
        }
        static int[,] AddColumn(ref int[,] tDimArr)
        {
            int[,] newTDimArr;
            if (tDimArr.Length == 0)
            {
                newTDimArr = new int[1, tDimArr.GetLength(1) + 1];
            }
            else
            {
                newTDimArr = new int[tDimArr.GetLength(0), tDimArr.GetLength(1) + 1];
            }
            newTDimArr = CopyElements(tDimArr, newTDimArr);
            int[] newColumn = new int[0];
            if (tDimArr.Length == 0)
            {
                newColumn = FillArr(newColumn, 1);
                if (newColumn.Length != 0)
                {
                    for (int i = 0; i < 1; i++)
                        newTDimArr[i, 0] = newColumn[i];
                }
                else
                    newTDimArr = new int[0,0];
            }
            else
            {
                newColumn = FillArr(newColumn, tDimArr.GetLength(0));
                if (newColumn.Length != 0)
                {
                    for (int i = 0; i < tDimArr.GetLength(0); i++)
                    {
                        newTDimArr[i, tDimArr.GetLength(1)] = newColumn[i];
                    }
                }
                else
                    return tDimArr;
            }
            tDimArr = newTDimArr;
            return tDimArr;
        }
        static int[,] CopyElements(int[,] tDimArr, int[,] newTDimArr)
        {
            if (newTDimArr.Length == 0)
                return newTDimArr;
            else
            {
                for (int i = 0; i < tDimArr.GetLength(0); i++)
                {
                    for (int j = 0; j < tDimArr.GetLength(1); j++)
                    {
                        newTDimArr[i, j] = tDimArr[i, j];
                    }
                }
                return newTDimArr;
            }
        }
        static int[] FillArr(ref int[] dimArr)
        {
            Console.WriteLine("=-= СОЗДАНИЕ МАССИВА =-=");
            Console.WriteLine("1. С помощью ручного ввода");
            Console.WriteLine("2. С помощью ДСЧ");
            Console.WriteLine("Любое другое число - выход в подменю");
            Console.WriteLine("\nВыберите способ заполнения массива: ");
            int answer = GetInt();
            switch(answer)
            {
                case 1:
                    {
                        dimArr = CreateArr(dimArr);
                        for (int i = 0; i < dimArr.Length; i++)
                        {
                            Console.Write($"Введите {i + 1} элемент массива: ");
                            dimArr[i] = GetInt();
                        }
                        Console.Clear();
                        if (dimArr.Length == 0)
                            Console.WriteLine("Задан пустой массив\n");
                        else
                            Console.WriteLine($"Массив сформирован из {dimArr.Length} элементов\n");
                        break;
                    }
                case 2:
                    {
                        dimArr = CreateArr(dimArr);
                        Random rnd = new Random();

                        for (int i = 0; i < dimArr.Length; i++)
                        {
                            dimArr[i] = rnd.Next(-99, 100);
                        }
                        Console.Clear();
                        if (dimArr.Length == 0)
                            Console.WriteLine("Задан пустой массив\n");
                        else
                            Console.WriteLine($"Массив сформирован из {dimArr.Length} элементов\n");
                        break;
                    }
                default:
                    {
                        Console.Clear();
                        Console.WriteLine("Выход в подменю\n");
                        break;
                    }
            }
            return dimArr;
        }
        static int[] FillCol(int[] col, int answer = 1)
        {
            Console.Clear();
            switch(answer)
            {
                case 1:
                    {
                        col = CreateArr(col);
                        for (int i = 0; i < col.Length; i++)
                        {
                            Console.WriteLine($"Введите {i + 1} элемент массива: ");
                            col[i] = GetInt();
                        }
                        break;
                    }
                case 2:
                    {
                        col = CreateArr(col);
                        Random rnd = new Random();

                        for (int i = 0; i < col.Length; i++)
                        {
                            col[i] = rnd.Next(0, 100);
                        }
                        break;
                    }
            }
            return col;
        }
        static int[] FillArr(int[] dimArr, int size)
        {
            Console.WriteLine("=-= ДОБАВЛЕНИЕ НОВОГО СТОЛБЦА =-=");
            Console.WriteLine("1. С помощью ручного ввода");
            Console.WriteLine("2. С помощью ДСЧ");
            Console.WriteLine("Любое другое число - выход в подменю");
            Console.WriteLine("\nВыберите способ заполнения столбца: ");
            int answer = GetInt();
            switch (answer)
            {
                case 1:
                    {
                        dimArr = CreateArr(dimArr, size);
                        for (int i = 0; i < dimArr.Length; i++)
                        {
                            Console.WriteLine($"Введите {i + 1} элемент столбца: ");
                            dimArr[i] = GetInt();
                        }
                        Console.Clear();
                        if (dimArr.Length == 0)
                            Console.WriteLine("Задан пустой столбец\n");
                        else
                            Console.WriteLine($"Столбец сформирован из {dimArr.Length} элементов и добавлен к матрице\n");
                        break;
                    }
                case 2:
                    {
                        dimArr = CreateArr(dimArr, size);
                        Random rnd = new Random();

                        for (int i = 0; i < dimArr.Length; i++)
                        {
                            dimArr[i] = rnd.Next(-99, 100);
                        }
                        Console.Clear();
                        if (dimArr.Length == 0)
                            Console.WriteLine("Задан пустой столбец\n");
                        else
                            Console.WriteLine($"Столбец сформирован из {dimArr.Length} элементов и добавлен к матрице\n");
                        break;
                    }
                default:
                    {
                        Console.Clear();
                        Console.WriteLine("Выход в подменю\n");
                        break;
                    }
            }
            return dimArr;
        }
        static int[,] FillArr(ref int[,] tDimArr)
        {
            Console.WriteLine("=-= СОЗДАНИЕ МАССИВА =-=");
            Console.WriteLine("1. С помощью ручного ввода");
            Console.WriteLine("2. С помощью ДСЧ");
            Console.WriteLine("Любое другое число - выход в подменю");
            Console.WriteLine("\nВыберите способ заполнения массива: ");
            int answer = GetInt();
            switch (answer)
            {
                case 1:
                    {
                        tDimArr = CreateArr(tDimArr);
                        for (int i = 0; i < tDimArr.GetLength(0); i++)
                        {
                            for (int j = 0; j < tDimArr.GetLength(1); j++)
                            {
                                Console.WriteLine($"Введите {j + 1} элемент {i + 1} строки:");
                                tDimArr[i, j] = GetInt();
                            }
                        }
                        Console.Clear();
                        if (tDimArr.Length == 0)
                            Console.WriteLine("Задан пустой массив\n");
                        else
                            Console.WriteLine($"Массив сформирован из {tDimArr.GetLength(0)} строк и {tDimArr.GetLength(1)} столбцов\n");
                        break;
                    }
                case 2:
                    {
                        tDimArr = CreateArr(tDimArr);
                        Random rnd = new Random();

                        for (int i = 0; i < tDimArr.GetLength(0); i++)
                        {
                            for (int j = 0; j < tDimArr.GetLength(1); j++)
                            {
                                tDimArr[i, j] = rnd.Next(-100, 100);
                            }
                        }
                        Console.Clear();
                        if (tDimArr.Length == 0)
                            Console.WriteLine("Задан пустой массив\n");
                        else
                            Console.WriteLine($"Массив сформирован из {tDimArr.GetLength(0)} строк и {tDimArr.GetLength(1)} столбцов\n");
                        break;
                    }
                default:
                    {
                        Console.Clear();
                        Console.WriteLine("Выход в подменю\n");
                        break;
                    }
            }
            return tDimArr;
        }
        static int[][] FillArr(ref int[][] jagArr)
        {
            Console.WriteLine("=-= СОЗДАНИЕ МАССИВА =-=");
            Console.WriteLine("1. С помощью ручного ввода");
            Console.WriteLine("2. С помощью ДСЧ");
            Console.WriteLine("Любое другое число - выход в подменю");
            Console.WriteLine("\nВыберите способ заполнения массива: ");
            int answer = GetInt();
            switch (answer)
            {
                case 1:
                    {
                        jagArr = CreateArr(jagArr);
                        for (int i = 0; i < jagArr.Length; i++)
                        {
                            int[] col = new int[0];
                            col = FillCol(col);
                            jagArr[i] = col;
                        }
                        Console.Clear();
                        if (jagArr.Length == 0)
                            Console.WriteLine("Задан пустой массив\n");
                        else
                            Console.WriteLine($"Массив сформирован из {jagArr.Length} строк\n");
                        break;
                    }
                case 2:
                    {
                        jagArr = CreateArr(jagArr);
                        Random rnd = new Random();

                        for (int i = 0; i < jagArr.Length; i++)
                        {
                            int[] col = new int[0];
                            col = FillCol(col, 2);
                            jagArr[i] = col;
                        }
                        Console.Clear();
                        if (jagArr.Length == 0)
                            Console.WriteLine("Задан пустой массив\n");
                        else
                            Console.WriteLine($"Массив сформирован из {jagArr.Length} строк\n");
                        break;
                    }
                default:
                    {
                        Console.Write("Выход в подменю");
                        break;
                    }
            } 
            return jagArr;
        }
        static int[][] DeleteString(ref int[][] jagArr)
        {
            if (jagArr.Length == 0)
                Console.WriteLine("Массив пуст, невозможно удалить строки\n");
            else
            {
                int numberFind;
                Console.WriteLine("Введите элемент для поиска: ");
                numberFind = GetInt();
                int countString = CountFindNumber(jagArr, numberFind);
                if (countString == 0)
                {
                    Console.Clear();
                    Console.WriteLine("Удаление не произведено, так как не было найдено подходящих строк\n");
                }
                else
                {
                    int[][] newJagArr = new int[jagArr.Length - countString][];
                    int i = -1, j = -1;
                    foreach (int[] item in jagArr)
                    {
                        i++;
                        int countNumbers = 0;
                        foreach (int elem in item)
                        {
                            if (elem != numberFind)
                                countNumbers++;
                        }
                        if (countNumbers == jagArr[i].Length)
                        {
                            j++;
                            newJagArr[j] = jagArr[i];
                        }
                    }
                    jagArr = newJagArr;
                    Console.Clear();
                    Console.WriteLine("Удаление строк завершено\n");
                }
            }
            return jagArr;
        }
        static int CountFindNumber(int[][] jagArr, int numberFind)
        {
            int countNumbers = 0;
            foreach (int[] item in jagArr)
            {
                foreach (int elem in item)
                {
                    if (elem == numberFind)
                    {
                        countNumbers++;
                        break;
                    }
                }
            }
            return countNumbers;
        }
        static int GetInt()
        {
            int intNumber;
            bool isConvert;
            do // цикл проверки корректного ввода пункта меню
            {
                isConvert = int.TryParse(Console.ReadLine(), out intNumber); // проверка введённой строки на принадлежность к целочисленному типу
                if (!isConvert || intNumber >= 1000 || intNumber <= -1000) // введённое число нецелочисленное
                    Console.Write("Неправильно введено число либо выходит из диапазона!\nПопробуйте ещё раз: ");
            } while (!isConvert || intNumber >= 1000 || intNumber <= -1000);

            return intNumber;
        }
        static void ExitMenu()
        {
            Console.Clear();
            Console.WriteLine("Вы уверены, что хотите завершить работу?");
            Console.WriteLine("1. Да");
            Console.WriteLine("2. Нет");
        }
        static void Main(string[] args)
        {
            int[] dimArr = new int[0];
            int[,] tDimArr = new int[0,0];
            int[][] jagArr = new int[0][];
            int answer;
            do
            {
                // вывод меню в консоль
                PrintMainMenu();
                answer = GetInt();
                Console.Clear();
                switch (answer)
                {
                    case 1:
                        {
                            do
                            {
                                PrintDimMenu();
                                answer = GetInt();
                                Console.Clear();
                                switch (answer)
                                {
                                    case 1:
                                        {
                                            FillArr(ref dimArr);
                                            break;
                                        }
                                    case 2:
                                        {
                                            PrintArr(dimArr);
                                            break;
                                        }
                                    case 3:
                                        {
                                            DeleteElement(ref dimArr);
                                            break;
                                        }
                                    case 4:
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
                            } while(answer != 4);
                            break;
                        }

                    case 2:
                        {
                            do
                            {
                                PrintTDimMenu();
                                answer = GetInt();
                                Console.Clear();
                                switch (answer)
                                {
                                    case 1:
                                        {
                                            FillArr(ref tDimArr);
                                            break;
                                        }
                                    case 2:
                                        {
                                            PrintArr(tDimArr);
                                            break;
                                        }
                                    case 3:
                                        {
                                            AddColumn(ref tDimArr);
                                            break;
                                        }
                                    case 4:
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
                            } while (answer != 4);
                            break;
                        }

                    case 3:
                        {
                            do
                            {
                                PrintJagMenu();
                                answer = GetInt();
                                Console.Clear();
                                switch (answer)
                                {
                                    case 1:
                                        {
                                            FillArr(ref jagArr);
                                            break;
                                        }
                                    case 2:
                                        {
                                            PrintArr(jagArr);
                                            break;
                                        }
                                    case 3:
                                        {
                                            DeleteString(ref jagArr);
                                            break;
                                        }
                                    case 4:
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
                            } while (answer != 4);
                            break;
                        }

                    case 0:
                        {
                            do
                            {
                                ExitMenu();
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