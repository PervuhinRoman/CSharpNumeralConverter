/* Замечания от 26.07.2021 2:00
 * Нужно использовать double для хранения исходного и конечного числа - НЕ НУЖНО
 * Разделить число на две части: целую и дробную - ГОТОВО
 * Понять как работать с буквами в составе числа - ГОТОВО
 */

/* Замечания от 27.07.2021 1:53
 * Реализовать перевод дробной части - ГОТОВО
 * Причесать код - ГОТОВО
 * Реализовать перевод и дробной, и целой частей из десятичной в любую другую - ГОТОВО
 */

/* Замечания от 27.07.2021 16:06
 * Превратить метод MidState в метод, который возвращает значение, а не просто выполняет действие - ГОТОВО
 * Сделать проверку на введённые значения - ГОТОВО
 * Автосохранение - ГОТОВО
 */

/* Замечания от 28.07.2021 1:34
 ** Сам конвертер работает **
 * Доделать проверку incoorect символов в number
 * Проверка на русские буквы
 */


using System;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            string number = "";

            string wholePart;
            string fractionalPart;

            int BaseNumber = 0;                            // основание СС исходного числа         
            int NewBaseNumber = 0;                         // основание СС конечного числа

            bool numberErrorState = false;                 // переменная статуса ошибки, если недопустимые символы в number
             
            void InputFunc(string number)                  // делит введённое число на целую и дробную часть               
            {                
                number += ".0";                            // решает проблему, если введена только целая часть
                string[] parts = number.Split('.', ',');

                wholePart = Convert.ToString(parts[0]);
                fractionalPart = Convert.ToString(parts[1]);

                // Test #1 Console.WriteLine("{0} - whole part, {1} - fractionalPart, test: {2}", wholePart, fractionalPart, (wholePart + fractionalPart));
            }

            int[] MidState(string num)                     // создаёт массив чисел, которые соответствуют номерам символв первоначалльного числа по ascii
            {
                int[] symbols = new int[num.Length];
                for (int i = 0; i < num.Length; i++)
                {
                    int item = (int)num[i];

                    if(item >= 97 & item <= 122)
                    {
                        item -= 32;
                    }

                    int itemNum;
                    //itemNum = (int)num[i] - 48; // На всякий случай если не хочется проверять маленькие буквы
                    itemNum = item - 48;

                    if (itemNum > BaseNumber - 1)
                    {
                        numberErrorState = true;
                    }

                    if (itemNum >= 10)
                    {
                        itemNum -= 7;

                        if (itemNum > BaseNumber - 1)
                        {
                            numberErrorState = true;
                        }
                    }
                    symbols[i] = itemNum;
                    // Тест #2 Console.WriteLine((int)num[i]);
                }

                return symbols;
            }

            int ToDec(int[] arr)
            {                                              // перевод в 10-ую СС
                int DecNumber = 0;

                int n = 0;
                for (int i = arr.Length - 1; i >= 0; i--)
                {
                    DecNumber += arr[i] * Convert.ToInt32(Math.Pow(BaseNumber, n));
                    n++;
                }

                return DecNumber;
            }

            string FromDec(int decNumber, int newBase)
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
                    } else if (numItem < 10)
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

            string OutputFunc(string p1, string p2)
            {
                string answer = "";

                if(p2 == "")
                {
                    answer = p1;
                } else
                {
                    answer = p1 + "." + p2;
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

                while (state == true)
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
                
                Console.WriteLine("Whole part in decimal notation: {0}", ToDec(MidState(wholePart)));
                Console.WriteLine("Fractional part in decimal notation: {0}", ToDec(MidState(fractionalPart)));
                
                Console.WriteLine("Answer: {0} ({1})", OutputFunc(FromDec(ToDec(MidState(wholePart)), NewBaseNumber),
                    FromDec(ToDec(MidState(fractionalPart)), NewBaseNumber)), NewBaseNumber); // основной вывод
                Console.WriteLine("-------------------------------------------------------");
            }            
        }
    }
}
