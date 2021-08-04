using System;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            string number = "";                            // число, которое необходимо перевести

            string wholePart = "";                         // целая чать числа
            string fractionalPart = "";                    // дробная часть числа

            int BaseNumber = 0;                            // основание СС исходного числа         
            int NewBaseNumber = 0;                         // основание СС конечного числа
            int accuracyNewNumber = 13;                    // точность вычислений, кол-во знаков после точки/запятой, если в задачи присутствует данное требование

            bool numberErrorState = false;                 // переменная статуса ошибки, если недопустимые символы в number

            void InputFunc(string number)                  // метод делит введённое число на целую и дробную часть               
            {
                number += ".0";                            // решает проблему, если введена только целая часть
                string[] parts = number.Split('.', ',');

                wholePart = Convert.ToString(parts[0]);
                fractionalPart = Convert.ToString(parts[1]);
            }

            int[] MidState(string num)                     // создаёт массив чисел, которые соответствуют номерам символов изначального числа по ascii
            {
                int[] symbols = new int[num.Length];
                for (int i = 0; i < num.Length; i++)
                {
                    int item = (int)num[i];

                    if (item >= 97 & item <= 122)
                    {
                        item -= 32;
                    }

                    int itemNum;
                    itemNum = item - 48;

                    if (itemNum > BaseNumber - 1)          // проверка на некорректность символа в составе числа (если символ/цифра блльше чем основание СС - 1, т.к. с 10-ти начинаются буквы)
                    {
                        numberErrorState = true;
                    }

                    if (itemNum >= 10)                     // перевод цифры в буквы (как было сказано выше с 10-ти начинаются буквы английского алфавита)
                    {
                        itemNum -= 7;

                        if (itemNum > BaseNumber - 1)
                        {
                            numberErrorState = true;
                        }
                    }
                    symbols[i] = itemNum;
                }

                return symbols;
            }

            // блок методов для целой части числа

            int ToDec(int[] arr)                           // метод перевода целой части числа в 10-ую СС
            {
                int DecNumber = 0;

                int n = 0;
                for (int i = arr.Length - 1; i >= 0; i--)
                {
                    DecNumber += arr[i] * Convert.ToInt32(Math.Pow(BaseNumber, n));
                    n++;
                }

                return DecNumber;
            }

            string FromDec(int decNumber, int newBase)     // метод перевода целой части числа в новую, заданную СС
            {
                string newNumber0 = "";
                string newNumber = "";

                string item = "";
                int numItem = 0;

                while (decNumber > 0)                      // работа с буквами                           
                {
                    numItem = decNumber % newBase;

                    if (numItem >= 10)
                    {
                        item = Convert.ToString((char)(numItem + 55));
                    }
                    else if (numItem < 10)
                    {
                        item = Convert.ToString(numItem);
                    }

                    newNumber0 += item;
                    decNumber /= newBase;
                }

                for (int i = newNumber0.Length - 1; i >= 0; i--)
                {
                    newNumber += newNumber0[i];
                }

                return newNumber;
            }

            // блок методов для дробной части числа

            double ToDecFractional(int[] arr)              // метод перевода дробной части числа в 10-ую СС
            {
                double DecNumber = 0;

                int n = 1;
                for (int i = 0; i < arr.Length; i++)
                {
                    DecNumber += Convert.ToDouble(arr[i]) * Math.Pow(BaseNumber, n * (-1));     //  n*(-1) добавлен для создания отрицательной степени, этого требует алгоритм создания дробной части числа
                    n++;
                }

                return DecNumber;
            }

            string FromDecFractional(double decNumber, int newBase)
            {
                string newNumber = "0.";

                string sNumber = "";                       // текущее дробное число в типе string для удобного деления на целую и дробную части
                string w = "";                             // от англ. whole - т.е. целая часть числа дробного числа, как бы любопытно это не звучало :)
                string f = "";                             // от англ. fractional - т.е. дробная часть числа дробного числа

                double dNumber = decNumber;                // текущее дробное число в типе double для вычислений

                bool dotIn = true;                         // переменная отвечающая за наличие ',' или '.' в числе

                while (f != "0" & accuracyNewNumber != 0)
                {
                    dNumber = dNumber * newBase;
                    sNumber = Convert.ToString(dNumber) + ".0";

                    string[] num = sNumber.Split('.', ',');        // деление числа на целую и дробную часть по знаку

                    if (dotIn == true)                     // если число не дробное, то программа не сможет взять дробную часть, а значит эта часть равна нулю
                    {
                        f = num[1];
                    }
                    else
                    {
                        f = "0";
                    }

                    w = num[0];
                    int wItem = Convert.ToInt32(w);

                    if (wItem >= 10)                       // работа с буквами
                    {
                        w = Convert.ToString((char)(wItem + 55));
                    }
                    else if (wItem < 10)
                    {
                        w = Convert.ToString(wItem);
                    }

                    newNumber += w;
                    dNumber = Convert.ToDouble("0," + f);

                    accuracyNewNumber--;                   /* если вычисления получаются бесконечными, например при переводе 0,56(10) в 2-ую, 
                                                              то по истеении данного счётчика-кол-вазнаков после запятой, программа будут остановлена */
                }

                return newNumber;
            }

            string OutputFunc(string p1, string p2)        // метод объединяющий результаты выполнения блоков для целой и дробной части числа
            {
                string answer = "";

                char[] MyChar = { '0' };                   // удаление 0 из дробной части
                string newP2 = p2.TrimStart(MyChar);

                if (p2 == "0")
                {
                    answer = p1;
                }
                else
                {
                    answer = p1 + newP2;
                }

                return answer;
            }

            // Основной код

            bool state = true;                             // отвечает за проверку вводимых значений оснований СС
            byte cnt = 0;                                  // отвеает за напоминания для user-а      

            while (true)
            {
                Console.Write("Enter the number: ");
                number = Console.ReadLine();

                while (state == true)                      // алгоритмы проверки вводимых пользователем значений
                {
                    if (cnt > 0)
                    {
                        Console.WriteLine("Invalid expression! The value must be between 2 and 36. Try again");
                    }

                    Console.Write("From: ");
                    BaseNumber = Convert.ToInt32(Console.ReadLine());

                    state = (BaseNumber < 2 || BaseNumber > 36);
                    cnt++;
                }

                state = true;
                cnt = 0;

                while (state == true)
                {
                    if (cnt > 0)
                    {
                        Console.WriteLine("Invalid expression! The value must be between 2 and 36. Try again");
                    }

                    Console.Write("To: ");
                    NewBaseNumber = Convert.ToInt32(Console.ReadLine());

                    state = (NewBaseNumber < 2 || NewBaseNumber > 36);
                    cnt++;
                }

                state = true;
                cnt = 0;

                InputFunc(number);

                // Основной вывод

                int decWholeNumber = ToDec(MidState(wholePart));
                string newWholeNumber = FromDec(decWholeNumber, NewBaseNumber);

                double decFracNumber = ToDecFractional(MidState(fractionalPart));
                string newFracNumber = FromDecFractional(decFracNumber, NewBaseNumber);

                Console.WriteLine("Quick answer: {0}", OutputFunc(newWholeNumber, newFracNumber));
                Console.WriteLine();

                Console.WriteLine("More:");
                Console.WriteLine("In 10: whole part = {0}; fractional part = {1}", decWholeNumber, decFracNumber);
                Console.WriteLine("{0} ({1}) = {2} ({3})", number, BaseNumber, OutputFunc(newWholeNumber, newFracNumber), NewBaseNumber);
                Console.WriteLine("-------------------------------------------------------");
                Console.WriteLine();
            }
        }
    }
}